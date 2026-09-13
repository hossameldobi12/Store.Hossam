using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        Expression<Func<TEntity, bool>>? Filteration { get; set; }
        List<Expression<Func<TEntity, object>>>? Includes { get; set; }
        public Expression<Func<TEntity, object>>? OrderBy { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDes { get; set; }
        int Skip { get; set; }
        int Take { get; set; }

        bool IsPaigination { get; set; }

        public void AddIncludes(Expression<Func<TEntity, object>> expression);
        public void AddOrderBy(Expression<Func<TEntity, object>> expression);

        public void AddPagination(int pageIndex, int PageSize);


        public void AddOrderByDes(Expression<Func<TEntity, object>> expression);


    }
}
