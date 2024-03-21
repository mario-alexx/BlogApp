using BlogClienteWasm.Helpers;
using BlogClienteWasm.Modelos;
using BlogClienteWasm.Servicios.IServicio;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BlogClienteWasm.Servicios
{
    public class PostServicio : IPostServicio
    {
        private readonly HttpClient _cliente;

        public PostServicio(HttpClient cliente)
        {
            _cliente = cliente;
        }

        public async Task<Post> ActualizarPost(int postId, Post post)
        {
            string content = JsonConvert.SerializeObject(post);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _cliente.PatchAsync($"{Inicializar.UrlBaseApi}api/posts/{postId}", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                string contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Post>(contentTemp);
                return result;
            }
            else
            {
                string contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<Post> CrearPost(Post post)
        {
            string content = JsonConvert.SerializeObject(post);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _cliente.PostAsync($"{Inicializar.UrlBaseApi}api/posts", bodyContent);
            
            if (response.IsSuccessStatusCode)
            {
                string contentTemp = await response.Content.ReadAsStringAsync();
                Post result = JsonConvert.DeserializeObject<Post>(contentTemp);
                return result;
            }
            else
            {
                string contentTemp = await response.Content.ReadAsStringAsync();
                ModeloError errorModel = JsonConvert.DeserializeObject<ModeloError>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<bool> EliminarPost(int postId)
        {
            HttpResponseMessage response = await _cliente.DeleteAsync($"{Inicializar.UrlBaseApi}api/posts/{postId}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                string content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<Post> GetPost(int postId)
        {
            HttpResponseMessage response = await _cliente.GetAsync($"{Inicializar.UrlBaseApi}api/posts/{postId}");
            
            if(response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Post post = JsonConvert.DeserializeObject<Post>(content);
                return post;
            }
            else
            {
                string content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }


        public async Task<IEnumerable<Post>> GetPosts()
        {
            HttpResponseMessage response = await _cliente.GetAsync($"{Inicializar.UrlBaseApi}api/posts");
            string content = await response.Content.ReadAsStringAsync();
            IEnumerable<Post> post = JsonConvert.DeserializeObject<IEnumerable<Post>>(content);
            return post;
        }

        public async Task<string> SubidaImagen(MultipartFormDataContent content)
        {
            HttpResponseMessage postResult = await _cliente.PostAsync($"{Inicializar.UrlBaseApi}api/upload", content);
            string postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                string imagenPost = Path.Combine($"{Inicializar.UrlBaseApi}", postContent);
                return imagenPost;
            }
        }
    }
}
