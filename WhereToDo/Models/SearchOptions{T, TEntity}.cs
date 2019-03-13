////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: SearchOptions.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: The search options provided within the API URL request. Provided by referenced Lynda.com course
 * 
 * Searching not yet implemented - 2-28-19
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
    public class SearchOptions<T, TEntity> : IValidatableObject
    {
        public string[] Search { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var processor = new SearchOptionsProcessor<T, TEntity>(Search);

            var validTerms = processor.GetValidTerms().Select(x => x.Name);
            var invalidTerms = processor.GetAllTerms().Select(x => x.Name)
                .Except(validTerms, StringComparer.OrdinalIgnoreCase);

            foreach (var term in invalidTerms)
            {
                yield return new ValidationResult(
                    $"Invalid search term '{term}'.",
                    new[] { nameof(Search) });
            };
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            var processor = new SearchOptionsProcessor<T, TEntity>(Search);
            return processor.Apply(query);
        }
    }
}
