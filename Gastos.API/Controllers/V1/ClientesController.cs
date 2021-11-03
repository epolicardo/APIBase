using System.Threading.Tasks;
using Configuration;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClientesController : Controller
    {
        private readonly IConfigurationHelper configHelper;
        private readonly IGenericRepository<Cliente> clienteRepository;

        public ClientesController(IConfigurationHelper configHelper, IGenericRepository<Cliente> clienteRepository)
        {
            this.configHelper = configHelper;
            this.clienteRepository = clienteRepository;
        }

        //Metodo encargado de retornar todos los clientes
        [HttpGet]
        public IActionResult Get()
        {
            var clientes = this.clienteRepository.GetAll();
            return this.Ok(clientes);
        }

        //Metodo encargado de retornar un cliente
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {

            var cliente = await this.clienteRepository.GetByIdAsync(id);
            if (cliente == null)
            {
                return this.NotFound();
            }
            return this.Ok(cliente);
        }


    }


    
}

