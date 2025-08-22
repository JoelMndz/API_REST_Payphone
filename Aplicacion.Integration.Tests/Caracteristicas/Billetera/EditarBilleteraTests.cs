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
    public class EditarBilleteraTests
    {
        private readonly SliceFixture sliceFixture;

        public EditarBilleteraTests(SliceFixture sliceFixture)
        {
            this.sliceFixture = sliceFixture;
        }

        [Fact]
        public async Task EditarBilletera_OK()
        {
            await sliceFixture.ResetCheckpoint();
            var billetera = FactoryBilletera.Crear();
            billetera.Estado = EstadosBilletera.Activa;
            await sliceFixture.InsertAsync(billetera);
            var nombreEsperado = "Pepe Mujica";
            await sliceFixture.SendAsync(new EditarBilletera.Comando(new EditarBilletera.DatosEditarBilletera
            {
                Id = billetera.Id,
                Estado = EstadosBilletera.Bloqueada,
                NombrePropietario = nombreEsperado
            }));

            var sut = await sliceFixture.FindOrDefaultAsync<BilleteraDominio>(billetera.Id);
            sut.ShouldNotBeNull();
            sut.NombrePropietario.ShouldBe(nombreEsperado.ToUpper());
            sut.Estado.ShouldBeEquivalentTo(EstadosBilletera.Bloqueada);
        }

        [Fact]
        public async Task EditarBilletera_CancelarBilleraConSaldo_Error()
        {
            await sliceFixture.ResetCheckpoint();
            var billetera = FactoryBilletera.Crear();
            billetera.SaldoActual = 40;
            billetera.Estado = EstadosBilletera.Activa;
            await sliceFixture.InsertAsync(billetera);

            Should.Throw<ErroresBilletera.NoSePuedeCancelarCuentaConSaldo>(
                async () => await sliceFixture.SendAsync(new EditarBilletera.Comando(new EditarBilletera.DatosEditarBilletera
                {
                    Id = billetera.Id,
                    Estado = EstadosBilletera.Cerrada,
                    NombrePropietario = billetera.NombrePropietario,
                }))
            );
        }
    }
}
