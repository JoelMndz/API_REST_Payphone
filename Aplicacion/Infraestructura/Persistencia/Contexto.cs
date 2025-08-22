using Aplicacion.Dominio.Billetera;
using Aplicacion.Dominio.Movimiento;
using Aplicacion.Infraestructura.Persistencia.Comunes;
using Aplicacion.Infraestructura.Persistencia.Configuracion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Infraestructura.Persistencia
{
    public partial class Contexto:DbContext
    {
        private readonly InterceptorEntidadAuditable interceptorEntidadAuditable = null!;
        public Contexto() { }
        public Contexto(
            DbContextOptions<Contexto> options,
            InterceptorEntidadAuditable interceptorEntidadAuditable
            ) : base(options)
        {
            this.interceptorEntidadAuditable = interceptorEntidadAuditable;
        }

        public virtual DbSet<Billetera> Billetera { get; set; }
        public virtual DbSet<Movimiento> Movimiento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BilleteraConfiguracion());
            modelBuilder.ApplyConfiguration(new MovimientoConfiguracion());
            OnModelCreatingPartial(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(this.interceptorEntidadAuditable);

            base.OnConfiguring(optionsBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
