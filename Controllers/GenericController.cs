using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Controllers
{
    public class GenericController : ControllerBase
    {
        private readonly IConfiguration configuration = new();

        public GenericController()
        {
        }
        public bool UseMockup(string Servicio)
        {
            if (configuration[Servicio] == configuration["AmbienteMockup"] && configuration["AmbienteMockup"] == "true")
                return true;
            return false;
        }

    }
}
