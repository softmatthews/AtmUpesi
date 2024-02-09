using System.Linq.Expressions;
using Domain.Core;

namespace Domain.Repositories
{
    public interface ICollectionRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> FilterBy(
            Expression<Func<object[], bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
                Expression<Func<object[], bool>> filterExpression,
                Expression<Func<object[], TProjected>> projectionExpression);

        TEntity FindOne(Expression<Func<object[], bool>> filterExpression);
        Task<TEntity> FindOneAsync(Expression<Func<object[], bool>> filterExpression);
        Task<TEntity> FindAnyAsync();
        Task<TEntity?> FindById(Guid id);
        Task<TEntity> FindByIdAsync(Guid id);
        Task<TEntity> InsertOneJsonAsync(string json);
        Task InsertManyDocsAsync(string json);
        void InsertOne(TEntity document);
        Task InsertOneAsync(TEntity document);
        void InsertMany(ICollection<TEntity> documents);
        Task InsertManyAsync(ICollection<TEntity> documents);
        void DeleteOne(Expression<Func<object[], bool>> filterExpression);
        Task DeleteOneAsync(Expression<Func<object[], bool>> filterExpression);
        void DeleteById(Guid id);
        Task DeleteByIdAsync(Guid id);
        void DeleteMany(Expression<Func<TEntity, bool>> filterExpression);
        Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression);
    }
}