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
                "1710034065",
                "0926687852",
                "0102030405",
                "1827364526",
                "0523456783",
                "2019283742",
                "1425364787",
                "0829384750",
                "2223344456",
                "0629183741"
            };
            return Billetera.Crear(
                documentoIdentidad ?? faker.PickRandom(cedulas), 
                nombrePropietario ?? faker.Person.FullName
            );
        }
    }
}
