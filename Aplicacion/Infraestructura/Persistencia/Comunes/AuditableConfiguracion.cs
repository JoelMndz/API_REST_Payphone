using Aplicacion.Dominio.Comunes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Infraestructura.Persistencia.Comunes
{
    public class AuditableConfiguracion
    {
        public static void Configurar<TEntity>(EntityTypeBuilder<TEntity> entity)
        where TEntity : EntidadAuditable
        {
            entity.HasKey(e => e.Id);

            entity.Property(
                    e => e.UsuarioCreacion)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(
                    e => e.FechaCreacion)
                .IsRequired()
                .HasColumnType("datetimeoffset");

            entity.Property(
                    e => e.FechaModificacion)
                .IsRequired()
                .HasColumnType("datetimeoffset");

            entity.Property(
                    e => e.TerminalCreacion)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(
                    e => e.TerminalModificacion)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(
                    e => e.UsuarioModificacion)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
