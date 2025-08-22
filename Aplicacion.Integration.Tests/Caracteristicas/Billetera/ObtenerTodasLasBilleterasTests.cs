using Aplicacion.Caracteristicas.Billetera;
using Aplicacion.Integration.Tests.Comun;
using Aplicacion.Shared.Test.Factorys;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Integration.Tests.Caracteristicas.Billetera
{
    [Collection(nameof(SliceFixture))]
    public class ObtenerTodasLasBilleterasTests
    {
        private readonly SliceFixture sliceFixture;

        public ObtenerTodasLasBilleterasTests(SliceFixture sliceFixture)
        {
            this.sliceFixture = sliceFixture;
        }
        [Fact]
        public async Task ObtenerTodo_RetornaListaVacia()
        {
            await sliceFixture.ResetCheckpoint();
            var sut = await sliceFixture.SendAsync(new ObtenerTodasLasBilleteras.Consulta());
            sut.Count.ShouldBe(0);
        }

        [Fact]
        public async Task ObtenerTodo_RetornaListaSinElimnados()
        {
            await sliceFixture.ResetCheckpoint();
            var billetera1 = FactoryBilletera.Crear();
            var billetera2 = FactoryBilletera.Crear();
            billetera1.Eliminar();
            await sliceFixture.InsertAsync(billetera1, billetera2);

            var sut = await sliceFixture.SendAsync(new ObtenerTodasLasBilleteras.Consulta());
            sut.Count.ShouldBe(1);
        }
    }
}
