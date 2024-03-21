using BlogClienteWasm.Servicios.IServicio;
using Microsoft.AspNetCore.Components;

namespace BlogClienteWasm.Pages.Autenticacion
{
    public partial class CompSalir
    {
        [Inject]
        public IServicioAutenticacion servicioAutenticacion { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await servicioAutenticacion.Salir();
            navigationManager.NavigateTo("/");
        }


    }
}
