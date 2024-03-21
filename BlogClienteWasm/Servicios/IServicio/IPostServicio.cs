using BlogClienteWasm.Modelos;

namespace BlogClienteWasm.Servicios.IServicio
{
    public interface IPostServicio
    {
        public Task<IEnumerable<Post>> GetPosts();
        public Task<Post> GetPost(int postId);
        public Task<Post> CrearPost(Post post);
        public Task<Post> ActualizarPost(int postId, Post post);
        public Task<bool> EliminarPost(int postId);
        public Task<string> SubidaImagen(MultipartFormDataContent content);
    }
}
