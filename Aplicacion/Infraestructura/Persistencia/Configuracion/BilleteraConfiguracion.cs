using Aplicacion.Dominio.Billetera;
using Aplicacion.Dominio.Billetera.Enums;
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
    public class BilleteraConfiguracion : IEntityTypeConfiguration<Billetera>
    {
        public void Configure(EntityTypeBuilder<Billetera> entidad)
        {
            AuditableConfiguracion.Configurar(entidad);
            
            entidad.Property(x => x.Estado)
                .HasConversion(
                    x => (int)x,
                    x => (EstadosBilletera)x)
                .IsRequired();

            entidad.Property(x => x.DocumentoIdentidad)
                .HasMaxLength(10)
                .IsRequired();

            entidad.Property(x => x.NombrePropietario)
                .HasMaxLength(100)
                .IsRequired();

            entidad.Property(x => x.SaldoActual)
                .IsRequired();
        }
    }
}
