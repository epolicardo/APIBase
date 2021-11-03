using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MensajesController : ControllerBase
    {
        //TODO: Controlador encargado de mostrar mensajes guardados en base de datos, parametrizables y genericos.
    }
}
