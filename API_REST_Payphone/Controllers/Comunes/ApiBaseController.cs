using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_Payphone.Controllers.Comunes
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ApiBaseController:ControllerBase
    {
        private ISender mediador = null!;

        protected ISender Mediador => this.mediador ??= HttpContext.RequestServices.GetService<ISender>() ?? null!;
    }
}
