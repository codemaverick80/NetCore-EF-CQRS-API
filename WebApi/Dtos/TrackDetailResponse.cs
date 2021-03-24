namespace WebApi.Dtos
{
    using AutoMapper;
    using Domain.Entities;
    using WebApi.Common.Mapping;
    public class TrackDetailResponse : IMapFrom<Track>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AlbumId { get; set; }
        public string Composer { get; set; }
        public string Performer { get; set; }
        public string Featuring { get; set; }
        public string Duration { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Track, TrackDetailResponse>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.TrackName));
        }

    }
}
