using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Estadistica
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int TotalSolicitudes { get; set; }
        public int TotalInformesQuejas { get; set; }
        public int TotalComunicaciones { get; set; }
        public int TotalResueltas { get; set; }
        public int TotalPendientes { get; set; }
    }
}
