using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.Application.Common.exceptions
{
    public class CatalogValidationException : Exception
    {
        public CatalogValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public CatalogValidationException(string propertyName, string[] errors)
           : this()
        {
            Errors.Add(propertyName, errors);
        }

        public CatalogValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
