using Core.Entities.Auth;
using Core.Entities.Auth.AuthDto;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_API.Dtos;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<SolicitudPermisosController> _logger;
        private readonly IUserRepository _userRepo;
        private ApiResponse _response;

        public UserController(IUserRepository userRepo, ILogger<SolicitudPermisosController> logger)
        {
            _userRepo = userRepo;
            _response = new();
            _logger = logger;
        }

        [HttpPost("login")]   // /api/usuario/login
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO modelo)
        {
            var loginResponse = await _userRepo.Login(modelo);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("UserName o Password son Incorrectos");
                return BadRequest(_response);
            }
            _response.IsExitoso = true;
            _response.statusCode = HttpStatusCode.OK;
            _response.Resultado = loginResponse;
            return Ok(_response);
        }

        [HttpPost("registrar")]   // /api/usuario/registrar
        public async Task<IActionResult> Registrar([FromBody] RegistroRequestDTO modelo)
        {
           
                bool isUserUnique = _userRepo.IsUsuarioUnico(modelo.UserName);

            var user = new UserApplication
            {
                UserName = modelo.UserName,
                Email = modelo.UserName,
                NormalizedEmail = modelo.UserName.ToUpper(),
                Names = modelo.Names,
            };

            var validationResult = await _userRepo.ValidatePasswordAsync(user, modelo.Password);

            if (!isUserUnique)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    _response.ErrorMessages.Add("Usuario ya Existe!");
                    return BadRequest(_response);
                }
                
                if (!validationResult.Succeeded)
                {
                
                var erroresValidacion = validationResult.Errors.Select(error => error.Description);

                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    _response.ErrorMessages.Add("La contraseña debe contener al menos un carácter no alfanumérico y 9 caracteres de largo.");
                    return BadRequest(_response);
                }

            var usuario = await _userRepo.Registrar(modelo);
                if (usuario == null)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    _response.ErrorMessages.Add("Error al registrar Usuario!");
                    return BadRequest(_response);
                }

                _response.statusCode = HttpStatusCode.OK;
                _response.IsExitoso = true;
                return Ok(_response);

        }
            
            
    }
}
