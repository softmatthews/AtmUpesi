using System.Linq.Expressions;

namespace Domain.Repositories
{
    /// <summary>
    /// Generic methods that every repository for entities must implement
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetAsync(Guid id);
        Task<TEntity?> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        IRepository<TEntity> IncludeAsync(Expression<Func<TEntity, object>> includeExpression);
        Task<double> CountAsync(Expression<Func<TEntity, bool>>? predicate);
        Task<DateTime?> MaxAsync(Expression<Func<TEntity, DateTime>> columnSelector);
        Task<DateTime?> MinAsync(Expression<Func<TEntity, DateTime>> columnSelector);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}