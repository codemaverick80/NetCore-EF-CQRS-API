namespace WebApi.Common.Mapping
{
    using AutoMapper;
    /*
     * All the querie's DTOs will implement this interface for entity mapping 
     */
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }  
}
