using Data;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controllers
{


    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext context;

        public GenericRepository(DataContext _context)

        {
            this.context = _context;

        }


        public IEnumerable<T> GetList()
        {

            return context.Set<T>().ToList();
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            return await context.Set<T>().FindAsync(Id);
        }
        public async Task<T> GetByNameAsync(string Name)
        {
            return await context.Set<T>().FindAsync(Name);
        }

        public bool Delete(T entity)
        {
            context.Set<T>().Remove(entity);

            return true;
        }

        public Task<bool> CreateAsync(T entity)
        {

            var a =  context.Set<T>().AddAsync(entity).IsCompletedSuccessfully;
            //await context.SaveChangesAsync();

            return Task.FromResult(a);

        }

        public IEnumerable<T> GetAll()
        {
            try
            {

            return context.Set<T>().ToList();
            }
            catch (Exception ex)
            {

                Log.Error(ex, "error al obtener listas");
                return null;
            }
        }

        public Task<bool> EditAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
