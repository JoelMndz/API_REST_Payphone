using Aplicacion.Dominio.Movimiento.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class MovimientoDTO
    {
        public int Id { get; set; }
        public TiposDeOperacion Tipo { get; set; }
        public double Cantidad { get; set; }
        public int IdBilletera { get; set; }
        public string DocumentoIdentidad { get; set; } = string.Empty;
        public string NombrePropietario { get; set; } = string.Empty;
        public DateTimeOffset FechaCreacion { get; set; }
    }
}
