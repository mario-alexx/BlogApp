using APIBlog.Modelos;
using APIBlog.Modelos.Dtos;
using AutoMapper;

namespace APIBlog.Mappers
{
    public class BlogMapper : Profile
    {
        protected BlogMapper()
        {
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Post, PostCrearDto>().ReverseMap();
            CreateMap<Post, PostActualizarDto>().ReverseMap();
        }
    }
}
