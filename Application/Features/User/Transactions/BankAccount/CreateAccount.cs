using Application.Core;
using Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ValidationException = Application.Core.Exceptions.ValidationException;
using Elsa.Services;
using Microsoft.Extensions.Logging;
using Application.Core.Notifications;
using Application.Core.Logging;
using Application.Interfaces.Settings;
using Domain.Enums;
using System.Collections;
using Application.Orchestrations.Transactions;
using Domain.Core;
using System.Drawing.Printing;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using Esprima;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Domain.Settings;
using Application.Core.Exceptions;
using Domain.User.Transactions;
using Domain.User.Account;
//using ChoETL;

namespace Application.Features.User.Transactions.BankAccount
{
    public class CreateAccount
    {
        public class Command : IRequest<Result<Unit>>
        {           
             public AccountDto dtoAccount { get; set; } 
            public string Name { get; }
            public bool Status { get; private set; }
            public string LastModifiedBy { get; }
            public double Amount ;
            public string Currency = null!;

            public Command(AccountDto dto)
            {
                 dtoAccount=dto;
                Name = dto.Name;
                Amount= dto.Amount;
                Currency= dto.Currency;
                Status = dto.Status;
                LastModifiedBy = "Ad";
            }

        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {

            private readonly IUnitOfWork _unitOfWork;
            private readonly IMediator _mediator;
            private readonly ILogger<Handler> _logger;
            private readonly IWorkflowLaunchpad _launchpad;
                       public Handler(IUnitOfWork unitOfWork, IMediator mediator, ILogger<Handler> logger,
                IWorkflowLaunchpad launchpad)
            {               
                _unitOfWork = unitOfWork;
                _logger = logger;
                _mediator = mediator;
                _launchpad = launchpad;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {

                using var scope = _logger.BeginScope(new Dictionary<string, object>
                {
                    ["Actor"] = "SYSTEM",
                    ["Package"] = "Transaction",
                    ["Feature"] = "WITHDRAW"
                });
                List<ValidationFailure> failures = new();

                try { 
              
                await _unitOfWork.Account.AddAsync(new Account
                        {
                Name = request.dtoAccount.Name,
                Amount=request.dtoAccount.Amount,
                AccountNumber=request.dtoAccount.AccountNumber,
                Currency= request.dtoAccount.Currency,
                Status = request.dtoAccount.Status,
                LastModifiedBy = "Ad",
                        });
                var success = await _unitOfWork.Complete();
                }
                catch (Exception ex)
                {
                    failures.Add(new ValidationFailure("Setting", $"Error '{1}' and is discarded - {ex.Message}"));
                }

                if (failures.Count == 0)
                {
                    return Result<Unit>.Success(Unit.Value);
                }
                else
                {
                    return Result<Unit>.Failure(string.Join("\n", failures.Select(f => f.ErrorMessage).ToList()));
                }
                return Result<Unit>.Success(Unit.Value);
            }
    
            }                    
                                    
        }
}