using DomainLayer.Contracts;
using DomainLayer.Models;
using System.Linq.Expressions;

namespace Service.Specifications
{
    public class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get;  private set; }
        public BaseSpecification(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }

        #region Include
        public List<Expression<Func<TEntity, object>>>? IncludeExpression { get; } = new List<Expression<Func<TEntity, object>>>();

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpression.Add(includeExpression);
        }
        #endregion


        #region Order

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }


        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
        #endregion


    }
}
