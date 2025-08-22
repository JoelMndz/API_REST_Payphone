using Aplicacion.Dominio.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Dominio.Billetera.Evento
{
    public record EliminarBilleteraEvent(Billetera Billetera): IEventoDominio;
}
