using Microsoft.EntityFrameworkCore;
using OrderMangement.Infrastructure.Data;
using OrederManagement.Core.Entities;
using OrederManagement.Core.Repository.Contract;
using OrederManagement.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMangement.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly OrderMangementDBContxt _dbcontext;

        public GenericRepository(OrderMangementDBContxt dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<int> AddAsync(T entity)
        {
            await _dbcontext.Set<T>().AddAsync(entity);
            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsyncWithSpec(ISpecifiction<T> specifiction)
        {
            return await SpecifictionEvalutor<T>.GetQuery(_dbcontext.Set<T>(), specifiction).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
           return await _dbcontext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsyncWithSpec(ISpecifiction<T> specifiction)
        {
            return await SpecifictionEvalutor<T>.GetQuery(_dbcontext.Set<T>(), specifiction).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _dbcontext.Set<T>().Update(entity);
            return await _dbcontext.SaveChangesAsync();
        }
    }
}
