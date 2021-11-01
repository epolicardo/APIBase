using System;

namespace Data.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? UltimaModificacion { get; set; }
    }
}