using APIBlog.Data;
using APIBlog.Modelos;
using APIBlog.Modelos.Dtos;
using APIBlog.Repositorio.IRepositorio;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace APIBlog.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _bd;
        private string claveSecreta;

        public UsuarioRepositorio(ApplicationDbContext bd, IConfiguration config)
        {
            _bd = bd;
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
        }

        public Usuario GetUsuario(int usuarioId)
        {
           return _bd.Usuario.FirstOrDefault(u => u.Id == usuarioId);
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _bd.Usuario.OrderBy(u => u.Id).ToList();
        }

        public bool IsUniqueUser(string usuario)
        {
            var usuarioId = _bd.Usuario.FirstOrDefault(u => u.NombreUsuario == usuario);
            if(usuarioId == null)
            {
                return true;
            }
            return false;
        }

        public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var passwordEncriptado = obtenerMd5(usuarioLoginDto.Password);
            var usuario = _bd.Usuario.FirstOrDefault(
                u => u.NombreUsuario.ToLower() == usuarioLoginDto.NombreUsuario.ToLower()
                && u.Password == passwordEncriptado
                );
            //Valida si el usuario no existe
            if (usuario == null) 
            {
                return new UsuarioLoginRespuestaDto()
                {
                    Token = "",
                    Usuario = null,
                };
            }

            // Existe el usuario
            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejadorToken.CreateToken(tokenDescriptor);

            var usuarioLoginRespuestaDto = new UsuarioLoginRespuestaDto()
            {
                Token = manejadorToken.WriteToken(token),
                Usuario = usuario
            };

            return usuarioLoginRespuestaDto;
        }

        public async Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto)
        {
            var passwordEncriptado = obtenerMd5(usuarioRegistroDto.Password);

            Usuario usuario = new()
            {
                NombreUsuario = usuarioRegistroDto.NombreUsuario,
                Nombre = usuarioRegistroDto.Nombre,
                Email = usuarioRegistroDto.Email,
                Password = usuarioRegistroDto.Password
            };

            _bd.Usuario.Add(usuario);
            usuario.Password = passwordEncriptado;
            await _bd.SaveChangesAsync();
            return usuario;
        }

        //Metodo para encriptar el password con MD5
        public static string obtenerMd5(string valor)
        {
            MD5CryptoServiceProvider x = new();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2".ToLower());

            return resp;
        }
    }
}
