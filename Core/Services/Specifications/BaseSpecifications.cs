using Domain.Models;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>>? Filteration { get; set; }
        public List<Expression<Func<TEntity, object>>>? Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>>? OrderBy { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDes { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaigination { get; set; }

        public BaseSpecifications(Expression<Func<TEntity, bool>>? expression)
        {
            Filteration = expression;
        }
        public void AddIncludes(Expression<Func<TEntity, object>> expression)
        {
            Includes?.Add(expression);
        }
        public void AddOrderBy(Expression<Func<TEntity, object>> expression)
        {
            OrderBy = expression;
        }
        public void AddOrderByDes(Expression<Func<TEntity, object>> expression)
        {
            OrderByDes = expression;
        }
        public void AddPagination(int pageIndex, int PageSize)
        {
            IsPaigination = true;
            Skip = (pageIndex - 1) * PageSize;
            Take = PageSize;
        }

    }
}
