namespace WebApi.Common.Mapping
{
using AutoMapper;
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}
