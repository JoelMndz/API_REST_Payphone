using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Helper.Comunes.Interfaces
{
    public interface IEntidadAuditable
    {
        public DateTimeOffset FechaCreacion { get; set; }
        public string TerminalCreacion { get; set; }
        public DateTimeOffset FechaModificacion { get; set; }
        public string TerminalModificacion { get; set; }
    }
}
