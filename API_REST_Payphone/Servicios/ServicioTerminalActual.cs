using Aplicacion.Helper.Servicios;

namespace API_REST_Payphone.Servicios
{
    public class ServicioTerminalActual: IServicioTerminalActual
    {
        private readonly IHttpContextAccessor http;
        public ServicioTerminalActual(IHttpContextAccessor http)
        {
            this.http = http;
        }

        public string DireccionIP => http.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    }
}
