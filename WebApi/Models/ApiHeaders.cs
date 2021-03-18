namespace WebApi.Models
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    public class ApiHeaders
    {
        /// <summary>
        /// Client Guid on request and Web api Guid on response.
        /// </summary>
        [Required]
        public string SenderMessageId { get; set; }
        /// <summary>
        /// Client application id on Request. Web api id on response.
        /// </summary>
        [Required]
        public string SenderApplicationId { get; set; }
        /// <summary>
        /// Client Host Name
        /// </summary>
        [Required]
        public string  SenderHostName { get; set; }
        /// <summary>
        /// Client will send timestamp to web api
        /// </summary>
        [Required]
        public DateTime CreationTimeStamp { get; set; }
        /// <summary>
        /// On request it will be empty but on response it will be the SenderMessageId
        /// </summary>
        public string SenderMessageIdEcho { get; set; }

        /// <summary>
        /// It will be client id on request and also on response.
        /// </summary>
        public string OriginationApplicationId { get; set; }
        public string TransactionId { get; set; }       

    }
}
