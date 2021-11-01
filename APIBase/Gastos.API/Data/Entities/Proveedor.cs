using System.Collections.Generic;

namespace Data.Entities
{

    public class Proveedor : EntityBase
    {
        public string RazonSocial { get; set; }
        public string CUIL { get; set; }
        public string CondicionIVA { get; set; }
        public List<Producto> Productos { get; set; }
    }
}