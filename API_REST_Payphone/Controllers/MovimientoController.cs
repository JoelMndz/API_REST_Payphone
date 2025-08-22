using API_REST_Payphone.Controllers.Comunes;
using Aplicacion.Caracteristicas.Movimiento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_Payphone.Controllers
{
    public class MovimientoController:ApiBaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> RealizarMovimiento(RealizarMovimiento.DatosRealizarMovimiento request)
        {
            var data = await Mediador.Send(new RealizarMovimiento.Comando(request));
            return Ok(data);
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerTodosLosMoSvimientos()
        {
            var data = await Mediador.Send(new ObtenerTodosLosMovimientos.Consulta());
            return Ok(data);
        }
    }
}
