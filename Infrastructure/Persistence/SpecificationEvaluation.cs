using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    static class SpecificationEvaluation
    { 
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity>   inputQuery,ISpecification<TEntity,TKey> specification)where TEntity:BaseEntity<TKey>
        {
            IQueryable<TEntity> query = inputQuery;
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }
            if (specification.IncludeExpression != null && specification.IncludeExpression.Any())
            {

                query = specification.IncludeExpression.Aggregate(query, (current, include) => current.Include(include));

            }
            return query;
        }
    }
}
