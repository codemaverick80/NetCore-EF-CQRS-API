namespace WebApi.Services
{
    using Application.Common.Interfaces;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    /*
     * MusicApi datbase System User Info
     * Name: System
     * Id: 7CF50BCB-1070-44BB-8E16-2175A6F39CC3
     */
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            //UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            //IsAuthenticated = UserId != null;


            //TODO : For demo purpose we are hard coding, 
            //      if we had proper auth system in place the we would use the above commented code
            UserId = "7cf50bcb-1070-44bb-8e16-2175a6f39cc3";
            IsAuthenticated = true;
        }
        public string UserId { get; }

        public bool IsAuthenticated { get; }
    }
}
