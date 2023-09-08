
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
    public class InformeQuejaController : ControllerBase
    {
        private readonly ILogger<InformeQuejaController> _logger;
        private readonly IInformeQuejaRepository _informeQejaRepo;
        private readonly IMapper _mapper;
        protected ApiResponse _response;

        public InformeQuejaController(ILogger<InformeQuejaController> logger, IInformeQuejaRepository informequejaRepo, IMapper mapper)
        {
            _logger = logger;
            _informeQejaRepo = informequejaRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetInformeQueja()
        {
            try
            {
                _logger.LogInformation("Obtener las quejas");
                IEnumerable<InformeQueja> informequejasList = await _informeQejaRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<InformeQuejaDto>>(informequejasList);
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


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CrearInformeQueja([FromBody] InformeQuejaDto informequejaDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (informequejaDto == null)
                {
                    return BadRequest(informequejaDto);
                }
                InformeQueja modelo = _mapper.Map<InformeQueja>(informequejaDto);

                modelo.FechaDeCreacion = DateTime.Now;
                modelo.FechaDeModificacion = DateTime.Now;
                await _informeQejaRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetInformeQueja", new { id = modelo.Id }, _response);

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
        public async Task<ActionResult<ApiResponse>> DeleteInformeQueja(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var informeQueja = await _informeQejaRepo.Obtener(v => v.Id == id);
                if (informeQueja == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _informeQejaRepo.Remover(informeQueja);

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
        public async Task<IActionResult> UpdateInformeQeuja(int id, [FromBody] InformeQuejaDto informeQuejaUpdateDto)
        {
            if (informeQuejaUpdateDto == null || id != informeQuejaUpdateDto.Id)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            InformeQueja modelo = _mapper.Map<InformeQueja>(informeQuejaUpdateDto);


            await _informeQejaRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }

    }
}
