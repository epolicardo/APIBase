using System;

namespace Data.Entities
{
    public class Persona : EntityBase
    {
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Celular { get; set; }
        public Domicilio Domicilio { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }

    public enum GrupoSanguineo
    {
        A,
        B,
        AB,
        O
    }

    public enum FactorSanguineo
    {
        Positivo,
        Negativo
    }
}