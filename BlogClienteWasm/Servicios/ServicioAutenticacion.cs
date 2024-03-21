using Blazored.LocalStorage;
using BlogClienteWasm.Helpers;
using BlogClienteWasm.Modelos;
using BlogClienteWasm.Servicios.IServicio;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;

namespace BlogClienteWasm.Servicios
{
    public class ServicioAutenticacion : IServicioAutenticacion
    {
        private readonly HttpClient _cliente;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public ServicioAutenticacion(HttpClient cliente, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _cliente = cliente;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<RespuestaAutenticacion> Login(UsuarioAutenticacion usuarioAutenticacion)
        {
            var content = JsonConvert.SerializeObject(usuarioAutenticacion);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _cliente.PostAsync($"{Inicializar.UrlBaseApi}api/usuarios/login", bodyContent);

            var contentTemp = await response.Content.ReadAsStringAsync();
            var resultado = (JObject)JsonConvert.DeserializeObject(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                var Token = resultado["result"]["token"].Value<string>();
                var Usuario = resultado["result"]["usuario"]["nombreUsuario"].Value<string>();

                await _localStorageService.SetItemAsync(Inicializar.Token_Local, Token);
                await _localStorageService.SetItemAsync(Inicializar.Datos_Usuario_Local, Usuario);
                ((AuthStateProvider)_authenticationStateProvider).NotificarUsuarioLogin(Token);

                _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);
                return new RespuestaAutenticacion { IsSuccess = true };
            }
            else
            {
                return new RespuestaAutenticacion { IsSuccess = false };
            }

        }

        public async Task Salir()
        {
            await _localStorageService.RemoveItemAsync(Inicializar.Token_Local);
            await _localStorageService.RemoveItemAsync(Inicializar.Datos_Usuario_Local);

            ((AuthStateProvider)_authenticationStateProvider).NotificarUsuarioSalir();
            _cliente.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RespuestaRegistro> SignIn(UsuarioRegistro usuarioRegistro)
        {
            string content = JsonConvert.SerializeObject(usuarioRegistro);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _cliente.PostAsync($"{Inicializar.UrlBaseApi}api/usuarios/registro", bodyContent);

            string contentTemp = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<RespuestaRegistro>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new RespuestaRegistro { RegistroCorrecto = true };
            }
            else
            {
                return resultado;
            }
        }
    }
}
