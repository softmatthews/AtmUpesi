using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Core.Behaviors
{
    public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> 
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }



        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            return await next();

            //try
            //{
            //    return await next();
            //}
            //catch (Exception ex)
            //{
            //    var requestName = typeof(TRequest).Name;

            //    _logger.LogError(ex, "KSuite Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

            //    throw;
            //}
        }
    }
}