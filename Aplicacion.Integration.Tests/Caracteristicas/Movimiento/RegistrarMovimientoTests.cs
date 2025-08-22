using Aplicacion.Caracteristicas.Billetera;
using Aplicacion.Caracteristicas.Movimiento;
using Aplicacion.Dominio.Billetera.Enums;
using Aplicacion.Dominio.Billetera.Errores;
using Aplicacion.Dominio.Movimiento.Enums;
using Aplicacion.Dominio.Movimiento.Errores;
using Aplicacion.Integration.Tests.Comun;
using Aplicacion.Shared.Test.Factorys;
using Shouldly;
using BilleteraDominio = Aplicacion.Dominio.Billetera.Billetera;

namespace Aplicacion.Integration.Tests.Caracteristicas.Movimiento
{
    [Collection(nameof(SliceFixture))]
    public class RegistrarMovimientoTests
    {
        private readonly SliceFixture sliceFixture;

        public RegistrarMovimientoTests(SliceFixture sliceFixture)
        {
            this.sliceFixture = sliceFixture;
        }

        [Fact]
        public async Task RealizarMovimiento_OK()
        {
            await sliceFixture.ResetCheckpoint();
            var billetera = FactoryBilletera.Crear();
            billetera.SaldoActual = 450.5;
            await sliceFixture.InsertAsync(billetera);

            var comando = new RealizarMovimiento.Comando(new RealizarMovimiento.DatosRealizarMovimiento
            {
                Cantidad = 450,
                IdBilletera = billetera.Id,
                Tipo = TiposDeOperacion.Debito,
            });
            
            var movimiento = await sliceFixture.SendAsync(comando);
            movimiento.Id.ShouldNotBe(0);
            var sut = await sliceFixture.FindOrDefaultAsync<BilleteraDominio>(billetera.Id);
            sut.SaldoActual.ShouldBe(billetera.SaldoActual - comando.Datos.Cantidad!.Value);
        }

        [Fact]
        public async Task RealizarMovimiento_SaldoNegativo_Error()
        {
            await sliceFixture.ResetCheckpoint();
            var billetera = FactoryBilletera.Crear();
            billetera.SaldoActual = 100;
            await sliceFixture.InsertAsync(billetera);

            var comando = new RealizarMovimiento.Comando(new RealizarMovimiento.DatosRealizarMovimiento
            {
                Cantidad = 450,
                IdBilletera = billetera.Id,
                Tipo = TiposDeOperacion.Debito,
            });

            Should.Throw<ErroresMovimiento.SaldoInsuficiente>(
                async () => await sliceFixture.SendAsync(comando)
            );
        }

        [Fact]
        public async Task RealizarMovimiento_Credito_OK()
        {
            await sliceFixture.ResetCheckpoint();
            var billetera = FactoryBilletera.Crear();
            billetera.SaldoActual = 450.5;
            await sliceFixture.InsertAsync(billetera);

            var comando = new RealizarMovimiento.Comando(new RealizarMovimiento.DatosRealizarMovimiento
            {
                Cantidad = 450,
                IdBilletera = billetera.Id,
                Tipo = TiposDeOperacion.Credito,
            });

            var movimiento = await sliceFixture.SendAsync(comando);
            movimiento.Id.ShouldNotBe(0);
            var sut = await sliceFixture.FindOrDefaultAsync<BilleteraDominio>(billetera.Id);
            sut.SaldoActual.ShouldBe(billetera.SaldoActual + comando.Datos.Cantidad!.Value);
        }

        [Fact]
        public async Task RealizarMovimiento_CuentaCerrada_Error()
        {
            await sliceFixture.ResetCheckpoint();
            var billetera = FactoryBilletera.Crear();
            billetera.SaldoActual = 100;
            billetera.Estado = EstadosBilletera.Cerrada;
            await sliceFixture.InsertAsync(billetera);

            var comando = new RealizarMovimiento.Comando(new RealizarMovimiento.DatosRealizarMovimiento
            {
                Cantidad = 450,
                IdBilletera = billetera.Id,
                Tipo = TiposDeOperacion.Debito,
            });

            Should.Throw<ErroresMovimiento.LaCuentaNoEstaActiva>(
                async () => await sliceFixture.SendAsync(comando)
            );
        }

        [Fact]
        public async Task RealizarMovimiento_CuentaBloqueada_Error()
        {
            await sliceFixture.ResetCheckpoint();
            var billetera = FactoryBilletera.Crear();
            billetera.SaldoActual = 100;
            billetera.Estado = EstadosBilletera.Bloqueada;
            await sliceFixture.InsertAsync(billetera);

            var comando = new RealizarMovimiento.Comando(new RealizarMovimiento.DatosRealizarMovimiento
            {
                Cantidad = 450,
                IdBilletera = billetera.Id,
                Tipo = TiposDeOperacion.Debito,
            });

            Should.Throw<ErroresMovimiento.LaCuentaNoEstaActiva>(
                async () => await sliceFixture.SendAsync(comando)
            );
        }

        [Fact]
        public async Task RealizarMovimiento_BilleteraEliminada_Error()
        {
            await sliceFixture.ResetCheckpoint();
            var billetera = FactoryBilletera.Crear();
            billetera.Eliminar();
            await sliceFixture.InsertAsync(billetera);

            var comando = new RealizarMovimiento.Comando(new RealizarMovimiento.DatosRealizarMovimiento
            {
                Cantidad = 450,
                IdBilletera = billetera.Id,
                Tipo = TiposDeOperacion.Credito,
            });

            Should.Throw<ErroresBilletera.NoExisteBilletera>(
                async () => await sliceFixture.SendAsync(comando)
            );
        }
    }
}
