namespace Data.Entities
{
    public class Familia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdFamiliaPadre { get; set; }
    }
}