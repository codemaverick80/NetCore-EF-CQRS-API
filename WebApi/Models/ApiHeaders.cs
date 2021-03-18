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

        public ApiHeaders ResponseHeaders(ApiHeaders requestHeader)
        {
            ApiHeaders headers = new ApiHeaders()
            {
                CreationTimeStamp = requestHeader.CreationTimeStamp,
                SenderMessageIdEcho = requestHeader.SenderMessageId,
                SenderHostName = requestHeader.SenderHostName,
                TransactionId = requestHeader.TransactionId,
                OriginationApplicationId = requestHeader.OriginationApplicationId,
                SenderMessageId = Guid.NewGuid().ToString(),
                SenderApplicationId = "Music-API",
            };
            return headers;
        }


        public static ApiHeaders GetHeaders(IHeaderDictionary headers)
        {
            ApiHeaders header = new ApiHeaders();

            foreach (var item in headers)
            {
                if (item.Key.StartsWith("API-"))
                {
                    switch (item.Key)
                    {
                        case "API-SenderMessageId":
                            header.SenderMessageId = item.Value.FirstOrDefault();
                            break;
                        case "API-SenderApplicationId":
                            header.SenderApplicationId = item.Value.FirstOrDefault();
                            break;
                        case "API-SenderHostName":
                            header.SenderHostName = item.Value.FirstOrDefault();
                            break;
                        case "API-CreationTimeStamp":
                            DateTime dt;
                            if (DateTime.TryParse(item.Value.FirstOrDefault(), out dt))
                                header.CreationTimeStamp = dt;
                            break;
                        case "API-SenderMessageIdEcho":
                            header.SenderMessageIdEcho = item.Value.FirstOrDefault();
                            break;
                        case "API-OriginationApplicationId":
                            header.OriginationApplicationId = item.Value.FirstOrDefault();
                            break;
                        case "API-TransactionId":
                            header.TransactionId = item.Value.FirstOrDefault();
                            break;
                    }
                }

            }
            return header;
        }


    }
}
