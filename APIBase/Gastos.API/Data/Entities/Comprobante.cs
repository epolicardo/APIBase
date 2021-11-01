namespace Data.Entities
{
    public class Comprobante : EntityBase
    {
        public TipoComprobante TipoComprobante { get; set; }
        public string PrefijoComprobante { get; set; }
        public string NroComprobante { get; set; }
        public Usuario Generacion { get; set; }
        public Usuario Modificacion { get; set; }

    }
}