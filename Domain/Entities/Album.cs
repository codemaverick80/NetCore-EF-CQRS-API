using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    /*
     * 
     * Album entity includes two Reference Navigation Properties (of Artist type & Genre type)
     * 
     * Artist entity includes a collection navigation property ICollection<Album>, which result in a
     * one-to-many relationship between Artist (one) and Album (many)
     *  
     * Genre entity includes a collection navigation property ICollection<Album>, which result in a
     * one-to-many relationship between Genre (one) and Album (many)
     * 
     */
    public class Album : AuditableEntity
    {

        public Album()
        {
            Tracks = new HashSet<Track>();
        }
        public Guid Id { get; set; }
        public string AlbumName { get; set; }
        public Guid ArtistId { get; set; }
        public Guid GenreId { get; set; }
        public int? Rating { get; set; }
        public int? Year { get; set; }
        public string? Label { get; set; }
        public string? ThumbnailTag { get; set; }
        public string? SmallThumbnail { get; set; }
        public string? MediumThumbnail { get; set; }
        public string? LargeThumbnail { get; set; }
        public string? AlbumUrl { get; set; }

        /*Reference Navigation Property*/
        public Artist Artist { get; set; }

        /*Reference Navigation Property*/
        public Genre Genre { get; set; }

        /*Collection Navigation property */
        public ICollection<Track> Tracks { get; private set; }
       
    }
}
