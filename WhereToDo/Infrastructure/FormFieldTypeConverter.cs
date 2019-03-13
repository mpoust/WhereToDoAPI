////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: FormFieldTypeConverter.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Assists in converting types between form field to types in API code and database. Provided by referenced Lynda.com course.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

namespace WhereToDo.Infrastructure
{
    public static class FormFieldTypeConverter
    {
        private static IReadOnlyDictionary<Type, string> TypeMapping = new Dictionary<Type, string>()
        {
            [typeof(bool)] = "boolean",
            [typeof(DateTime)] = "datetime",
            [typeof(DateTimeOffset)] = "datetime",
            [typeof(byte)] = "byte",
            [typeof(float)] = "decimal",
            [typeof(double)] = "decimal",
            [typeof(decimal)] = "decimal",
            [typeof(TimeSpan)] = "duration",
            [typeof(short)] = "integer",
            [typeof(int)] = "integer",
            [typeof(long)] = "integer",
            [typeof(string)] = "string"
        };

        public static string GetTypeName(Type fieldType)
        {
            if (fieldType.IsArray) return "array";

            // Unwrap Nullable<> if applicable
            var type = Nullable.GetUnderlyingType(fieldType) ?? fieldType;

            if (TypeMapping.TryGetValue(type, out var value)) return value;

            return null;
        }
    }
}
