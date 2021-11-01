using Data;
using Data.Entities;
using Configuration;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TransaccionesController : ControllerBase
    {
        private readonly IGenericRepository<Transaccion> genericRepository;
        private readonly DataContext context;
        private readonly IConfigurationHelper configHelper;

        public TransaccionesController(IGenericRepository<Transaccion> genericRepository, DataContext dataContext, IConfigurationHelper configHelper)
        {
            this.genericRepository = genericRepository;
            this.context = dataContext;
            this.configHelper = configHelper;
        }


        [HttpPost]
        [Route("CreateAsync")]
        public async Task<bool> CreateAsync(Transaccion entity)
        {
            Ubicacion origen = new Ubicacion();
          
            entity.Origen = origen;

            entity.Origen.FechaAlta = DateTime.Now;
            return await genericRepository.CreateAsync(entity);

        }

        [HttpGet]
        [Route("GetByIdAsync")]
        public async Task<Transaccion> GetByIdAsync(int Id)
        {
            return await genericRepository.GetByIdAsync(Id);

        }
        [HttpGet]
        [Route("GetList")]
        public IEnumerable<Transaccion> GetList()
        {
            LogContext.PushProperty("Metodo", MethodBase.GetCurrentMethod());
            LogContext.PushProperty("Server", Environment.MachineName);
            IEnumerable<Transaccion> Transacciones = genericRepository.GetAll();
            Log.Information("Transacciones: {@Transacciones}", Transacciones);
            return Transacciones;

        }

    }

}
