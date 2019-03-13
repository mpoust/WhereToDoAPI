﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: SortOptionsProcessor.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Processes sort options provided in the API URL. Provided by referenced Lynda.com course
 * 
 * No sorting yet implemented - 2-28-19
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
    public class SortOptionsProcessor<T, TEntity>
    {
        private readonly string[] _orderBy;

        public SortOptionsProcessor(string[] orderBy)
        {
            _orderBy = orderBy;
        }

        public IEnumerable<SortTerm> GetAllTerms()
        {
            if (_orderBy == null) yield break; // return empty enumerable

            foreach (var term in _orderBy)
            {
                if (string.IsNullOrEmpty(term)) continue;

                var tokens = term.Split(' ');

                if (tokens.Length == 0)
                {
                    yield return new SortTerm { Name = term };
                    continue;
                }

                var descending = tokens.Length > 1 && tokens[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

                yield return new SortTerm
                {
                    Name = tokens[0],
                    Descending = descending
                };
            }
        }

        // Create a list of valid terms from approved terms from the model
        public IEnumerable<SortTerm> GetValidTerms()
        {
            var queryTerms = GetAllTerms().ToArray();
            if (!queryTerms.Any()) yield break;

            var declaredTerms = GetTermsFromModel();

            foreach (var term in queryTerms)
            {
                var declaredTerm = declaredTerms
                    .SingleOrDefault(x => x.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase));
                if (declaredTerm == null) continue; // not a valid term on the model

                yield return new SortTerm
                {
                    Name = declaredTerm.Name,
                    EntityName = declaredTerm.EntityName,
                    Descending = term.Descending,
                    Default = declaredTerm.Default
                };
            }
        }

        // Get list of sortable fields with reflection
        private static IEnumerable<SortTerm> GetTermsFromModel()
            => typeof(T).GetTypeInfo()
            .DeclaredProperties
            .Where(p => p.GetCustomAttributes<SortableAttribute>().Any())
            .Select(p => new SortTerm
            {
                Name = p.Name,
                EntityName = p.GetCustomAttribute<SortableAttribute>().EntityProperty,
                Default = p.GetCustomAttribute<SortableAttribute>().Default
            });

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            var terms = GetValidTerms().ToArray();
            if (!terms.Any())
            {
                terms = GetTermsFromModel().Where(t => t.Default).ToArray();
            }

            if (!terms.Any()) return query;

            var modifiedQuery = query;
            var useThenBy = false;

            foreach (var term in terms)
            {
                var propertyInfo = ExpressionHelper
                    .GetPropertyInfo<TEntity>(term.Name);
                var obj = ExpressionHelper.Parameter<TEntity>();

                // Build the LINQ expression backwards:
                var key = ExpressionHelper
                    .GetPropertyExpression(obj, propertyInfo);
                var keySelector = ExpressionHelper
                    .GetLambda(typeof(TEntity), propertyInfo.PropertyType, obj, key);

                modifiedQuery = ExpressionHelper
                    .CallOrderByOrThenBy(
                        modifiedQuery, useThenBy, term.Descending, propertyInfo.PropertyType, keySelector);

                useThenBy = true;
            }

            return modifiedQuery;
        }
    }
}
