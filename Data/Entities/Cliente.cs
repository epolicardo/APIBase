using Data.Entities;
using System;

namespace Data.Entities
{
    public class Cliente : EntityBase
    {
        public Persona Persona { get; set; }
        public decimal Saldo { get; set; }
        public DateTime UltimoRegistro { get; set; }
        public Estado Estado { get; set; }


        public TipoCliente TipoCliente { get; set; }
        public int ListaPrecioAsociada { get; set; } //Permitiria utilizar distintos tipos de lista de precios segun cliente
    }

    public enum Estado
    {
        Activo = 1,
        Inactivo = 0
    }
    public enum TipoCliente
    {
        ConsumidorFinal = 1,
        Mayorista = 2,
        Empleado = 3,
        Informal = 4
    }
}
