using AutoMapper;
using WebApi.Dtos.Link;
using WebApi.Models;

namespace WebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Link, GetLinkDto>();
            CreateMap<AddLinkDto, Link>();
        }
    }
}