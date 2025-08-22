using Aplicacion.Helper.Comunes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Dominio.Comunes
{
    public abstract class EntidadBase: IEntidad
    {
        public int Id { get; set; }
        public bool Eliminado { get; set; }
    }
}
