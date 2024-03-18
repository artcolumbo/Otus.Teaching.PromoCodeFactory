using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Domain;

namespace Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<T?> GetByIdAsync(Guid id);

        Task CreateAsync(T item);
        
        Task UpdateAsync(T item);

        Task DeleteAsync(Guid id);
    }
}