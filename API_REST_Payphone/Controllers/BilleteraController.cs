using API_REST_Payphone.Controllers.Comunes;
using Aplicacion.Caracteristicas.Billetera;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_Payphone.Controllers
{
    [Authorize]
    public class BilleteraController:ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult> CrearBilletera(CrearBilletera.DatosCrearBilletera request)
        {
            var data = await Mediador.Send(new CrearBilletera.Comando(request));
            return Ok(data);
        }
    }
}
