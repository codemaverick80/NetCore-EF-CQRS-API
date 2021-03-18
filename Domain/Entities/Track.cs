namespace Domain.Entities
{
    using Domain.Common;
    using System;
    public class Track : AuditableEntity
    {
        public Guid Id { get; set; }
        public string TrackName { get; set; }
        public Guid AlbumId { get; set; }
        public string Composer { get; set; }
        public string Performer { get; set; }
        public string Featuring { get; set; }
        public string Duration { get; set; }

        public Album Album { get; set; }


    }
}
