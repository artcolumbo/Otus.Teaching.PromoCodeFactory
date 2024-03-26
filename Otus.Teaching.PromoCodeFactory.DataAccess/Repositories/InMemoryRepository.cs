using System;
using System.Collections;
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
        public InMemoryRepository(IList<T> data)
        {
            Data = data;
        }
        
        protected IList<T> Data { get; set; }
        
        public Task<IList<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(element => element.Id == id));
        }

        public Task CreateAsync(T item)
        {
            Data.Add(item);
            return Task.CompletedTask;
        }
        
        public Task UpdateAsync(T item)
        {
            var index = Data.IndexOf(Data.Where(element => element.Id == item.Id)!.FirstOrDefault()!);
            if (index != -1)
                Data[index] = item;
            
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            Data.Remove(Data.Where(element => element.Id == id)!.FirstOrDefault()!);
            return Task.CompletedTask;
        }
    }
}