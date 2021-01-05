﻿using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Artist: AuditableEntity
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        public Guid Id { get; set; }
        public string ArtistName { get; set; }
        public string YearActive { get; set; }
        public string Biography { get; set; }
        public string ThumbnailTag { get; set; }
        public string SmallThumbnail { get; set; }
        public string LargeThumbnail { get; set; }

        public ArtistBasicInfo ArtistBasicInfo { get; set; }
        public ICollection<Album> Albums { get; private set; }

    }
}
