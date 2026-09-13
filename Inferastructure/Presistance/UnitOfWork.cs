using Domain.Contracts;
using Domain.Models;
using Presistance.Data.Contexts;
using Presistance.Data.Reposiotries;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Presistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _Context;
        //private readonly Dictionary<string, object> _reposiotries;

        private readonly ConcurrentDictionary<string, object> _reposiotries;
        public UnitOfWork(StoreDbContext Context)
        {
            _Context = Context;
            //reposiotries = new Dictionary<string, object>();
            _reposiotries = new ConcurrentDictionary<string, object>();
        }
        //public IGenericReposiotry<TEntity, Tkey> GetReposiotry<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        //{
        //    var type = typeof(TEntity).Name;
        //    if (!_reposiotries.ContainsKey(type))
        //    {
        //        _reposiotries[type] = new GenericReposiotry<TEntity, Tkey>(_Context);
        //    }
        //    return (IGenericReposiotry<TEntity, Tkey>)_reposiotries[type];
        //}

        public IGenericReposiotry<TEntity, Tkey> GetReposiotry<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            return (IGenericReposiotry<TEntity, Tkey>)_reposiotries.GetOrAdd(typeof(TEntity).Name, new GenericReposiotry<TEntity, Tkey>(_Context));
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _Context.SaveChangesAsync();
        }
    }
}
