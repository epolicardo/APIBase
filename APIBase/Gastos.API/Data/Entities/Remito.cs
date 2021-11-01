using System.Collections.Generic;

namespace Data.Entities
{
    public class Remito : EntityBase
    {

        public Comprobante Comprobante { get; set; }
        public List<Producto> Productos { get; set; }
        public Proveedor Proveedor { get; set; }

    }
}
