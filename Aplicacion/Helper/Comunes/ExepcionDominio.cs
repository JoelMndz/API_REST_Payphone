using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Helper.Comunes
{
    public class ExepcionDominio(string mensaje):Exception(mensaje);
}
