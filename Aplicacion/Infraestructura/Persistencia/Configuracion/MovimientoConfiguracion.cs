using Aplicacion.Dominio.Movimiento;
using Aplicacion.Dominio.Movimiento.Enums;
using Aplicacion.Infraestructura.Persistencia.Comunes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Infraestructura.Persistencia.Configuracion
{
    public class MovimientoConfiguracion : IEntityTypeConfiguration<Movimiento>
    {
        public void Configure(EntityTypeBuilder<Movimiento> entidad)
        {
            AuditableConfiguracion.Configurar(entidad);

            entidad.Property(x => x.Tipo)
                .HasConversion(
                    x => (int)x,
                    x => (TiposDeOperacion)x)
                .IsRequired();

            entidad.Property(x => x.Cantidad)
                .IsRequired();

            entidad.Property(x => x.IdBilletera)
                .IsRequired();

            entidad.HasOne(x => x.Billetera)
                .WithMany(x => x.Movimientos)
                .HasForeignKey(x => x.IdBilletera);
        }
    }
}
