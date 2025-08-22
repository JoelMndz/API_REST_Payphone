using Aplicacion.Caracteristicas.Billetera;
using Aplicacion.Dominio.Billetera.Enums;
using Aplicacion.Dominio.Billetera.Errores;
using Aplicacion.Integration.Tests.Comun;
using Aplicacion.Shared.Test.Factorys;
using Shouldly;
using BilleteraDominio = Aplicacion.Dominio.Billetera.Billetera;

namespace Aplicacion.Integration.Tests.Caracteristicas.Billetera
{
    [Collection(nameof(SliceFixture))]
    public class EliminarBilleteraTests
    {
        private readonly SliceFixture sliceFixture;

        public EliminarBilleteraTests(SliceFixture sliceFixture)
        {
            this.sliceFixture = sliceFixture;
        }

        [Fact]
        public async Task ElimniarBilletera_OK()
        {
            await sliceFixture.ResetCheckpoint();
            var billetera = FactoryBilletera.Crear();
            await sliceFixture.InsertAsync(billetera);

            var sut = await sliceFixture.SendAsync(new EliminarBilletera.Comando(billetera.Id));
            sut.Id.ShouldBe(billetera.Id);

            var billeteraEnBase = await sliceFixture.FindAsync<BilleteraDominio>(sut.Id);
            billeteraEnBase.ShouldNotBeNull();
            billeteraEnBase.Eliminado.ShouldBeTrue();
        }

        [Fact]
        public async Task ElimniarBilletera_SiYaEstaElimnado_Error()
        {
            await sliceFixture.ResetCheckpoint();
            var billetera = FactoryBilletera.Crear();
            billetera.Eliminar();
            await sliceFixture.InsertAsync(billetera);

            Should.Throw<ErroresBilletera.NoExisteBilletera>(
                async()=> await sliceFixture.SendAsync(new EliminarBilletera.Comando(billetera.Id))
            );
        }

        [Fact]
        public async Task ElimniarBilletera_SiLaCuentaTieneSaldo_Error()
        {
            await sliceFixture.ResetCheckpoint();
            var billetera = FactoryBilletera.Crear();
            billetera.SaldoActual = 40;
            await sliceFixture.InsertAsync(billetera);

            Should.Throw<ErroresBilletera.NoSePuedeEliminarCuentaConSaldo>(
                async () => await sliceFixture.SendAsync(new EliminarBilletera.Comando(billetera.Id))
            );
        }
    }
}
