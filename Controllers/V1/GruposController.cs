using Configuration;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GruposController : Controller
    {
        private readonly DataContext context;
        private readonly IGenericRepository<Grupo> _genericRepository;

        public IConfigurationHelper ConfigHelper { get; }

        public GruposController(IGenericRepository<Grupo> genericRepository, DataContext _context, IConfigurationHelper configHelper)
        {
            this.context = _context;
            ConfigHelper = configHelper;
            this._genericRepository = genericRepository;

        }

        [HttpGet]
        [Route("GetByIdAsync")]
        public async Task<Grupo> GetByIdAsync(int Id)
        {

            Grupo grupo = await _genericRepository.GetByIdAsync(Id);
            return grupo;

        }

        [HttpGet]
        [Route("GetList")]
        public List<Grupo> GetList()
        {
            Log.Information("GetList");
            return context.Grupos.Include(x => x.Integrantes).ToList();



        }


        /// <summary>
        /// Obtiene los grupos a los que pertenece un usuario
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetListByUser")]
        public IEnumerable<Grupo> GetListByUser(string IdUsuario)
        {
            return context.Grupos.ToList();
        }

        [HttpPost]
        [Route("CreateAsync")]
        public async Task<bool> CreateAsync(Grupo entity)
        {
            entity.FechaAlta = DateTime.Now;
            entity.UltimaModificacion = DateTime.Now;
            entity.UGI = Guid.NewGuid().ToString();
            return await _genericRepository.CreateAsync(entity);

        }



    }
}
