using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Services.Common.Extensions
{
    /// <summary>
    /// Exception handling helpers
    /// </summary>
    public class ExceptionExtensions : Exception
    {
        /// <summary>
        /// Handel Aggregate exception.
        /// </summary>
        /// <param name="aex">The exception.</param>
        /// <returns>BadRequestObjectResult</returns>
        public static IActionResult HandleException(AggregateException aex)
        {
            Exception inner = aex.InnerExceptions.FirstOrDefault();
            return new BadRequestObjectResult(inner.Message);
        }

    }
}
