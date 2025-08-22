using Aplicacion.Dominio.Usuario;
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
    public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> entidad)
        {
            AuditableConfiguracion.Configurar(entidad);

            entidad.Property(x => x.UserName)
                .HasMaxLength(25)
                .IsRequired();

            entidad.Property(x => x.Clave)
                .HasColumnType("nvarchar(max)")
                .IsRequired();
        }
    }
}
