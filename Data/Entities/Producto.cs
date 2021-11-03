using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Producto : EntityBase
    {

        public string Codigo { get; set; }
        public string EAN { get; set; }
        public string Nombre { get; set; } //Insecticida Raid Mata Pulgas y Garrapatas
        public string Descripcion { get; set; } //Insecticida Raid Mata Pulgas y Garrapatas Etiqueta Lila x 390cm3
        [MaxLength(40)]
        public string NombreFiscal { get; set; }//RAID MPyG 390cm3
        public string Marca { get; set; } //Raid
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; } //120.50       
        public bool EsPerecedero { get; set; } //True 
        public UnidadMedida UnidadMedida { get; set; }
        public Familia Familia { get; set; }
        public string URLImagen { get; set; }
        public decimal Cantidad { get; set; }
        public List<Proveedor> Proveedores { get; set; }


    }


}

public enum UnidadMedida
{
    Unidades = 0,
    Cm3 = 1,
    Grs = 2
}


