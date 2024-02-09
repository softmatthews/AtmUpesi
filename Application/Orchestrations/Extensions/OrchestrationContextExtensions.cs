using Application.Core;

namespace Application.Orchestrations.Extensions
{
    public static class OrchestrationContextExtensions
    {
        //public static async Task<T> ScheduleOrchestrationAndCheckResult<T>(this OrchestrationContext context, Type type, object parameter)
        //{
        //    var result = await context.CreateSubOrchestrationInstance<T>(type, parameter);

        //    if (result is Result<T> res && !res.IsSuccess)
        //    {
        //        throw new ApplicationException($"Task of type {type.Name} failed with error: {res.Error}");
        //    }

        //    return result;
        //}
    }

}
