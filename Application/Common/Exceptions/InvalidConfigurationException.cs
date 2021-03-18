namespace Application.Common.Exceptions
{
    using FluentValidation.Results;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class InvalidConfigurationException : ApplicationBaseException
    {
        public override string Reason => "InvalidConfigurationException";
        // public BadRequestException():base() {}
        public InvalidConfigurationException(string message) : base(message) { }
        public InvalidConfigurationException(string message, Exception ex) : base(message, ex) { }

        public InvalidConfigurationException(string message, List<ValidationFailure> failures) : this(message)
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
