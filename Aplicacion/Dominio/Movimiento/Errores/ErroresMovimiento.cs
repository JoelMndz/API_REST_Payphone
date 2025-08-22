using Aplicacion.Helper.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Dominio.Movimiento.Errores
{
    public class ErroresMovimiento
    {
        public class SaldoInsuficiente(double saldoActual)
            :ExepcionDominio($"Saldo insufuciente, tu saldo actual es ${saldoActual}");

        public class LaCuentaNoEstaActiva()
            : ExepcionDominio($"No se puede realizar el movimiento porque la cuenta no esta activa!");
    }
}
