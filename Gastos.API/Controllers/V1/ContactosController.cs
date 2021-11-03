using Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ContactosController : Controller
    {
        private readonly IConfigurationHelper configHelper;

        public ContactosController(IConfigurationHelper configHelper)
        {
            this.configHelper = configHelper;
        }

    }
}
