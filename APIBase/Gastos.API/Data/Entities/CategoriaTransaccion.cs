namespace Data.Entities
{
    public class CategoriaTransaccion : EntityBase
    {
        public Categoria IdCategoria { get; set; }
        public Transaccion IdTransaccion { get; set; }
    }
}
