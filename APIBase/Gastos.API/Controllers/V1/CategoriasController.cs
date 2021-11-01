using Configuration;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoriasController : ControllerBase
    {

        private readonly IGenericRepository<Categoria> genericRepository;
        private readonly DataContext context;
        private readonly IConfigurationHelper configHelper;

        public CategoriasController(IGenericRepository<Categoria> _genericRepository, DataContext _context, IConfigurationHelper configHelper)
        {
            genericRepository = _genericRepository;
            this.context = _context;
            this.configHelper = configHelper;
        }

        [HttpGet]
        [Route("GetByIdAsync")]
        public async Task<Categoria> GetByIdAsync(int Id)
        {
            if (configHelper.UseMockup("Categorias"))
            {
                Categoria categoria = new Categoria()
                {
                    FechaAlta = DateTime.Now,
                    UltimaModificacion = DateTime.Now,
                    Nombre = "MockCategroia"
                };
                return categoria;
            }
            return await genericRepository.GetByIdAsync(Id);

        }
        [HttpGet]
        [Route("GetList")]
        public IEnumerable<Categoria> GetList()
        {
            if (configHelper.UseMockup("Categorias"))
            {
                return null;
            }
            return genericRepository.GetAll();
        }

        [HttpPost]
        [Route("CreateAsync")]
        public async Task<bool> CreateAsync(Categoria entity)
        {
            if (configHelper.UseMockup("Categorias"))
            {
                return false;
            }
            return await genericRepository.CreateAsync(entity);

        }
    }
}
