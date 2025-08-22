using Aplicacion.Dominio.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Dominio.Usuario
{
    public class Usuario:EntidadAuditable
    {
        public string UserName { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
    }
}
