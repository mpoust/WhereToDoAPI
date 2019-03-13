////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: SortOptions.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: The sort options provided within the API URL request. Provided by referenced Lynda.com course.
 * 
 * Sorting not yet implemented - 2-28-19
 * 
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WhereToDo.Infrastructure;

namespace WhereToDo.Models
{
    public class SortOptions<T, TEntity> : IValidatableObject
    {
        public string[] OrderBy { get; set; }

        // ASP.NET Core calls this to validate incoming parameters
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var processor = new SortOptionsProcessor<T, TEntity>(OrderBy);

            var validTerms = processor.GetValidTerms().Select(x => x.Name);

            var invalidTerms = processor.GetAllTerms().Select(x => x.Name)
                .Except(validTerms, StringComparer.OrdinalIgnoreCase);

            foreach (var term in invalidTerms)
            {
                yield return new ValidationResult(
                    $"Invalid sort term '{term}'.",
                    new[] { nameof(OrderBy) });
            }
        }

        // Service code will call this to apply these sort options to a database query
        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            var processor = new SortOptionsProcessor<T, TEntity>(OrderBy);
            return processor.Apply(query);
        }
    }
}
