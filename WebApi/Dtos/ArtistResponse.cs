namespace WebApi.Dtos
{
    using AutoMapper;
    using Domain.Entities;
    using System;
    using WebApi.Common.Mapping;
    public class ArtistResponse: IMapFrom<Artist>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public string SmallThumbnail { get; set; }    


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Artist, ArtistResponse>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.ArtistName));
        }


    }
}
