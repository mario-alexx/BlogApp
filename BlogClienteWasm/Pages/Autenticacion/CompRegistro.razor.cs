using BlogClienteWasm.Modelos;
using BlogClienteWasm.Servicios.IServicio;
using Microsoft.AspNetCore.Components;

namespace BlogClienteWasm.Pages.Autenticacion
{
    public partial class CompRegistro
    {
        private UsuarioRegistro UsuarioParaRegistro = new();
        public bool EstaProcesando { get; set; } = false;
        public bool MostrarErroresRegistro { get; set; }

        public IEnumerable<string> Errores {  get; set; }

        [Inject]
        public IServicioAutenticacion servicioAutenticacion { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }    

        private async Task RegistrarUsuario()
        {
            MostrarErroresRegistro = false;
            EstaProcesando = true;
            var result = await servicioAutenticacion.SignIn(UsuarioParaRegistro);

            if (result.RegistroCorrecto)
            {
                EstaProcesando = false;
                navigationManager.NavigateTo("/acceder");
            }
            else
            {
                EstaProcesando = false;
                Errores = result.Errores;
                MostrarErroresRegistro = true;
            }   
        }
    
    }
}
