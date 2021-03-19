namespace WebApi.Dtos
{
    using AutoMapper;
    using Domain.Entities;
    using System;
    using WebApi.Common.Mapping;
    public class AlbumResponse : IMapFrom<Album>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Label { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Album, AlbumResponse>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.AlbumName));
        }
    }
}
