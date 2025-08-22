using API_REST_Payphone.Controllers.Comunes;
using Aplicacion.Caracteristicas.Autenticacion;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_Payphone.Controllers
{
    public class AutenticacionController:ApiBaseController
    {
        [HttpPost("registro")]
        public async Task<ActionResult> RegustrarUsuario(RegistrarUsuario.DatosRegistrarUsuario request)
        {
            var data = await Mediador.Send(new RegistrarUsuario.Comando(request));
            return Ok(data);
        }
    }
}
