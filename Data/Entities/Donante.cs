namespace Data.Entities
{
    public class Donante :EntityBase
    {
        public Persona Persona { get; set; }
        public GrupoSanguineo Grupo { get; set; }
        public FactorSanguineo Factor { get; set; }
    }
}