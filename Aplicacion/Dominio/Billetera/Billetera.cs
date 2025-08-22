using Aplicacion.Dominio.Billetera.Enums;
using Aplicacion.Dominio.Billetera.Errores;
using Aplicacion.Dominio.Comunes;
using Aplicacion.Dominio.Movimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Dominio.Billetera
{
    public class Billetera:EntidadAuditable
    {
        public string DocumentoIdentidad { get; set; } = string.Empty;
        public string NombrePropietario { get; set; } = string.Empty;
        public EstadosBilletera Estado { get; set; }
        public double SaldoActual { get; set; }
        public List<Movimiento.Movimiento> Movimientos { get; set; } = new();

        public static Billetera Crear(string documentoIdentidad, string nombrePropietario)
        {
            return new Billetera()
            {
                DocumentoIdentidad = documentoIdentidad.Trim(),
                NombrePropietario = nombrePropietario.ToUpper().Trim(),
                Estado = EstadosBilletera.Activa,
                SaldoActual = 0
            };
        }

        public void Eliminar()
        {
            if (SaldoActual > 0) throw new ErroresBilletera.NoSePuedeEliminarCuentaConSaldo(Id, SaldoActual);
            Eliminado = true;
        }

        public void Editar(string nombrePropietario, EstadosBilletera estado)
        {
            NombrePropietario = nombrePropietario.ToUpper().Trim();
            if(Estado != EstadosBilletera.Cerrada && estado == EstadosBilletera.Cerrada && SaldoActual > 0)
                throw new ErroresBilletera.NoSePuedeCancelarCuentaConSaldo(Id, SaldoActual);
            Estado = estado;
        }
    }
}
