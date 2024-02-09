using Application.Core.Exceptions;
using Application.Core.Logging;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Core.Behaviors
{
    public class TransactionNotificationExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> 
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IMediator _mediator;

        public TransactionNotificationExceptionBehaviour(ILogger<TRequest> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {                

                return await next();
            }
            catch (Exceptions.TransactionNotificationException ex)
            {
                var requestName = typeof(TRequest).Name;

                throw;
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
               
                throw;
            }
        }
    }
}