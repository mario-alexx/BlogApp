using System.ComponentModel.DataAnnotations;

namespace APIBlog.Modelos.Dtos
{
    public class UsuarioRegistroDto
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatio")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "El nombre es obligatio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El email es obligatio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El password es obligatio")]
        public string Password { get; set; }
    }
}
