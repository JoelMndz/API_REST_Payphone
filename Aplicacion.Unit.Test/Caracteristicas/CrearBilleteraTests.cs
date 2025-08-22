using Aplicacion.Caracteristicas.Billetera;
using Bogus;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Unit.Test.Caracteristicas
{
    public class CrearBilleteraTests
    {
        private readonly Faker faker;
        public CrearBilleteraTests()
        {
            faker = new Faker("es_MX");
        }
        [Fact]
        public async void ValidadorComando_CamposCorrectos()
        {
            var comando = new CrearBilletera.Comando(new CrearBilletera.DatosCrearBilletera
            {
                DocumentoIdentidad = "1312386921",
                NombrePropietario = "Joel Loor"
            });
            var validador = new CrearBilletera.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldNotHaveValidationErrorFor(x => x.Datos.DocumentoIdentidad);
            sut.ShouldNotHaveValidationErrorFor(x => x.Datos.NombrePropietario);
        }

        [Fact]
        public async void ValidadorComando_Cedula_CuandoEsNull_Error()
        {
            var comando = new CrearBilletera.Comando(new CrearBilletera.DatosCrearBilletera
            {
                DocumentoIdentidad = null
            });
            var validador = new CrearBilletera.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.DocumentoIdentidad);
        }

        [Fact]
        public async void ValidadorComando_Cedula_CuandoEstaVacia_Error()
        {
            var comando = new CrearBilletera.Comando(new CrearBilletera.DatosCrearBilletera
            {
                DocumentoIdentidad = ""
            });
            var validador = new CrearBilletera.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.DocumentoIdentidad);
        }

        [Fact]
        public async void ValidadorComando_Cedula_CuandoNoTieneLaEstructura_Error()
        {
            var comando = new CrearBilletera.Comando(new CrearBilletera.DatosCrearBilletera
            {
                DocumentoIdentidad = "1312386925"
            });
            var validador = new CrearBilletera.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.DocumentoIdentidad);
        }

        [Fact]
        public async void ValidadorComando_Cedula_CuandoNoTieneLetras_Error()
        {
            var comando = new CrearBilletera.Comando(new CrearBilletera.DatosCrearBilletera
            {
                DocumentoIdentidad = "131238692A"
            });
            var validador = new CrearBilletera.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.DocumentoIdentidad);
        }

        [Fact]
        public async void ValidadorComando_NombrePropietario_CuandoEsNull_Error()
        {
            var comando = new CrearBilletera.Comando(new CrearBilletera.DatosCrearBilletera
            {
                NombrePropietario = null
            });
            var validador = new CrearBilletera.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.NombrePropietario);
        }

        [Fact]
        public async void ValidadorComando_NombrePropietario_CuandoEstaVacio_Error()
        {
            var comando = new CrearBilletera.Comando(new CrearBilletera.DatosCrearBilletera
            {
                NombrePropietario = ""
            });
            var validador = new CrearBilletera.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.NombrePropietario);
        }

        [Fact]
        public async void ValidadorComando_NombrePropietario_CuandoTieneNumeros_Error()
        {
            var comando = new CrearBilletera.Comando(new CrearBilletera.DatosCrearBilletera
            {
                NombrePropietario = "Joel123"
            });
            var validador = new CrearBilletera.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.NombrePropietario);
        }

        [Fact]
        public async void ValidadorComando_NombrePropietario_CuandoTieneMasDe100Carcateres_Error()
        {
            var comando = new CrearBilletera.Comando(new CrearBilletera.DatosCrearBilletera
            {
                NombrePropietario = faker.Lorem.Letter(101)
            });
            var validador = new CrearBilletera.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.NombrePropietario);
        }

        [Fact]
        public async void ValidadorComando_NombrePropietario_CuandoTieneMenosDe4Carcateres_Error()
        {
            var comando = new CrearBilletera.Comando(new CrearBilletera.DatosCrearBilletera
            {
                NombrePropietario = "sol"
            });
            var validador = new CrearBilletera.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.NombrePropietario);
        }
    }
}
