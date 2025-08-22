using Aplicacion.Dominio.Billetera.Evento;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Caracteristicas.Billetera.Eventos
{
    public class EliminarBilleteraEventHandler : INotificationHandler<EliminarBilleteraEvent>
    {
        private readonly ILogger<EliminarBilleteraEventHandler> logger;

        public EliminarBilleteraEventHandler(ILogger<EliminarBilleteraEventHandler> logger)
        {
            this.logger = logger;
        }
        public async Task Handle(EliminarBilleteraEvent notification, CancellationToken cancellationToken)
        {
            logger.LogWarning($"Se acaba de eliminar la billera con id {notification.Billetera.Id} de propietario {notification.Billetera.NombrePropietario}");
        }
    }
}
