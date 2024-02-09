using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Domain.Settings;

namespace Domain.Repositories.Settings
{
    public interface ILogsRepository : IRepository<Log>
    {
        IQueryable<Log?> GetAllAsQueryable();
        Task<(int count, IEnumerable<Log> logs)> FilterByQueriesPaginatedAsync(
                    string? actor, string? package, string? feature, string? subfeature, string? transactionId,
                    int pageSize,
                    int pageNumber);
        Task<(int count, IEnumerable<Log> emails)> FilterByQueriesAsync(
                     IEnumerable<QueryFilter> queries
                );

    }

}