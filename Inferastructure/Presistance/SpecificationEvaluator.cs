using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity, Tkey>
            (IQueryable<TEntity> inputQuery, ISpecifications<TEntity, Tkey> specifications)
            where TEntity : BaseEntity<Tkey>
        {
            var query = inputQuery;
            if (specifications.Filteration is not null)
                query = query.Where(specifications.Filteration);
            if (specifications.OrderBy != null)
                query = query.OrderBy(specifications.OrderBy);
            else if (specifications.OrderByDes != null)
                query = query.OrderByDescending(specifications.OrderByDes);

            if (specifications.IsPaigination)
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            query = specifications.Includes?.Aggregate(query, (CurrentQuery, icludeExpression) => CurrentQuery.Include(icludeExpression));

            return query;
        }
    }
}
