using Aplicacion.Helper.Comunes;
using System.Net;

namespace API_REST_Payphone.Middlewares
{
    public class ExepcionMiddleware
    {
        public record ExcepcionRespuesta(HttpStatusCode StatusCode, string Mensaje);
        private readonly RequestDelegate next;
        private readonly ILogger<ExepcionMiddleware> logger;

        public ExepcionMiddleware(RequestDelegate next, ILogger<ExepcionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                await ManejarExcepcion(context, e);
            }
        }

        private async Task ManejarExcepcion(HttpContext context, Exception exception)
        {
            var respuesta = exception switch
            {
                ExcepcionValidacion e => new ExcepcionRespuesta(HttpStatusCode.BadRequest, e.Errors.FirstOrDefault().Value.FirstOrDefault()),
                ExepcionDominio e => new ExcepcionRespuesta(HttpStatusCode.BadRequest, e.Message),
                Exception e => ManejarExcepcionNoControlada(e)
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)respuesta.StatusCode;
            await context.Response.WriteAsJsonAsync(respuesta);

        }

        private ExcepcionRespuesta ManejarExcepcionNoControlada(Exception exception)
        {
            //Pendiente: Solo mostrar el mensaje cuando sea en desarrollo, en produccion NO
            return new ExcepcionRespuesta(HttpStatusCode.InternalServerError, exception.Message ?? "Error, comuniquese con sistemas");
        }
    }
}
