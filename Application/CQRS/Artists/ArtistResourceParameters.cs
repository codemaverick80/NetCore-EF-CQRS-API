using Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CQRS.Artists
{
   public class ArtistResourceParameters : PagingParameters
   {
        public string Filter { get; set; }

   }
}
