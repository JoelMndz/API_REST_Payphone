using Aplicacion.Helper.Servicios;

namespace API_REST_Payphone.Servicios
{
    public class ServicioUsuarioActual:IServicioUsuarioActual
    {
        private readonly IHttpContextAccessor http;
        public ServicioUsuarioActual(IHttpContextAccessor http)
        {
            this.http = http;
        }

        public string Username => http.HttpContext?
            .User.Claims.FirstOrDefault(x => x.Type == "username")?.Value ?? string.Empty;

        public int Id => int.Parse(http.HttpContext!
            .User.Claims.FirstOrDefault(x => x.Type == "id")?.Value ?? "0");

    }
}
