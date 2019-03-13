////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: IonOutputFormatter.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Formatter for outputting responses in line with the ION JSON Specification. Provided by referenced Lynda.com course
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Text;
using System.Threading.Tasks;

namespace WhereToDo.Infrastructure
{
    public class IonOutputFormatter : TextOutputFormatter
    {
        private readonly JsonOutputFormatter _jsonOutputFormatter;

        public IonOutputFormatter(JsonOutputFormatter jsonOutputFormatter)
        {
            _jsonOutputFormatter = jsonOutputFormatter ?? throw new ArgumentNullException(nameof(jsonOutputFormatter));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/ion+json"));
            SupportedEncodings.Add(Encoding.UTF8);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
            => _jsonOutputFormatter.WriteResponseBodyAsync(context, selectedEncoding);
    }
}
