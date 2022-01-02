using ElektronikGigantenLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public class GenericRepository<T, TDBContext> : IGenericRepository<T>
        where T : class
        where TDBContext : ElektronikGigantenContext
    {

        protected ElektronikGigantenContext _dbcontext;

        public GenericRepository(ElektronikGigantenContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }

        public async Task Insert(T obj)
        {
            _dbcontext.Add(obj);
            await Save();
        }
        public async Task Update(T obj)
        {
            _dbcontext.Update(obj);
            await Save ();
        }
        public async Task Delete(int id)
        {
            var entityToDelete = _dbcontext.Set<T>().Find(id);
            _dbcontext.Remove(entityToDelete);
            await Save();
        }
        public async Task Save()
        {
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public virtual async Task<bool> entityExists(int id)
        {
            var result =  await _dbcontext.Set<T>().FindAsync(id);
            if(result != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }
    }
}
