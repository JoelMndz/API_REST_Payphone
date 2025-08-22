using Aplicacion.Helper.Comportamientos;
using Aplicacion.Infraestructura.Persistencia;
using Aplicacion.Infraestructura.Persistencia.Comunes;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public static class InyeccionDependencia
    {
        public static IServiceCollection AgregarAplicacion(this IServiceCollection services)
        {
            services.AddScoped<InterceptorEntidadAuditable>();
            services.AddScoped<InterceptorDespachadorEventos>();
            services.AddDbContext<Contexto>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
                options.UseSqlServer(Environment.GetEnvironmentVariable("URI_DB"),
                    (a) => a.MigrationsAssembly("API_REST_Payphone"));
            },
            ServiceLifetime.Transient);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Validacion<,>));

            return services;
        }
    }
}
