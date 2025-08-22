using Aplicacion.Helper.Comunes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Dominio.Comunes
{
    public abstract class EntidadAuditable : EntidadBase, IEntidadAuditable
    {
        public string UsuarioCreacion { get; set; } = string.Empty;
        public DateTimeOffset FechaCreacion { get; set; }
        public string TerminalCreacion { get; set; } =  string.Empty;
        public DateTimeOffset FechaModificacion { get; set; }
        public string TerminalModificacion { get; set; } = string.Empty;
        public string UsuarioModificacion { get; set; } = string.Empty;
    }
}
