using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Exceptions
{
    public class NotFoundException : ApplicationBaseException
    { 
        public override string Reason => "NotFoundException";

        // public NotFoundException():base() {} 
        public NotFoundException(string name, object key) : base($"No record found for the id {key}") { }
        public NotFoundException(string message, Exception ex):base(message,ex){}
    }
}
