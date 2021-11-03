using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class ProductosController : Controller
    {
        private readonly IGenericRepository<Producto> genericRepository;
        private readonly IGenericRepository<Familia> genericRepositoryFamilia;
        private readonly DataContext dataContext;

        public ProductosController(IGenericRepository<Producto> genericRepository, IGenericRepository<Familia> genericRepositoryFamilia, DataContext dataContext)
        {
            this.genericRepository = genericRepository;
            this.genericRepositoryFamilia = genericRepositoryFamilia;
            this.dataContext = dataContext;
        }

        [HttpGet]
        [Route("GetList")]
        public IEnumerable<Producto> GetList()
        {
            LogContext.PushProperty("Metodo", MethodBase.GetCurrentMethod());
            LogContext.PushProperty("Server", Environment.MachineName);
            
            IEnumerable<Producto> Productos = genericRepository.GetAll();
            Log.Information("Productos: {@Productos}", Productos);
            return Productos;

        }

        [HttpPost]
        [Route("PostProducto")]
        public async Task<bool> PostProducto(Producto producto)
        {
            LogContext.PushProperty("Metodo", MethodBase.GetCurrentMethod());
            LogContext.PushProperty("Server", Environment.MachineName);
            Log.Information("Productos: {@Productos}", producto);
            await genericRepository.CreateAsync(producto);

            return genericRepository.SaveAsync().IsCompleted;


        }

        [HttpGet]
        [Route("ImportarProductos")]
        public Task<bool> ImportarProductos()
        {
            var reader = new StreamReader(System.IO.File.OpenRead(@"c:\repos\gastos\csv\productos.csv"));
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            bool status = false;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                if (!values[0].ToUpper().Equals("CODIGO"))
                {

                    Producto producto = new Producto();
                    producto.FechaAlta = DateTime.Now;
                    producto.UltimaModificacion = DateTime.Now;

                    producto.Codigo = values[0].ToString();
                    producto.EAN = values[1].ToString();
                    producto.Nombre = values[2].ToString();
                    producto.Descripcion = values[3].ToString();
                    producto.NombreFiscal = values[4].ToString();

                    producto.Marca = values[5].ToString();
                    producto.Precio = Convert.ToDecimal(values[6].ToString());
                    producto.EsPerecedero = values[7].ToString() == "NO" ? false : true;
                    switch (values[8].ToString())
                    {
                        case "Unidad":
                            producto.UnidadMedida = UnidadMedida.Unidades;
                            break;
                        case "Grs":
                            producto.UnidadMedida = UnidadMedida.Grs;
                            break;
                        case "Cm3":
                            producto.UnidadMedida = UnidadMedida.Cm3;
                            break;
                        default:
                            producto.UnidadMedida = UnidadMedida.Unidades;
                            break;
                    }
                    Log.Debug(producto.EAN);

                    dataContext.Productos.Add(producto);
                }


            }
            dataContext.SaveChanges();
            return Task.FromResult(status);
        }
    }
}
