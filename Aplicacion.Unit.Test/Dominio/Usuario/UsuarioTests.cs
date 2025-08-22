using Shouldly;
using UsuarioDominio = Aplicacion.Dominio.Usuario.Usuario;

namespace Aplicacion.Unit.Test.Dominio.Usuario
{
    public class UsuarioTests
    {
        [Fact]
        public void Crear_RetornaUsuario()
        {
            var usernameEsperado = " jmendez ";
            var claveEsperada = "Paypal123";
            var sut = UsuarioDominio.Crear(usernameEsperado, claveEsperada);
            sut.UserName.ShouldBe(usernameEsperado.Trim().ToUpper());
            sut.Clave.ShouldBe(claveEsperada);
        }
    }
}
