using Aplicacion.Helper.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Dominio.Billetera.Errores
{
    public class ErroresBilletera
    {
        public class NoExisteBilletera(int id):ExepcionDominio($"No existe la billetera con el id '{id}'");
        public class NoSePuedeEliminarCuentaConSaldo(int id, double saldo):ExepcionDominio($"No se puede eliminar la cuenta con el id '{id}', porque tiene de saldo actual ${saldo}");
    }
}
