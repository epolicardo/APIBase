using Configuration;
using Controllers;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Gastos.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DonantesController
    {
        private readonly IConfigurationHelper configHelper;
        private readonly DataContext dataContext;
        private readonly IGenericRepository<Donante> donanteRepository;
        private readonly IGenericRepository<Persona> personaRepository;

        public DonantesController(IConfigurationHelper configHelper, DataContext dataContext, IGenericRepository<Donante> donanteRepository, IGenericRepository<Persona> personaRepository)
        {
            this.configHelper = configHelper;
            this.dataContext = dataContext;
            this.donanteRepository = donanteRepository;
            this.personaRepository = personaRepository;
        }



        [HttpGet]
        [Route("GetList")]
        public IEnumerable<Donante> GetList()
        {
            LogContext.PushProperty("Metodo", MethodBase.GetCurrentMethod());
            LogContext.PushProperty("Server", Environment.MachineName);

            IEnumerable<Donante> donantes = (IEnumerable<Donante>)donanteRepository.GetAll();
            List<Donante> ListaDonantes = dataContext.Donantes.Include(x => x.Persona).ThenInclude(x=>x.Domicilio).ToList();
            Log.Information("Donantes: {@Donantes}", donantes);
            return donantes;

        }




        [HttpGet]
        [Route("GetById")]
        public IEnumerable<Donante> GetById(int id)
        {
            LogContext.PushProperty("Metodo", MethodBase.GetCurrentMethod());
            LogContext.PushProperty("Server", Environment.MachineName);

            IEnumerable<Donante> donantes = (IEnumerable<Donante>)donanteRepository.GetByIdAsync(id);
            Log.Information("Donantes: {@Donantes}", donantes);
            return donantes;

        }

        [HttpPost]
        [Route("Post")]
        public async Task<bool> Post(Donante donante)
        {
            LogContext.PushProperty("Metodo", MethodBase.GetCurrentMethod());
            LogContext.PushProperty("Server", Environment.MachineName);
            Log.Information("Donantes: {@Donantes}", donante);
            await personaRepository.CreateAsync(donante.Persona);
            await donanteRepository.CreateAsync(donante);
            await personaRepository.SaveAsync();
            return donanteRepository.SaveAsync().IsCompleted;

        }


    }
}

