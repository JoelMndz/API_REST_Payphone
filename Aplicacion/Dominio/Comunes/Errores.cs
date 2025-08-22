using Aplicacion.Helper.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Dominio.Comunes
{
    public class Errores
    {
        public class NoSePuedieronGuardarLosCambios():ExepcionDominio("No se puedieron guardar los cambios!");
    }
}
