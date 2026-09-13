using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        public  Task<int> SaveChangeAsync();
        public IGenericReposiotry<TEntity, Tkey> GetReposiotry<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>;
    }

}
