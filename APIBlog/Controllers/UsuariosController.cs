using APIBlog.Modelos;
using APIBlog.Modelos.Dtos;
using APIBlog.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APIBlog.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usRepo;
        protected RespuestasAPI _respuestaApi;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepositorio usuarioRepositorio, IMapper mapper)
        {
            _usRepo = usuarioRepositorio;
            _mapper = mapper;
            _respuestaApi = new();
        }

        [HttpPost("registro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registro([FromBody] UsuarioRegistroDto usuarioRegistroDto)
        {
            bool validarNombreUsuarioUnico = _usRepo.IsUniqueUser(usuarioRegistroDto.NombreUsuario);

            if (!validarNombreUsuarioUnico)
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessage.Add("El nombre de usuario ya existe");
                return BadRequest(_respuestaApi);
            }

            var usuario = await _usRepo.Registro(usuarioRegistroDto);
            if (usuario == null) 
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessage.Add("Error en el registro");
                return BadRequest(_respuestaApi);
            }

            _respuestaApi.StatusCode = HttpStatusCode.OK;
            _respuestaApi.IsSuccess = true;
            return Ok(_respuestaApi);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            var respuestaLogin = await _usRepo.Login(usuarioLoginDto);
            if (respuestaLogin.Usuario == null || string.IsNullOrEmpty(respuestaLogin.Token))
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessage.Add("El nombre de usuario o password es incorrecto");
                return BadRequest(_respuestaApi);
            }

            _respuestaApi.StatusCode = HttpStatusCode.OK;
            _respuestaApi.IsSuccess = true;
            _respuestaApi.Result = respuestaLogin;
            return Ok(_respuestaApi);
        }


        // Endpoint's para usuarios (Obtener lista de usuarios o usuario por id)

        //[Authorize]
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public IActionResult GetUsuarios()
        //{
        //    var listaUsuarios = _usRepo.GetUsuarios();

        //    var listaUsuariosDto = new List<UsuarioDto>();

        //    foreach (var lista in listaUsuarios)
        //    {
        //        listaUsuariosDto.Add(_mapper.Map<UsuarioDto>(lista));
        //    }
        //    return Ok(listaUsuariosDto);
        //}

        //[Authorize]
        //[HttpGet("{usuarioId:int}", Name ="GetUsuario")]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult GetUsuario(int usuarioId)
        //{
        //    var itemUsuario = _usRepo.GetUsuario(usuarioId);

        //    if (itemUsuario == null)
        //        return NotFound();

        //    var itemUsuarioDto = _mapper.Map<UsuarioDto>(itemUsuario);
        //    return Ok(itemUsuarioDto);
        //}
    }
}
