namespace Data.Entities
{
    public class Domicilio :EntityBase
    {
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Piso { get; set; }
        public string Departamento { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
        public string CodigoPostal { get; set; }

    }
}