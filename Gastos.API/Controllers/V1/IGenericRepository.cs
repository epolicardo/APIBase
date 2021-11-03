using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    public interface IGenericRepository<T> where T : class
    {

        IEnumerable<T> GetAll();

        Task<T> GetByIdAsync(int Id);
        Task<T> GetByNameAsync(string Name);

        bool Delete(T entity);

        Task<bool> CreateAsync(T entity);

        Task<bool> EditAsync(T entity);
        Task<int> SaveAsync();





    }
}
