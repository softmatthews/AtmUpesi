using Domain.Core;
using Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.ElsaWorkFlow
{
    public interface IElsaRepository : IElsaRepository<Log>
    {
        IQueryable<Log?> GetAllAsQueryable();
        Task<Log?> FirstOrDefault(Expression<Func<Log, bool>> predicate);
        Task<(int count, IEnumerable<Log> logs)> FilterByQueriesPaginatedAsync(
              string? actor, string? package, string? feature, string? subfeature,
              int pageSize,
              int pageNumber);
        Task<(int count, IEnumerable<Log> emails)> FilterByQueriesAsync(
               IEnumerable<QueryFilter> queries
            );
        public Task<IDictionary<string, double>> GetGroupedTransactionsByMessageTypesAsync(List<string> messageTypes);

    }

}