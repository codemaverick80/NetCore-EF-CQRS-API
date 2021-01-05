using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class ArtistBasicInfo: AuditableEntity
    {
        public Guid ArtistId { get; set; }
        public string Born { get; set; }
        public string Died { get; set; }
        public string AlsoKnownAs { get; set; }

        public Artist Artist { get; set; }

    }
}
