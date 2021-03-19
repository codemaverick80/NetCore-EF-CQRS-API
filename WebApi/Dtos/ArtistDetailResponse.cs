namespace WebApi.Dtos
{
    using AutoMapper;
    using Domain.Entities;
    using System;
    using WebApi.Common.Mapping;
    public class ArtistDetailResponse : IMapFrom<Artist>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string YearActive { get; set; }
        public string Biography { get; set; }
        public string ThumbnailTag { get; set; }
        public string SmallThumbnail { get; set; }
        public string LargeThumbnail { get; set; }
        public string Born { get; set; }
        public string Died { get; set; }
        public string AlsoKnownAs { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Artist, ArtistDetailResponse>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.ArtistName))
                .ForMember(d => d.Born, opt => opt.MapFrom(s => s.ArtistBasicInfo.Born))
                .ForMember(d => d.Died, opt => opt.MapFrom(s => s.ArtistBasicInfo.Died))
                .ForMember(d=>d.AlsoKnownAs,opt=>opt.MapFrom(s=>s.ArtistBasicInfo.AlsoKnownAs));
        }
    }
}
