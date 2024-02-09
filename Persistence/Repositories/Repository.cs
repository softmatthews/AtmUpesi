using System.Linq.Expressions;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected readonly DbContext _context;
		protected DbSet<TEntity> _entities;
		protected IQueryable<TEntity> _query;

		public Repository(DbContext context)
		{
			_context = context;
			_entities = _context.Set<TEntity>();
			_query = _entities;
		}

		public async Task AddAsync(TEntity entity)
		{
			await _entities.AddAsync(entity);
		}

		public IRepository<TEntity> IncludeAsync(Expression<Func<TEntity, object>> includeExpression)
		{
			_query = _query.Include(includeExpression);
			return this;
		}

		public async Task AddRangeAsync(IEnumerable<TEntity> entities)
		{
			await _entities.AddRangeAsync(entities);
		}

		public async Task<TEntity?> GetAsync(Guid id)
		{
			return await _entities.FindAsync(id);
		}

		public async Task<TEntity?> GetAsync(int id)
		{
			return await _entities.FindAsync(id);
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _entities.ToListAsync();
		}

        
        public void Remove(TEntity entity)
		{
			_entities.Remove(entity);
		}

		public void RemoveRange(IEnumerable<TEntity> entities)
		{
			_entities.RemoveRange(entities);
		}

		public async Task<TEntity?> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
		{
			var result = await _query.SingleOrDefaultAsync(predicate);
			_query = _entities;
			return result;
		}

		public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			return _entities.Where(predicate);
		}

		public async Task<double> CountAsync(Expression<Func<TEntity, bool>>? predicate)
		{
			return predicate == null ?
		 		await _entities.CountAsync() :
		 		await _entities.Where(predicate).CountAsync();
		}

		public async Task<DateTime?> MaxAsync(Expression<Func<TEntity, DateTime>> columnSelector)
		{
			return await _entities.MaxAsync(columnSelector);
		}

		public async Task<DateTime?> MinAsync(Expression<Func<TEntity, DateTime>> columnSelector)
		{
			return await _entities.MinAsync(columnSelector);
		}
	}
}