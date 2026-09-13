using Domain.Models;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericReposiotry<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Task<IEnumerable<TEntity>> GetAllAsync(bool TrackedChange = false);
        public Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity,Tkey> specifications,bool TrackedChange = false);
        public Task<TEntity?> GetByIdAsync(int id);
        public Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> specifications);
        public Task AddAsync(TEntity entity);
        public void Delete(TEntity entity);
        public void Update(TEntity entity);
    }
}
