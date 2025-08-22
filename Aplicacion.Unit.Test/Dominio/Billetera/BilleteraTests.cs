using Aplicacion.Dominio.Billetera;
using Aplicacion.Shared.Test.Factorys;
using Shouldly;
using BilleteraDominio = Aplicacion.Dominio.Billetera.Billetera;

namespace Aplicacion.Unit.Test.Dominio.Billetera
{
    public class BilleteraTests
    {
        [Fact]
        public void Crear_RetornaBilletera()
        {
            var cedulaEsperada = "1312386921 ";
            var nombreEsperado = " Joel Loor ";

            var sut = BilleteraDominio.Crear(cedulaEsperada, nombreEsperado);
            sut.DocumentoIdentidad.ShouldBe(cedulaEsperada.Trim());
            sut.NombrePropietario.ShouldBe(nombreEsperado.Trim().ToUpper());
        }

        [Fact]
        public void Eliminar_OK()
        {
            var billetera = FactoryBilletera.Crear();
            billetera.Eliminado.ShouldBeFalse();
            billetera.Eliminar();
            billetera.Eliminado.ShouldBeTrue();
        }
    }
}
