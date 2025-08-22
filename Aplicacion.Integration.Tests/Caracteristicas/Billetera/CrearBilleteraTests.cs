using Aplicacion.Caracteristicas.Billetera;
using Aplicacion.Dominio.Billetera;
using Aplicacion.Dominio.Billetera.Enums;
using Aplicacion.Integration.Tests.Comun;
using Aplicacion.Shared.Test.Factorys;
using Shouldly;
using BilleteraDominio = Aplicacion.Dominio.Billetera.Billetera;

namespace Aplicacion.Integration.Tests.Caracteristicas.Billetera
{
    [Collection(nameof(SliceFixture))]
    public class CrearBilleteraTests
    {
        private readonly SliceFixture sliceFixture;

        public CrearBilleteraTests(SliceFixture sliceFixture)
        {
            this.sliceFixture = sliceFixture;
        }

        [Fact]
        public async Task CrearBilletera_RetornaBilletera()
        {
            await sliceFixture.ResetCheckpoint();
            var billetera = FactoryBilletera.Crear();
            var comando = new CrearBilletera.Comando(new CrearBilletera.DatosCrearBilletera { 
                DocumentoIdentidad = billetera.DocumentoIdentidad,
                NombrePropietario = billetera.NombrePropietario,
            });
            var sut = await sliceFixture.SendAsync(comando);
            sut.Id.ShouldNotBe(0);

            var billeteraEnBase = await sliceFixture.FindAsync<BilleteraDominio>(sut.Id);
            billeteraEnBase.ShouldNotBeNull();
            billeteraEnBase.DocumentoIdentidad.ShouldBe(billetera.DocumentoIdentidad);
            billeteraEnBase.NombrePropietario.ShouldBe(billetera.NombrePropietario.Trim().ToUpper());
            billeteraEnBase.SaldoActual.ShouldBe(0);
            billeteraEnBase.Estado.ShouldBe(EstadosBilletera.Activa);

        }
    }
}
