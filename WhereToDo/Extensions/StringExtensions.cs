////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: StringExtensions.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Provided by the referenced Lynda.com course. Extending  String to provide camelCasing
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


namespace WhereToDo.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var first = input.Substring(0, 1).ToLower();
            if (input.Length == 1) return first;

            return first + input.Substring(1);
        }
    }
}
