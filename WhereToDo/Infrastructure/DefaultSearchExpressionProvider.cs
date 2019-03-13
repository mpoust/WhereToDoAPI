////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: DefaultSearchExpressionProvider.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Provided by referenced Lynda.com course.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WhereToDo.Infrastructure
{
    public class DefaultSearchExpressionProvider : ISearchExpressionProvider
    {
        protected const string EqualsOperator = "eq";

        public virtual IEnumerable<string> GetOperators()
        {
            yield return EqualsOperator;
        }

        public virtual Expression GetComparison(
        MemberExpression left,
        string op,
        ConstantExpression right)
        {
            switch (op.ToLower())
            {
                case EqualsOperator: return Expression.Equal(left, right);
                default: throw new ArgumentException($"Invalid operator '{op}'.");
            }
        }

        public virtual ConstantExpression GetValue(string input)
        => Expression.Constant(input);
    }
}
