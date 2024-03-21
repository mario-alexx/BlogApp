using BlogClienteWasm.Modelos;

namespace BlogClienteWasm.Servicios.IServicio
{
    public interface IServicioAutenticacion
    {
        Task<RespuestaRegistro> SignIn(UsuarioRegistro usuarioRegistro);
        Task<RespuestaAutenticacion> Login(UsuarioAutenticacion usuarioAutenticacion);
        Task Salir();
    }
}
