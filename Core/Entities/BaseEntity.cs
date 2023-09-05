using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public DateTime FechaDeModificacion { get; set; }

    }
}
