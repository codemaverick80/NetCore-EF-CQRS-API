using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using WebApi.Models;

namespace WebApi.Common.Helpers
{
    public class HeaderUtility
    {
        public ApiHeaders SetResponseHeaders(ApiHeaders requestHeader)
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
