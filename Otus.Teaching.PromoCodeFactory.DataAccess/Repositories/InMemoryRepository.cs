using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data;
        }
        
        protected IEnumerable<T> Data { get; set; }
        
        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task CreateAsync(T item)
        {
            Data = Data.Append(item);
            return Task.CompletedTask;
        }
        
        public Task UpdateAsync(T item)
        {
            Data = Data.Select(element => element.Id == item.Id ? item : element);
            
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            Data = Data.Where(element => element.Id != id);
            
            return Task.CompletedTask;
        }
    }
}