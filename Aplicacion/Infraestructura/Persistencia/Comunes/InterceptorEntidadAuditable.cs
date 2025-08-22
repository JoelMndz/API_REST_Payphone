using Aplicacion.Dominio.Comunes;
using Aplicacion.Helper.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Infraestructura.Persistencia.Comunes
{
    public class InterceptorEntidadAuditable: SaveChangesInterceptor
    {
        private readonly IServicioTerminalActual terminalActual;
        private readonly IServicioUsuarioActual usuarioActual;

        public InterceptorEntidadAuditable(IServicioTerminalActual terminalActual, IServicioUsuarioActual usuarioActual)
        {
            this.terminalActual = terminalActual;
            this.usuarioActual = usuarioActual;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            GestionarAuditoria(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            GestionarAuditoria(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void GestionarAuditoria(DbContext? contexto)
        {
            if (contexto is null) return;
            foreach (var item in contexto.ChangeTracker.Entries<EntidadAuditable>())
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.Entity.UsuarioCreacion = usuarioActual.Username;
                        item.Entity.FechaCreacion = DateTimeOffset.Now;
                        item.Entity.TerminalCreacion = this.terminalActual.DireccionIP;
                        item.Entity.FechaModificacion = DateTimeOffset.Now;
                        item.Entity.TerminalModificacion = this.terminalActual.DireccionIP;
                        item.Entity.UsuarioModificacion = usuarioActual.Username;
                        break;
                    case EntityState.Modified:
                        item.Entity.FechaModificacion = DateTimeOffset.Now;
                        item.Entity.TerminalModificacion = this.terminalActual.DireccionIP;
                        item.Entity.UsuarioModificacion = usuarioActual.Username;
                        break;
                }
            }
        }
    }
}
