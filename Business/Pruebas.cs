using Data;
using Data.Entities;

namespace Business
{
    public class Pruebas<T> where T : class
    {
        private readonly DataContext context;

        public Pruebas(DataContext context)
        {
            this.context = context;

        }

        public string hola(int Id)
        {

            context.Set<Transaccion>().Find(Id);

            return "";
        }
    }
}
