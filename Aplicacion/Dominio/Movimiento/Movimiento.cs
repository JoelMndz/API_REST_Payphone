using Aplicacion.Dominio.Billetera;
using Aplicacion.Dominio.Comunes;
using Aplicacion.Dominio.Movimiento.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Dominio.Movimiento
{
    public class Movimiento: EntidadAuditable
    {
        public TiposDeOperacion Tipo { get; set; }
        public double Cantidad { get; set; }
        public int IdBilletera { get; set; }
        public Billetera.Billetera Billetera { get; set; } = new();

        public static Movimiento Crear(TiposDeOperacion tipo, double cantidad, Billetera.Billetera billetera)
        {
            return new Movimiento()
            {
                Tipo = tipo,
                Cantidad = cantidad,
                IdBilletera = billetera.Id,
                Billetera = billetera,
            };
        }
    }
}
