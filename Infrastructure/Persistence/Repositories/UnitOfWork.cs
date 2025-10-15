using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {

        private readonly Dictionary<Type, object> _repositories = new();

      

        public IGenericRepository<TEntity,TKey> GetRepository<TEntity,TKey>() where TEntity : BaseEntity<TKey>
        {
            if(_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IGenericRepository<TEntity,TKey>)_repositories[typeof(TEntity)];
            }
            var repository = new GenericRepository<TEntity,TKey>(_dbContext);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }





        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }


        public async Task<int> SaveChangesAsync()
        {
           return await _dbContext.SaveChangesAsync();
        }

       
    }
}
