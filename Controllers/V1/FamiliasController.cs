using Configuration;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FamiliasController : Controller
    {
        private readonly IConfigurationHelper configHelper;
        private readonly IGenericRepository<Familia> familiaRepository;

        public FamiliasController(IConfigurationHelper configHelper, IGenericRepository<Familia> FamiliaRepository)
        {
            this.configHelper = configHelper;
            familiaRepository = FamiliaRepository;
        }

        [HttpGet]
        [Route("GetList")]
        public IEnumerable<Familia> GetList()
        {
            if (configHelper.UseMockup("Debug_Usuarios"))
            {

            }
            return familiaRepository.GetAll();
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<Familia> GetById(int id)
        {
            if (configHelper.UseMockup("Debug_Usuarios"))
            {

            }
            return await familiaRepository.GetByIdAsync(id);
        }


        [HttpGet]
        [Route("ImportarFamilias")]
        public async Task<bool> ImportarFamilias()
        {
            var reader = new StreamReader(System.IO.File.OpenRead(@"c:\repos\gastos\csv\Familias.csv"));
            bool status = false;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                if (!values[0].Equals("Familia"))
                {

                    Familia familia = new Familia();
                    familia.Nombre = values[0].ToString();
                    familia.IdFamiliaPadre = null;

                    await familiaRepository.CreateAsync(familia);
                }
            }
            await familiaRepository.SaveAsync();
            return status;
        }
    }
}
