namespace Application.CQRS.Albums.Queries
{
    using Application.Common;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class AlbumResourceParameters : PagingParameters
    {
        public string Expression { get; set; }
    }
}
