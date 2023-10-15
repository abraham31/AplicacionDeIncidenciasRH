using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Data;
using System.Net;
using System.Security.Claims;
using WEB_API.Dtos;
using WEB_API.Helpers;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudPermisosController : ControllerBase
    {
        private readonly IHubContext<NotHub> _hubContext;
        private readonly ILogger<SolicitudPermisosController> _logger;
        private readonly ISolicitudPermisoRepository _solicitudPermisoRepo;
        private readonly IMapper _mapper;
        protected ApiResponse _response;

        public SolicitudPermisosController(ILogger<SolicitudPermisosController> logger, 
            ISolicitudPermisoRepository solicitudPermisoRepo, IMapper mapper, IHubContext<NotHub> hubContext)
        {
            _logger = logger;
            _solicitudPermisoRepo = solicitudPermisoRepo;
            _hubContext = hubContext;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetSolicitudPerimsos()
        {
            try
            {
                _logger.LogInformation("Obtener las solicitudes de permisos");
                IEnumerable<SolicitudPermiso> solicitudPermisosList = await _solicitudPermisoRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<SolicitudPermisosDto>>(solicitudPermisosList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("Id:int", Name = "GetSolicitudPermiso")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetSolicitudPermiso(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer la solicitud con Id" + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                var solicitudpermiso = await _solicitudPermisoRepo.Obtener(v => v.Id == id);

                if (solicitudpermiso == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = !false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<SolicitudPermisosDto>(solicitudpermiso);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CrearSolicitudPermiso([FromBody] SolicitudPermisosDto solicitudPermisoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (solicitudPermisoDto == null)
                {
                    return BadRequest(solicitudPermisoDto);
                }
                var userIdString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                SolicitudPermiso modelo = _mapper.Map<SolicitudPermiso>(solicitudPermisoDto);

                if (string.IsNullOrEmpty(userIdString))
                {
                    return BadRequest("Error al obtener el UserId del contexto actual.");
                }

                modelo.UserId = userIdString;
                modelo.FechaDeCreacion = DateTime.Now;
                modelo.FechaDeModificacion = DateTime.Now;
                modelo.Estado = EstadoIncidencia.Pendinte;
                await _solicitudPermisoRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                try
                {
                    // Intenta enviar la notificación
                    await _hubContext.Clients.User(userIdString).SendAsync("ReceiveNotification", "Nuevo solicitud de permiso creada");
                }
                catch (Exception ex)
                {
                    // Si ocurre un error al enviar la notificación, agrega un mensaje de error específico
                    _response.ErrorMessages = new List<string>() { "Error al enviar la notificación: " + ex.Message };
                    // Puedes configurar un código de estado apropiado, como 500 (Error interno del servidor)
                    return StatusCode(StatusCodes.Status500InternalServerError, _response);
                }

                return CreatedAtRoute("GetSolicitudPermiso", new { id = modelo.Id }, _response);

            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeleteSolicitudPermiso(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var permiso = await _solicitudPermisoRepo.Obtener(v => v.Id == id);
                if (permiso == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _solicitudPermisoRepo.Remover(permiso);

                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSolicitudPermiso(int id, [FromBody] SolicitudPermisosDto solicitudPermisoUpdateDto)
        {
            if (solicitudPermisoUpdateDto == null || id != solicitudPermisoUpdateDto.Id)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            SolicitudPermiso modelo = _mapper.Map<SolicitudPermiso>(solicitudPermisoUpdateDto);


            await _solicitudPermisoRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }

    }
}
