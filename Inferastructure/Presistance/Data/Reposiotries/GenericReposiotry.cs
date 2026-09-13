using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistance.Data.Contexts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Data.Reposiotries
{
    public class GenericReposiotry<TEntity, Tkey> : IGenericReposiotry<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext _Context;
        public GenericReposiotry(StoreDbContext storeDbContext)
        {
            _Context = storeDbContext;

        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool TrackedChange = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return TrackedChange ?
             (IEnumerable<TEntity>)await _Context.products.Include(p => p.ProductType).Include(p => p.ProductBrand).ToListAsync()
           : (IEnumerable<TEntity>)await _Context.products.Include(p => p.ProductType).Include(p => p.ProductBrand).AsNoTracking().ToListAsync();
            }
            return TrackedChange ?
                  await _Context.Set<TEntity>().ToListAsync()
                : await _Context.Set<TEntity>().AsNoTracking().ToListAsync();

        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _Context.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
            await _Context.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _Context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _Context.Set<TEntity>().Update(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications, bool TrackedChange = false)
        {
            return await ApplySaticSpecification(specifications).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> specifications)
        {

            return await ApplySaticSpecification(specifications).FirstOrDefaultAsync();
        }
        private IQueryable<TEntity> ApplySaticSpecification(ISpecifications<TEntity,Tkey> specifications)
        {
            return SpecificationEvaluator.GetQuery(_Context.Set<TEntity>(), specifications);
        }
    }
}
