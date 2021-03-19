namespace WebApi.Dtos
{    
    using AutoMapper;
    using Domain.Entities;
    using WebApi.Common.Mapping;

    public class GenreGetResponse : IMapFrom<Genre>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Genre, GenreGetResponse>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.GenreName));
        }
    }

}
