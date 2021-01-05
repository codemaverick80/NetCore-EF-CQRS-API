﻿using AutoMapper;

namespace Application.Common.Mappings
{
    /*
     * All the querie's DTOs will implement this interface for entity mapping 
     */
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile);

       // void Mapping(Profile profile)=> profile.CreateMap(typeof(T), GetType());

    }
}
