using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Domain.Core;
using Domain.Enums;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Linq.Expressions;
// using System.Text;
// using System.Threading.Tasks;
using Domain.Settings;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Persistence.MongoDB;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Domain.Repositories.Settings;

namespace Persistence.Repositories.Settings
{

    public class LogsRepository : Repository<Log>, ILogsRepository
    {

        public LogsRepository(DataContext context) : base(context)
        {

        }
        public IQueryable<Log?> GetAllAsQueryable()
        {
            var result = _entities.AsQueryable();

            return result;
        }

        public async Task<(int count, IEnumerable<Log> logs)> FilterByQueriesPaginatedAsync(
             string? actor, string? package, string? feature, string? subfeature, string? transactionId,
             int pageSize,
             int pageNumber)
        {
            var query = _entities.AsQueryable();

            if (!string.IsNullOrEmpty(actor))
                query = query.Where(x => x.Actor == actor);
            if (!string.IsNullOrEmpty(package))
                query = query.Where(x => x.Package == package);
            if (!string.IsNullOrEmpty(feature))
                query = query.Where(x => x.Feature == feature);
            if (!string.IsNullOrEmpty(subfeature))
                query = query.Where(x => x.Subfeature == subfeature);
            if (!string.IsNullOrEmpty(transactionId))
                query = query.Where(x => x.TransactionId == transactionId);

            var count = await query.CountAsync();

            var logs = await query
                    .OrderByDescending(x => x.TimeStamp)
                    .Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .ToListAsync();

            return (count, logs);
        }

        public static Expression<Func<T, bool>> BuildExpression<T>(QueryFilter filter)
        {
            var type = typeof(T);
            var property = type.GetProperty(filter.QueryCriteria) ?? throw new ArgumentException($"'{filter.QueryCriteria}' is not a property of type '{type}'.");
            var parameter = Expression.Parameter(type, "x");
            var propertyAccess = Expression.Property(parameter, property);
            var constantValue = Expression.Constant(Convert.ChangeType(filter.Value, property.PropertyType));

            Expression condition;
            switch (Enum.Parse<EQueryFilters>(filter.QueryType))
            {
                case EQueryFilters.LesserThan:
                    condition = Expression.LessThan(propertyAccess, constantValue);
                    break;
                case EQueryFilters.LesserThanOrEqual:
                    condition = Expression.LessThanOrEqual(propertyAccess, constantValue);
                    break;
                case EQueryFilters.GreaterThan:
                    condition = Expression.GreaterThan(propertyAccess, constantValue);
                    break;
                case EQueryFilters.GreaterThanOrEqual:
                    condition = Expression.GreaterThanOrEqual(propertyAccess, constantValue);
                    break;
                case EQueryFilters.Contains:
                    if (property.PropertyType != typeof(string))
                    {
                        throw new InvalidOperationException("The 'Contains' operation is only supported on properties of type 'string'.");
                    }
                    condition = Expression.Call(propertyAccess, "Contains", null, constantValue);
                    break;
                case EQueryFilters.Exact:
                    condition = Expression.Equal(propertyAccess, constantValue);
                    break;
                default:
                    throw new ArgumentException($"'{filter.QueryType}' is not a valid query type.");
            }

            return Expression.Lambda<Func<T, bool>>(condition, parameter);
        }

        public async Task<(int count, IEnumerable<Log> emails)> FilterByQueriesAsync(IEnumerable<QueryFilter> queries)
        {
            // Create query predicate using the query filter
            var predicate = PredicateBuilder.New<Log>();

            foreach (QueryFilter query in queries)
            {
                predicate = predicate.And(BuildExpression<Log>(query));

            }

            var data = await _entities
                    .Where(predicate)
                    .ToListAsync();

            return (data.Count, data);
        }


    }

}