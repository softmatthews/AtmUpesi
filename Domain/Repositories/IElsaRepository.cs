using System.Linq.Expressions;

namespace Domain.Repositories
{
    /// <summary>
    /// Generic methods that every repository for elsa entities must implement
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IElsaRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        IElsaRepository<TEntity> Include(Expression<Func<TEntity, object>> includeExpression);
        Task<double> CountAsync(Expression<Func<TEntity, bool>>? predicate);
        Task<DateTime?> MaxAsync(Expression<Func<TEntity, DateTime>> columnSelector);
        Task<DateTime?> MinAsync(Expression<Func<TEntity, DateTime>> columnSelector);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}