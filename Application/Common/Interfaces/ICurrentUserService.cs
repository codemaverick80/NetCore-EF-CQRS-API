namespace Application.Common.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface ICurrentUserService
    {
        string UserId { get; }
        bool IsAuthenticated { get; }
    }
}
