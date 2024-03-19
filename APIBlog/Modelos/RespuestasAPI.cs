using System.Net;

namespace APIBlog.Modelos
{
    public class RespuestasAPI
    {
        public RespuestasAPI()
        {
            ErrorMessage = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } 
        public List<string> ErrorMessage { get; set; }
        public object Result { get; set; }
    }
}
