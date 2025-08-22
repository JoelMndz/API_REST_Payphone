using Aplicacion.Dominio.Billetera;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Shared.Test.Factorys
{
    public class FactoryBilletera
    {
        public static Billetera Crear(string? documentoIdentidad = null, string? nombrePropietario = null)
        {
            var faker = new Faker("es_MX");
            var cedulas = new List<string>
            {
                "1312386921",
                "1312386962"
            };
            return Billetera.Crear(
                documentoIdentidad ?? faker.PickRandom(cedulas), 
                nombrePropietario ?? faker.Person.FullName
            );
        }
    }
}
