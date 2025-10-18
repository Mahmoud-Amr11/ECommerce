using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

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
            if(specification.OrderBy !=null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            if(specification.OrderByDescending !=null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }
            if (specification.IncludeExpression != null && specification.IncludeExpression.Any())
            {

                query = specification.IncludeExpression.Aggregate(query, (current, include) => current.Include(include));

            }
            return query;
        }
    }
}
