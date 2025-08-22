using Aplicacion.Caracteristicas.Autenticacion;
using Aplicacion.Caracteristicas.Billetera;
using Bogus;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Unit.Test.Caracteristicas.Autenticacion
{
    public class RegistrarUsuarioTests
    {
        private readonly Faker faker;
        public RegistrarUsuarioTests()
        {
            faker = new Faker("es_MX");
        }

        [Fact]
        public async void ValidadorComando_CamposCorrectos()
        {
            var comando = new RegistrarUsuario.Comando(new RegistrarUsuario.DatosRegistrarUsuario
            {
                UserName = "jmendez",
                Clave = "Paypal2025"
            });
            var validador = new RegistrarUsuario.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldNotHaveValidationErrorFor(x => x.Datos.UserName);
            sut.ShouldNotHaveValidationErrorFor(x => x.Datos.Clave);
        }
        [Fact]
        public async void ValidadorComando_UserName_NoAceptaNull_Error()
        {
            var comando = new RegistrarUsuario.Comando(new RegistrarUsuario.DatosRegistrarUsuario
            {
                UserName = null,
            });
            var validador = new RegistrarUsuario.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.UserName);
        }

        [Fact]
        public async void ValidadorComando_UserName_SoloLetras_Error()
        {
            var comando = new RegistrarUsuario.Comando(new RegistrarUsuario.DatosRegistrarUsuario
            {
                UserName = "jmendez14",
            });
            var validador = new RegistrarUsuario.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.UserName);
        }

        [Fact]
        public async void ValidadorComando_UserName_Maximo25Caracteres_Error()
        {
            var comando = new RegistrarUsuario.Comando(new RegistrarUsuario.DatosRegistrarUsuario
            {
                UserName = faker.Lorem.Letter(26),
            });
            var validador = new RegistrarUsuario.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.UserName);
        }

        [Fact]
        public async void ValidadorComando_UserName_Minimo4Caracteres_Error()
        {
            var comando = new RegistrarUsuario.Comando(new RegistrarUsuario.DatosRegistrarUsuario
            {
                UserName = "sol",
            });
            var validador = new RegistrarUsuario.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.UserName);
        }

        [Fact]
        public async void ValidadorComando_Clave_NoAceptaNull_Error()
        {
            var comando = new RegistrarUsuario.Comando(new RegistrarUsuario.DatosRegistrarUsuario
            {
                Clave = null,
            });
            var validador = new RegistrarUsuario.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.Clave);
        }

        [Fact]
        public async void ValidadorComando_Clave_FormatoConMayusculaMinuculaNumero_Error()
        {
            var comando = new RegistrarUsuario.Comando(new RegistrarUsuario.DatosRegistrarUsuario
            {
                Clave = "AAAASDASD123",
            });
            var validador = new RegistrarUsuario.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.Clave);
        }

        [Fact]
        public async void ValidadorComando_Clave_Minimo8Carcateres_Error()
        {
            var comando = new RegistrarUsuario.Comando(new RegistrarUsuario.DatosRegistrarUsuario
            {
                Clave = "Adf12",
            });
            var validador = new RegistrarUsuario.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.Clave);
        }

        [Fact]
        public async void ValidadorComando_Clave_Maximo50Caracteres_Error()
        {
            var comando = new RegistrarUsuario.Comando(new RegistrarUsuario.DatosRegistrarUsuario
            {
                Clave = faker.Lorem.Letter(51),
            });
            var validador = new RegistrarUsuario.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.Clave);
        }
    }
}
