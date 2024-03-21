using BlogClienteWasm.Modelos;
using BlogClienteWasm.Servicios.IServicio;
using Microsoft.AspNetCore.Components;
using System.Web;

namespace BlogClienteWasm.Pages.Autenticacion
{
    public partial class CompAcceder
    {
        private UsuarioAutenticacion usuarioAutenticacion = new UsuarioAutenticacion();
        public bool EstaProcesando { get; set; } = false;
        public bool MostrarErroresAutenticacion { get; set; }
        public string UrlRetorno { get; set; }

        public string Errores { get; set; }

        [Inject]
        public IServicioAutenticacion servicioAutenticacion { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        private async Task AccesoUsuario()
        {
            MostrarErroresAutenticacion = false;
            EstaProcesando = true;
            var result = await servicioAutenticacion.Login(usuarioAutenticacion);

            if (result.IsSuccess)
            {
                EstaProcesando = false;
                var urlAbsoluta = new Uri(navigationManager.Uri);
                var parametrosQuery = HttpUtility.ParseQueryString(urlAbsoluta.Query);
                UrlRetorno = parametrosQuery["returnUrl"];

                if (string.IsNullOrEmpty(UrlRetorno))
                {
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    navigationManager.NavigateTo("/" + UrlRetorno);
                }
            }
            else
            {
                EstaProcesando = false;
                MostrarErroresAutenticacion = true;
                Errores = "El usuario o el password no son validos";
                navigationManager.NavigateTo("/acceder");
            }
        }
    }
}
