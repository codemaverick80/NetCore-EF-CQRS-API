namespace WebApi.Dtos
{
    using AutoMapper;
    using Domain.Entities;
    using System;
    using WebApi.Common.Mapping;

    public class AlbumDetailResponse:IMapFrom<Album>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }       
        public int Rating { get; set; }
        public int Year { get; set; }
        public string Label { get; set; }       
        public string SmallThumbnail { get; set; }
        public string MediumThumbnail { get; set; }
        public string LargeThumbnail { get; set; }
        public string AlbumUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Album, AlbumDetailResponse>()
                 .ForMember(d => d.Name, opt => opt.MapFrom(s => s.AlbumName));
        }
    }
}
