using OrederManagement.Core.Entities;
using OrederManagement.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrederManagement.Core.Repository.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);

        Task<IReadOnlyList<T>> GetAllAsyncWithSpec(ISpecifiction<T> specifiction);
        Task<T> GetByIdAsyncWithSpec(ISpecifiction<T> specifiction);


    }
}
