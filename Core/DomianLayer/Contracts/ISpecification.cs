using DomainLayer.Models;
using System.Linq.Expressions;

namespace DomainLayer.Contracts
{
    public interface ISpecification<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Expression<Func<TEntity, bool>>? Criteria { get; }
        List<Expression<Func<TEntity,object>>>? IncludeExpression { get; }
        Expression<Func<TEntity,object>> OrderBy { get; }   
        Expression<Func<TEntity,object>> OrderByDescending { get; }

         int Take{ get;  }
         int Skip { get;  }
         bool IsPagingEnabled { get; }


    }
}
