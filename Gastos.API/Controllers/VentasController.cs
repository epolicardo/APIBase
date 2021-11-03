using Configuration;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controllers
{
    public class VentasController : ControllerBase
    {
        private readonly DataContext dataContext;
        private readonly IGenericRepository<Transaccion> genericRepository;
        private readonly IConfigurationHelper configHelper;
        public VentasController(IConfigurationHelper configurationHelper, DataContext dataContext, IGenericRepository<Transaccion> genericRepository)
        {
            this.configHelper = configurationHelper;
            this.dataContext = dataContext;
            this.genericRepository = genericRepository;
        }

        public IConfigurationHelper ConfigurationHelper { get; }

        public int SellsCreate()
        {
            if (configHelper.UseMockup("Ventas"))
            {
                return 1;
            }
            else
            {
                Transaccion transaccion = new Transaccion
                {
                    Tipo = TipoTransaccion.Venta,
                    FechaAlta = DateTime.Today,
                    FechaRealizado = DateTime.Today,
                    //Obtener la cuenta mediante metodo get
                    Cuenta = new Cuenta() { },
                    Descripcion = "Realizamos una venta" 

                };
                return 0;
            }
        }
    }
}
