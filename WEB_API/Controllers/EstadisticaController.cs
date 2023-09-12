using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Globalization;
using System.Net;
using WEB_API.Dtos;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticaController : ControllerBase
    {
        private readonly ILogger<EstadisticaController> _logger;
        private readonly ISolicitudPermisoRepository _solicitudPermisoRepo;
        private readonly ISolicitudVacacionesRepository _solicitudVacacionesRepo;
        private readonly IInformeQuejaRepository _informeQejaRepo;

        protected ApiResponse _response;

        public EstadisticaController(ILogger<EstadisticaController> logger, IMapper mapper, IInformeQuejaRepository informeQuejaRepository)
        {
            _logger = logger;
            _response = new();
            _informeQejaRepo = informeQuejaRepository;


        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetTotalEstadistics()
        {
            
            try
            {
                _logger.LogInformation("Obtener los reportes");

                var SolicitudesVacaciones = await _solicitudVacacionesRepo.ObtenerTodosAsync();
                var SolicitudesPermisos = await _solicitudPermisoRepo.ObtenerTodosAsync();
                var informeQueja = await _informeQejaRepo.ObtenerTodosAsync();

                var totalSolicitudesVacaciones = SolicitudesVacaciones.Count();
                var totalSolicitudesPermisos = SolicitudesPermisos.Count();
                var totalInformeQueja = informeQueja.Count();



                var estadisticasGenerales = new EstadisticasGenerales
                {
                    TotalSolicitudesVacaciones = totalSolicitudesVacaciones,
                    TotalSolicitudesPermisos = totalSolicitudesPermisos,
                    TotalInformseQuejas = totalInformeQueja
                    
                }; 


                _response.Resultado = estadisticasGenerales;
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

        [Authorize(Roles = "admin")]
        [HttpGet("estadisticasPorPeriodo/{periodo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetEstadisticasPorPeriodo(string periodo)
        {
            try
            {
                _logger.LogInformation($"Obtener estadísticas por período: {periodo}");

                // Parsear el valor de período en un objeto DateTime
                if (DateTime.TryParseExact(periodo, "MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaSeleccionada))
                {
                    // Calcular la fecha de inicio (primer día del mes) y la fecha de fin (último día del mes)
                    DateTime fechaInicio = new DateTime(fechaSeleccionada.Year, fechaSeleccionada.Month, 1);
                    DateTime fechaFin = fechaInicio.AddMonths(1).AddDays(-1);

                    // Obtener las estadísticas por período para solicitudes de vacaciones
                    var solicitudesVacacionesPorPeriodo = await _solicitudVacacionesRepo.ObtenerPorPeriodoAsync(
                        solicitud => solicitud.FechaSolicitud,
                        fechaInicio,
                        fechaFin
                    );

                    // Obtener las estadísticas por período para solicitudes de permisos
                    var solicitudesPermisosPorPeriodo = await _solicitudPermisoRepo.ObtenerPorPeriodoAsync(
                        permiso => permiso.FechaPermiso,
                        fechaInicio,
                        fechaFin
                    );

                    // Obtener las estadísticas por período para informes de quejas
                    var informeQuejaPorPeriodo = await _informeQejaRepo.ObtenerPorPeriodoAsync(
                        informe => informe.FechaInforme,
                        fechaInicio,
                        fechaFin
                    );

                    // Realizar cálculos para estadísticas por período aquí

                    var estadisticasPorPeriodo = new EstadisticasPorPeriodo
                    {
                        Periodo = periodo,
                        TotalSolicitudesVacaciones = solicitudesVacacionesPorPeriodo.Count(),
                        TotalSolicitudesPermisos = solicitudesPermisosPorPeriodo.Count(),
                        // Asigna otras estadísticas por período
                    };

                    _response.Resultado = estadisticasPorPeriodo;
                    _response.statusCode = HttpStatusCode.OK;

                    return Ok(_response);
                }
                else
                {
                    // Si el formato de período no es válido, puedes manejar el error aquí
                    // Por ejemplo, devolver un BadRequest con un mensaje de error
                    _response.IsExitoso = false;
                    _response.ErrorMessages = new List<string>() { "Formato de período no válido." };
                    return BadRequest(_response);
                }
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
