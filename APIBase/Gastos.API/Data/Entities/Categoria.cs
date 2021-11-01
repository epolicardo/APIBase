using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    /// <summary>
    /// Entidad Categoria. Representa una categoria de los gastos
    /// </summary>
    public class Categoria : EntityBase
    {
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Padre")]
        public Categoria Padre { get; set; }



    }
}
