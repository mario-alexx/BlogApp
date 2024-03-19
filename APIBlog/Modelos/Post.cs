using System.ComponentModel.DataAnnotations;

namespace APIBlog.Modelos
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public string? RutaImagen { get; set; }
        [Required]
        public string Etiqueta { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaActualizacion { get; set; }
    }
}
