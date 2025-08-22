using Aplicacion.Infraestructura.Persistencia;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Integration.Tests.Comun
{
    internal class AplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(cfg =>
            {
                var integrationConfig = new ConfigurationBuilder()
                    //.AddJsonFile("appsettings.Test.json") //Lo descomento si empiezo a trabajar con el .json
                    .AddEnvironmentVariables()
                    .Build();

                cfg.AddConfiguration(integrationConfig);
            });

            builder.ConfigureServices((builder, services) =>
            {
                services
                .Remove<DbContextOptions<Contexto>>()
                .AddDbContext<Contexto>((sp, options) =>
                    options.UseSqlServer(Environment.GetEnvironmentVariable("URI_DB"),
                        builder => builder.MigrationsAssembly(typeof(Contexto).Assembly.FullName)));
            });
        }
    }
}
