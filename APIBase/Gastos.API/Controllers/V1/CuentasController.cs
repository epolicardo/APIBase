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
    public class CuentasController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IGenericRepository<Cuenta> genericRepository;

        public CuentasController(DataContext _context, IGenericRepository<Cuenta> _genericRepository, IConfigurationHelper configHelper)
        {
            context = _context;
            genericRepository = _genericRepository;
            ConfigHelper = configHelper;
        }

        public IConfigurationHelper ConfigHelper { get; }

        [HttpPost]
        [Route("CreateAsync")]
        public async Task<bool> CreateAsync(Cuenta entity)
        {
            TipoCuenta tipoCuenta = new TipoCuenta
            {
                FechaAlta = DateTime.Today,
                Nombre = "Caja Ahorro",
                UltimaModificacion = DateTime.Now

            };

            TipoMoneda tipoMoneda = new TipoMoneda
            {
                Nombre = "Pesos",
                Simbolo = "ARS",
                Equivalencia = 1
            };
            Institucion institucion = new Institucion
            {
                Titulo = "Banco de Cordoba",
                FechaAlta = DateTime.Now,
                UltimaModificacion = DateTime.Now
            };

            Cuenta cuenta = new Cuenta
            {
                Activa = true,
                FechaAlta = DateTime.Now,
                SaldoInicial = 0,
                Descripcion = "Caha de Ahorro Universal",
                Moneda = tipoMoneda,
                Nombre = "Bancor",
                Institucion = institucion,
                UltimaModificacion = DateTime.Now,
                SaldoActual = 0,
                TipoCuenta = tipoCuenta
            };

            return await genericRepository.CreateAsync(cuenta);

        }

        [HttpGet]
        [Route("GetByIdAsync")]
        public async Task<Cuenta> GetByIdAsync(int Id)
        {

            if (ConfigHelper.UseMockup("Debug_Cuentas"))
            {
                Cuenta cuenta = new Cuenta
                {
                    Activa = true
                };
                return cuenta;
            }
            return await genericRepository.GetByIdAsync(Id);

        }
        [HttpGet]
        [Route("GetList")]
        public IEnumerable<Cuenta> GetList()
        {
            return genericRepository.GetAll();

        }

    }
}
