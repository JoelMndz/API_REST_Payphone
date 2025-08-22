using Aplicacion.Helper.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Dominio.Usuario.Errores
{
    public class ErroresUsuario
    {
        public class ElUserNameYaExiste(string userName):ExepcionDominio($"El userName {userName} ya existe!");
        public class CredencialesIncorrectas():ExepcionDominio($"Credenciales incorrrectas!");
    }
}
