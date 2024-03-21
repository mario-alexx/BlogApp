namespace BlogClienteWasm.Modelos
{
    public class RespuestaAutenticacion
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public IEnumerable<string> Errores { get; set; }
        public Usuario Usuario { get; set; }
    }
}
