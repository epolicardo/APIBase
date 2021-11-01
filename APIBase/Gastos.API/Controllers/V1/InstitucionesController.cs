using Configuration;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class InstitucionesController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IGenericRepository<Institucion> genericRepository;

        public InstitucionesController(DataContext _context, IGenericRepository<Institucion> _genericRepository, IConfigurationHelper configHelper)
        {
            context = _context;
            genericRepository = _genericRepository;
            ConfigHelper = configHelper;
        }

        public IConfigurationHelper ConfigHelper { get; }
    }
}
