using Aplicacion.Dominio.Billetera.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class BilleteraDTO
    {
        public int Id { get; set; }
        public string DocumentoIdentidad { get; set; } = string.Empty;
        public string NombrePropietario { get; set; } = string.Empty;
        public EstadosBilletera Estado { get; set; }
        public double SaldoActual { get; set; }
    }
}
