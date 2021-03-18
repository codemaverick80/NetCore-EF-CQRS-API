namespace WebApi.Models
{
    using System.Collections.Generic;
    public class ApiErrorResponse
    {
        public List<SystemError> SystemError { get; set; }

    }

    public class SystemError
    {
        public string CreatorApplicatioId { get; set; }
        public string CreatorSubApplicationId { get; set; }
        public string Code { get; set; }
        public string SubCode { get; set; }
        public string Message { get; set; }
        public string Cause { get; set; }

        public KeyValuePair<string, string> Disgnostic { get; set; }
    }

}
