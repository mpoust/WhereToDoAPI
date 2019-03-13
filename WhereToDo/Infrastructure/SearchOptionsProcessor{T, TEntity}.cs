////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: SearchOptionsProcessor.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Processes search options provided in API URL. Provided by referenced Lynda.com course.
 * 
 * No searching yet implemented - 2-28-19
 * 
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WhereToDo.Infrastructure
{
    public class SearchOptionsProcessor<T, TEntity>
    {
        private readonly string[] _searchQuery;

        public SearchOptionsProcessor(string[] searchQuery)
        {
            _searchQuery = searchQuery;
        }

        public IEnumerable<SearchTerm> GetAllTerms()
        {
            if (_searchQuery == null) yield break;

            foreach (var expresion in _searchQuery)
            {
                if (string.IsNullOrEmpty(expresion)) continue;

                var tokens = expresion.Split(' ');

                if (tokens.Length == 0)
                {
                    yield return new SearchTerm
                    {
                        ValidSyntax = false,
                        Name = expresion
                    };
                    continue;
                }

                if (tokens.Length < 3)
                {
                    yield return new SearchTerm
                    {
                        ValidSyntax = false,
                        Name = tokens[0]
                    };
                    continue;
                }

                yield return new SearchTerm
                {
                    ValidSyntax = true,
                    Name = tokens[0],
                    Operator = tokens[1],
                    Value = string.Join(" ", tokens.Skip(2))
                };
            }
        }

        public IEnumerable<SearchTerm> GetValidTerms()
        {
            var queryTerms = GetAllTerms()
                .Where(x => x.ValidSyntax)
                .ToArray();

            if (!queryTerms.Any()) yield break;

            var declaredTerms = GetTermsFromModel();

            foreach (var term in queryTerms)
            {
                var declaredTerm = declaredTerms
                    .SingleOrDefault(x => x.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase));
                if (declaredTerm == null) continue;

                yield return new SearchTerm
                {
                    ValidSyntax = term.ValidSyntax,
                    Name = declaredTerm.Name,
                    Operator = term.Operator,
                    Value = term.Value,
                    ExpressionProvider = declaredTerm.ExpressionProvider
                };
            }
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            var terms = GetValidTerms().ToArray();
            if (!terms.Any()) return query;

            var modifiedQuery = query;

            foreach (var term in terms)
            {
                var propertyInfo = ExpressionHelper
                    .GetPropertyInfo<TEntity>(term.Name);
                var obj = ExpressionHelper.Parameter<TEntity>();

                // Build up the LINQ expression backwards:
                var left = ExpressionHelper.GetPropertyExpression(obj, propertyInfo);
                var right = term.ExpressionProvider.GetValue(term.Value);
                var comparisonExpression = term.ExpressionProvider
                    .GetComparison(left, term.Operator, right);
                var lambdaExpression = ExpressionHelper
                    .GetLambda<TEntity, bool>(obj, comparisonExpression);
                modifiedQuery = ExpressionHelper.CallWhere(modifiedQuery, lambdaExpression);
            }
            return modifiedQuery;
        }

        private static IEnumerable<SearchTerm> GetTermsFromModel()
            => typeof(T).GetTypeInfo()
            .DeclaredProperties
            .Where(p => p.GetCustomAttributes<SearchableAttribute>().Any())
            .Select(p => new SearchTerm
            {
                Name = p.Name,
                ExpressionProvider = p.GetCustomAttribute<SearchableAttribute>().ExpressionProvider
            });

    }
}
