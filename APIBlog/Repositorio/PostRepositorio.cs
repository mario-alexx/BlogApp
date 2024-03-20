using APIBlog.Data;
using APIBlog.Modelos;
using APIBlog.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APIBlog.Repositorio
{
    public class PostRepositorio : IPostRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public PostRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarPost(Post post)
        {
            post.FechaActualizacion = DateTime.Now;
            // Asnotracking es para que no haga un seguimiento a la imagen y no genere un error
            var imagenBD = _bd.Post.AsNoTracking().FirstOrDefault(c => c.Id == post.Id);
            
            if(post.RutaImagen == null)
            {
                post.RutaImagen = imagenBD.RutaImagen;
            }
            _bd.Post.Update(post);
            return Guardar();
        }

        public bool BorrarPost(Post post)
        {
            _bd.Post.Remove(post);
            return Guardar();
        }

        public bool CrearPost(Post post)
        {
            post.FechaCreacion = DateTime.Now;
            _bd.Post.Add(post);
            return Guardar();
        }

        public bool ExistePost(string nombre)
        {
            bool valor = _bd.Post.Any(c => c.Titulo.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        } 

        public bool ExistePost(int id)
        {
            return _bd.Post.Any(c => c.Id == id);
        }

        public Post GetPost(int postId)
        {
            return _bd.Post.FirstOrDefault(c => c.Id == postId);
        }

        public ICollection<Post> GetPosts()
        {
            return _bd.Post.OrderBy(c => c.Id).ToList();

        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
