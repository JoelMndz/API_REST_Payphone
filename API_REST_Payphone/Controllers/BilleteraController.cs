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

        [HttpGet]
        public async Task<ActionResult> ObtenerTodasLasBilleteras()
        {
            var data = await Mediador.Send(new ObtenerTodasLasBilleteras.Consulta());
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarBilletera(int id)
        {
            var data = await Mediador.Send(new EliminarBilletera.Comando(id));
            return Ok(data);
        }

        [HttpPatch]
        public async Task<ActionResult> EditarBilletera(EditarBilletera.DatosEditarBilletera request)
        {
            var data = await Mediador.Send(new EditarBilletera.Comando(request));
            return Ok(data);
        }
    }
}
