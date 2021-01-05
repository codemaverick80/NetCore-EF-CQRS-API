using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Common.Exceptions
{
    public class RequestValidationException : ApplicationBaseException
    {       
        public override string Reason => "ValidationException";

        public RequestValidationException(string message) : base(message) {}
        public RequestValidationException(string message, Exception ex) : base(message, ex) { }
        public RequestValidationException(string message,List<ValidationFailure> failures) : this(message)
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Errors.Add(propertyName, propertyFailures);
            }
        }

        //public RequestValidationException() : base()
        //{
        //    Failures = new Dictionary<string, string[]>();
        //}

        //public RequestValidationException(List<ValidationFailure> failures): this()
        //{
        //    var propertyNames = failures
        //        .Select(e => e.PropertyName)
        //        .Distinct();

        //    foreach (var propertyName in propertyNames)
        //    {
        //        var propertyFailures = failures
        //            .Where(e => e.PropertyName == propertyName)
        //            .Select(e => e.ErrorMessage)
        //            .ToArray();

        //        Failures.Add(propertyName, propertyFailures);
        //    }
        //}

        //public IDictionary<string, string[]> Failures { get; }


    }
}
