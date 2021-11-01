using Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DomiciliosController : Controller
    {
        private readonly IConfigurationHelper configHelper;

        public DomiciliosController(IConfigurationHelper configHelper)
        {
            this.configHelper = configHelper;
        }

    }
}
