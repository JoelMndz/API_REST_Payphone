using Aplicacion.Dominio.Billetera;
using Aplicacion.Dominio.Movimiento;
using Aplicacion.Dominio.Usuario;
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
        private readonly InterceptorDespachadorEventos interceptorDespachadorEventos = null!;
        public Contexto() { }
        public Contexto(
            DbContextOptions<Contexto> options,
            InterceptorEntidadAuditable interceptorEntidadAuditable,
            InterceptorDespachadorEventos interceptorDespachadorEventos
            ) : base(options)
        {
            this.interceptorEntidadAuditable = interceptorEntidadAuditable;
            this.interceptorDespachadorEventos = interceptorDespachadorEventos;
        }

        public virtual DbSet<Billetera> Billetera { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Movimiento> Movimiento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BilleteraConfiguracion());
            modelBuilder.ApplyConfiguration(new MovimientoConfiguracion());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());
            OnModelCreatingPartial(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(this.interceptorEntidadAuditable);
            optionsBuilder.AddInterceptors(this.interceptorDespachadorEventos);

            base.OnConfiguring(optionsBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
