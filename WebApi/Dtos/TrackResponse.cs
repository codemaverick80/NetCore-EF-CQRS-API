namespace WebApi.Dtos
{
    using AutoMapper;
    using Domain.Entities;
    using System;
    using WebApi.Common.Mapping;

    public class TrackResponse : IMapFrom<Track>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }        

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Track, TrackResponse>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.TrackName));
        }
    }
}
