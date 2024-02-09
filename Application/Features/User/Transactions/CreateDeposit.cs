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
//using ChoETL;

namespace Application.Features.User.Transactions
{
    public class CreateDeposit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string AccountNumber= null!;
            public string IdNo = null!;
            public double Amount ;
            public string Currency = null!;
            public Command(DepositDto dto)
            {
                AccountNumber =dto.AccountNumber;
                IdNo =dto.IdNo;
                Amount= dto.Amount;
                Currency =dto.Currency;
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
               
                // Identify the withdrawal came from which media and handle it differently e.g  MPESA, WEB, APP
                string? type ="ATM"; //"APP,WEB, ATM,MPESA,BANK,
                WithdrawalDto withdrawalDto=new(){
                AccountNumber=request.AccountNumber,
                IdNo =request.AccountNumber,
                Amount= request.Amount,
                Currency =request.AccountNumber                
                };               
                
                List<Transaction> messages = new();

                switch (type)
                {
                    case "ATM":
                        await HandleATMWithdrawAsync(messages, withdrawalDto);
                        break;
                    case "MPESA":
                        await HandleMPESAWithdrawAsync(messages, withdrawalDto);
                        break;
                    case "APP":                        
                        break;
                }

                foreach (Transaction transaction in messages)
                {
                    
                        //save to a transaction DB prefferably a NoSQL DB
                         await _unitOfWork.Transaction.AddAsync(transaction);

                        // var result = //await _unitOfWork.WithdrawalsCollection("Withdrawal" + type).InsertOneJsonAsync(message.ToString());

                        // Update system
                        var transactionMessage = new TransactionMessage
                        {
                            TransactionID = "12", //result.Id!,
                            TransactionType = type,
                            TrafficType = "API", //result.TrafficType,
                            Group = ""//result.Group,
                        };

                        await _mediator.Publish(transactionMessage);
                        var workflow = await _launchpad.FindStartableWorkflowAsync("WithdrawWorkflow");
                        await _launchpad.DispatchStartableWorkflowAsync(workflow!, input: new Elsa.Models.WorkflowInput(transactionMessage));
                                              
                    
                }
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
        
            public async Task HandleATMWithdrawAsync(List<Transaction> messages, WithdrawalDto withdrawalDto)
            {
                //check if A/C exists
                //check A/C balance
                //add a/c balance with deposit amount

                //if A/C Balance is greater than withdrawal request amount proceed else throw and error
                
        Transaction transaction=new(){
            Amount=withdrawalDto.Amount,
            Currency=withdrawalDto.Currency,
            TransactionType="Deposit",
            Details="ATM, Deposit transaction. Amount deposited is"+withdrawalDto.Amount,
            Status=true,
            LastModifiedBy="user",
        };
       //public ICollection<AccountTransactions> AccountTransactions { get; set; } = new List<AccountTransactions>();    

                 messages.Add(transaction);               
            }

            public async Task HandleMPESAWithdrawAsync(List<Transaction> messages, WithdrawalDto withdrawalDto )
            {
            
            }
             public async Task HandleAPPWithdrawAsync(List<Transaction> messages, WithdrawalDto withdrawalDto)
            {
            
            }

            }                    
                                    
        }
}