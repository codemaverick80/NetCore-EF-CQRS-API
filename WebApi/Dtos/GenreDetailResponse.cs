namespace WebApi.Dtos
{
    using AutoMapper;
    using Domain.Entities;
    using System;
    using WebApi.Common.Mapping;

    public class GenreDetailResponse : IMapFrom<Genre>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Genre, GenreDetailResponse>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.GenreName));
        }
    }
}
