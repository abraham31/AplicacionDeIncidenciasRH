using Core.Entities;

namespace WEB_API.Dtos
{
    public class InformeQuejaDto
    {
        public int UserId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public EstadoIncidencia Estado { get; set; }
    }
}
