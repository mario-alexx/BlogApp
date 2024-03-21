using APIBlog.Modelos;
using APIBlog.Modelos.Dtos;
using AutoMapper;

namespace APIBlog.Mappers
{
    public class BlogMapper : Profile
    {
        public BlogMapper()
        {
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Post, PostCrearDto>().ReverseMap();
            CreateMap<Post, PostActualizarDto>().ReverseMap();

            //CreateMap<Usuario, UsuarioDto>().ReverseMap();
            //CreateMap<Usuario, UsuarioLoginDto>().ReverseMap();
            //CreateMap<Usuario, UsuarioLoginRespuestaDto>().ReverseMap();
            //CreateMap<Usuario, UsuarioRegistroDto>().ReverseMap();
        }
    }
}
