////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: ISearchExpressionProvider.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Interface for search expressions. Provided by referenced Lynda.com course.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Linq.Expressions;

namespace WhereToDo.Infrastructure
{
    public interface ISearchExpressionProvider
    {
        IEnumerable<string> GetOperators();

        ConstantExpression GetValue(string input);

        Expression GetComparison(
            MemberExpression left,
            string op,
            ConstantExpression right);
    }
}
