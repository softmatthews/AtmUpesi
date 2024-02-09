using Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CollectionRepository<TEntity> : ICollectionRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoCollection<TEntity> _collection;
        protected readonly IMongoDatabase _database;

        public CollectionRepository(IMongoDatabase database, IMongoCollection<TEntity> collection)
        {
            _database = database;
            _collection = collection;
        }
       Task<TEntity> ICollectionRepository<TEntity>.InsertOneJsonAsync(string json)
        {
            throw new NotImplementedException();
        }

        void ICollectionRepository<TEntity>.DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        Task ICollectionRepository<TEntity>.DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        void ICollectionRepository<TEntity>.DeleteMany(Expression<Func<TEntity, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        Task ICollectionRepository<TEntity>.DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        void ICollectionRepository<TEntity>.DeleteOne(Expression<Func<object[], bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        Task ICollectionRepository<TEntity>.DeleteOneAsync(Expression<Func<object[], bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        IEnumerable<TEntity> ICollectionRepository<TEntity>.FilterBy(Expression<Func<object[], bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        IEnumerable<TProjected> ICollectionRepository<TEntity>.FilterBy<TProjected>(Expression<Func<object[], bool>> filterExpression, Expression<Func<object[], TProjected>> projectionExpression)
        {
            throw new NotImplementedException();
        }

        Task<TEntity> ICollectionRepository<TEntity>.FindAnyAsync()
        {
            throw new NotImplementedException();
        }

        Task<TEntity?> ICollectionRepository<TEntity>.FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<TEntity> ICollectionRepository<TEntity>.FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        TEntity ICollectionRepository<TEntity>.FindOne(Expression<Func<object[], bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        Task<TEntity> ICollectionRepository<TEntity>.FindOneAsync(Expression<Func<object[], bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        void ICollectionRepository<TEntity>.InsertMany(ICollection<TEntity> documents)
        {
            throw new NotImplementedException();
        }

        Task ICollectionRepository<TEntity>.InsertManyAsync(ICollection<TEntity> documents)
        {
            throw new NotImplementedException();
        }

        Task ICollectionRepository<TEntity>.InsertManyDocsAsync(string json)
        {
            throw new NotImplementedException();
        }

        void ICollectionRepository<TEntity>.InsertOne(TEntity document)
        {
            throw new NotImplementedException();
        }

        async Task ICollectionRepository<TEntity>.InsertOneAsync(TEntity document)
        {
            await _collection.InsertOneAsync(document);
        }

        public Task<bool> UpdateSeenAsync(string id, TEntity document)
        {
            throw new NotImplementedException();
        }
    }
}
