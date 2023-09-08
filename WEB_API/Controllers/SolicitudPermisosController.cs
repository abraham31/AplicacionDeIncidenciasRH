using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_API.Dtos;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudPermisosController : ControllerBase
    {
        private readonly ILogger<SolicitudPermisosController> _logger;
        private readonly ISolicitudPermisoRepository _solicitudPermisoRepo;
        private readonly IMapper _mapper;
        protected ApiResponse _response;

        public SolicitudPermisosController(ILogger<SolicitudPermisosController> logger, ISolicitudPermisoRepository solicitudPermisoRepo, IMapper mapper)
        {
            _logger = logger;
            _solicitudPermisoRepo = solicitudPermisoRepo;
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

        [HttpGet("Id:int", Name = "GetSolicitadPermiso")]
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
                SolicitudPermiso modelo = _mapper.Map<SolicitudPermiso>(solicitudPermisoDto);

                modelo.FechaDeCreacion = DateTime.Now;
                modelo.FechaDeModificacion = DateTime.Now;
                await _solicitudPermisoRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

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
