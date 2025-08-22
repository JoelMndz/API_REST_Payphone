using Aplicacion.Caracteristicas.Movimiento;
using Aplicacion.Dominio.Movimiento.Enums;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Unit.Test.Caracteristicas.Movimiento
{
    public class RealizarMovimientoTests
    {
        [Fact]
        public async void ValidadorComando_OK()
        {
            var comando = new RealizarMovimiento.Comando(new RealizarMovimiento.DatosRealizarMovimiento
            {
                Cantidad = 30,
                IdBilletera = 1,
                Tipo = TiposDeOperacion.Debito,
            });
            var validador = new RealizarMovimiento.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldNotHaveValidationErrorFor(x => x.Datos.Cantidad);
            sut.ShouldNotHaveValidationErrorFor(x => x.Datos.Tipo);
            sut.ShouldNotHaveValidationErrorFor(x => x.Datos.IdBilletera);
        }

        [Fact]
        public async void ValidadorComando_CamposNull_Error()
        {
            var comando = new RealizarMovimiento.Comando(new RealizarMovimiento.DatosRealizarMovimiento
            {
                Cantidad = null,
                IdBilletera = null,
                Tipo = null,
            });
            var validador = new RealizarMovimiento.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.Cantidad);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.Tipo);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.IdBilletera);
        }

        [Fact]
        public async void ValidadorComando_Cantidad_MayorA0_Error()
        {
            var comando = new RealizarMovimiento.Comando(new RealizarMovimiento.DatosRealizarMovimiento
            {
                Cantidad = -1
            });
            var validador = new RealizarMovimiento.ValidadorComando();
            var sut = await validador.TestValidateAsync(comando);
            sut.ShouldHaveValidationErrorFor(x => x.Datos.Cantidad);
        }
    }
}
