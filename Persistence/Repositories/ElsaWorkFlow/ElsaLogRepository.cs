using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Elsa.Persistence.EntityFramework.SqlServer;
using System.Linq.Expressions;
using Domain.Core;
using MongoDB.Driver;
using Persistence.MongoDB;
using System.Text.RegularExpressions;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using LinqKit;
using System.Xml.Linq;
using Domain.Enums;
using Domain.Repositories.ElsaWorkFlow;
using Elsa.Persistence.EntityFramework.Core;

namespace Persistence.Repositories.ElsaWorkFlow
{

    public class ElsaLogRepository : ElsaRepository<Log>, IElsaRepository
    {

        public ElsaLogRepository(ElsaContext context) : base(context)
        {

        }
        public Task<Log?> FirstOrDefault(Expression<Func<Log, bool>> predicate)
        {
            return _entities.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<Log?> GetAllAsQueryable()
        {
            var result = _entities.AsQueryable();

            return result;
        }

        public async Task<(int count, IEnumerable<Log> logs)> FilterByQueriesPaginatedAsync(
               string? actor, string? package, string? feature, string? subfeature,
               int pageSize,
               int pageNumber)
        {
            var query = _entities.AsQueryable();

            if (!string.IsNullOrEmpty(actor))
                query = query.Where(x => x.Actor != null && x.Actor.Equals(actor));

            if (!string.IsNullOrEmpty(package))
                query = query.Where(x => x.Package != null && x.Package.Equals(package));

            if (!string.IsNullOrEmpty(feature))
                query = query.Where(x => x.Feature != null && x.Feature.Equals(feature));

            if (!string.IsNullOrEmpty(subfeature))
                query = query.Where(x => x.Subfeature != null && x.Subfeature.Equals(subfeature));

            var count = await query.CountAsync();
            var logs = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

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

        public async Task<IDictionary<string, double>> GetGroupedTransactionsByMessageTypesAsync(List<string> messageTypes)
        {
            return await _entities
                 .Where(transaction => messageTypes == null || !messageTypes.Any() || (transaction.Actor != null && messageTypes.Contains(transaction.Actor)))
                 .GroupBy(transaction => transaction.Package)
                 .Select(group => new { MessageType = group.Key, TransactionCount = group.Count() })
                 .ToDictionaryAsync(result => result.MessageType ?? string.Empty, result => (double)result.TransactionCount);

        }
    }

}