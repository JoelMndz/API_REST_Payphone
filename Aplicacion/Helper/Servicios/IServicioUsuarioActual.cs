using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Helper.Servicios
{
    public interface IServicioUsuarioActual
    {
        public int Id { get; }
        public string Username { get; }
    }
}
