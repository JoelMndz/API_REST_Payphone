using Aplicacion.Dominio.Billetera.Enums;
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
    }
}
