using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Common.Exceptions
{
    public class BadRequestException: ApplicationBaseException
    {

        public override string Reason => "BadRequestException";
        // public BadRequestException():base() {}
        public BadRequestException(string message):base(message)  { }
        public BadRequestException(string message, Exception ex):base(message,ex) { }

        public BadRequestException(string message, List<ValidationFailure> failures) : this(message)
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

       
    }
}
