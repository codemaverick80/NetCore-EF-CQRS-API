using Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CQRS.Albums.Queries
{
  public  class AlbumResourceParameters : PagingParameters
   {
        public string Expression { get; set; }
   }
}
