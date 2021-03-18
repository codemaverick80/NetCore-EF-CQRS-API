namespace Application.Common.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    [Serializable]
    [ExcludeFromCodeCoverage]
    public abstract class ApplicationBaseException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }
        public abstract string Reason { get; }
        protected ApplicationBaseException() : this(string.Empty) { }
        protected ApplicationBaseException(string message) : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }
        protected ApplicationBaseException(string message, Exception ex) : base(message, ex) { }
        protected ApplicationBaseException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    }
}
