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
    public class EstadisticaController : ControllerBase
    {
        private readonly ILogger<EstadisticaController> _logger;
        private readonly IMapper _mapper;
        protected ApiResponse _response;

        public EstadisticaController(ILogger<EstadisticaController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetEstadistica()
        {
            try
            {
                _logger.LogInformation("Obtener las estadisticas");
                IEnumerable<Estadistica> estadisticaList = await _informeQejaRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<InformeQuejaDto>>(estadisticaList);
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

        [HttpGet("Id:int", Name = "GetInformeQueja")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetInformeQueja(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer el informe con Id" + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                var informequeja = await _informeQejaRepo.Obtener(v => v.Id == id);

                if (informequeja == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = !false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<SolicitudPermisosDto>(informequeja);
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
    }
}
