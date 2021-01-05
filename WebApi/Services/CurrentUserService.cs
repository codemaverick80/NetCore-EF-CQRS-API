using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            //UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            //IsAuthenticated = UserId != null;

            UserId = "7cf50bcb-1070-44bb-8e16-2175a6f39cc3";
            IsAuthenticated = true;
        }
        public string UserId  {get;}

        public bool IsAuthenticated { get; }
    }
}
