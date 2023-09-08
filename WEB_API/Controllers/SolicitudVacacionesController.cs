using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_API.Dtos;
using System.Net;
using Core.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure;


namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudVacacionesController : ControllerBase
    {
        private readonly ILogger<SolicitudVacacionesController> _logger;
        private readonly ISolicitudVacacionesRepository _solicitudVacacionesRepo;
        private readonly IMapper _mapper;
        protected ApiResponse _response;

        public SolicitudVacacionesController(ILogger<SolicitudVacacionesController> logger, ISolicitudVacacionesRepository solicitudVacacionesRepo, IMapper mapper)
        {
            _logger = logger;
            _solicitudVacacionesRepo = solicitudVacacionesRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetSolicitudVacaciones()
        {
            try
            {
                _logger.LogInformation("Obtener las solicitudes de vacaciones");
                IEnumerable<SolicitudVacaciones> solicitudVacacionesList = await _solicitudVacacionesRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<SolicitudVacacionesDto>>(solicitudVacacionesList);
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

        [HttpGet("Id:int", Name = "GetSolicitadVacacion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetSolicitudVacacion(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer la solicitud con Id" + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso=false;
                    return BadRequest(_response);
                }
                var solicitudvacaciones = await _solicitudVacacionesRepo.Obtener(v => v.Id == id);

                if (solicitudvacaciones == null)
                {
                    _response.statusCode=HttpStatusCode.NotFound;
                    _response.IsExitoso=!false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<SolicitudVacacionesDto>(solicitudvacaciones);
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
        public async Task<ActionResult<ApiResponse>> CrearSolicitudVacaciones([FromBody] SolicitudVacacionesDto solicitudVacacionesDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (solicitudVacacionesDto == null)
                {
                    return BadRequest(solicitudVacacionesDto);
                }
                SolicitudVacaciones modelo = _mapper.Map<SolicitudVacaciones>(solicitudVacacionesDto);

                modelo.FechaDeCreacion = DateTime.Now;
                modelo.FechaDeModificacion = DateTime.Now;
                await _solicitudVacacionesRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetSolicitudVacacion", new { id = modelo.Id }, _response);

            } 
            catch(Exception ex) 
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
        public async Task<ActionResult<ApiResponse>> DeleteSolicitudVacaciones(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villa = await _solicitudVacacionesRepo.Obtener(v => v.Id == id);
                if (villa == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _solicitudVacacionesRepo.Remover(villa);

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
        public async Task<IActionResult> UpdateSolicitudVacaciones(int id, [FromBody] SolicitudVacacionesDto solicitudVacacionesUpdateDto)
        {
            if (solicitudVacacionesUpdateDto == null || id != solicitudVacacionesUpdateDto.Id)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            SolicitudVacaciones modelo = _mapper.Map<SolicitudVacaciones>(solicitudVacacionesUpdateDto);


            await _solicitudVacacionesRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }
    }
}
