using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Application.Common.exceptions
{
    public class OrderValidationException : Exception
    {
        public OrderValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public OrderValidationException(string propertyName, string[] errors)
           : this()
        {
            Errors.Add(propertyName, errors);
        }

        public OrderValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
