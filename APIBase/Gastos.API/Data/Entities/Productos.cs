using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Productos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string  Descripcion { get; set; }
        public List<Proveedor> Proveedores{ get; set; }
        public double StockActual { get; set; }
        public double StockMinimo { get; set; }
        public double StockOptimo { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal UltimoPrecioCompra { get; set; }
        public int ListaPrecio { get; set; }

    }
}
