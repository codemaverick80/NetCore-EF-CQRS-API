using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Exceptions
{
    public class DeleteFailureException : ApplicationBaseException
    {

        public override string Reason => "DeleteFailureException";
        // public BadRequestException():base() {}
        public DeleteFailureException(string message) : base(message) { }
        public DeleteFailureException(string message, Exception ex) : base(message, ex) { }

        public DeleteFailureException(string message, string name, object key)
            : base($"Deletion of entity \"{name}\" ({key}) failed. {message}") { }
        public DeleteFailureException(string message, List<ValidationFailure> failures) : this(message)
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
