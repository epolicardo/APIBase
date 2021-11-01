using System;

namespace Data.Entities
{
    public class Transaccion : EntityBase
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaRealizado { get; set; }
        public double Importe { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool Estado { get; set; } // Pendiente o Realizado
        public Cuenta Cuenta { get; set; }
        public string Observaciones { get; set; }
        public Repeticion Frecuencia { get; set; }
        public TipoTransaccion Tipo { get; set; }

        public bool RepetirPorFecha { get; set; }
        public bool ConsiderarEnEstadisticas { get; set; }
        public bool ConsiderarEnEconomiaMensual { get; set; }
        public bool IncorporarEnTotales { get; set; }
        public Ubicacion Origen { get; set; }
        public Ubicacion Destino { get; set; }
        public Usuario Usuario { get; set; }
    }


    public enum TipoTransaccion
    {
        Ingreso = 1,
        Egreso = 2,

        Transferencia = 3,
        Venta = 4, 
        Compra = 5, 
        Devolucion = 6, 
        Regalo = 7
    };
    public enum Repeticion
    {
        Ninguna = 1,
        Repetir = 2,
        TransaccionFija = 3
    };
}
