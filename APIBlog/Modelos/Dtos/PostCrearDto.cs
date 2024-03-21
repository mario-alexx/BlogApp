﻿using System.ComponentModel.DataAnnotations;

namespace APIBlog.Modelos.Dtos
{
    public class PostCrearDto
    {
        [Required(ErrorMessage = "El título es obligatorio")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "La descripción es obligatorio")]
        public string Descripcion { get; set; }
        public string RutaImagen { get; set; }
        [Required(ErrorMessage = "Las etiquetas son obligatorios")]
        public string Etiqueta { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
