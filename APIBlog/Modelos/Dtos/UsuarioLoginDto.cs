using System.ComponentModel.DataAnnotations;

namespace APIBlog.Modelos.Dtos
{
    public class UsuarioLoginDto
    { 
        [Required(ErrorMessage = "El nombre de usuario es obligatio")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "El password es obligatio")]
        public string Password { get; set; }
    }
}
