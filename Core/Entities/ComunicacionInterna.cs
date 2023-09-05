using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ComunicacionInterna : BaseEntity
    {
        public int UserId { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaEnvio { get; set; }
        public bool Leeido { get; set; }
    }
}
