using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SolicitudPermiso : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public EstadoIncidencia Estado { get; set; }
    }
}
