﻿using System.ComponentModel.DataAnnotations;

namespace BlogClienteWasm.Modelos
{
    public class UsuarioAutenticacion
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Password { get; set; }
    }
}
