using API.Setup.Filters;
using API.SignalR;
using Application.Features.User;
using Application.Features.User.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers.User.Transactions.Account
{
    [LicenseCheck(Package = "Account")]
    public class BankAccountController : BaseApiController
    {
        [HttpPost()]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDto dto)
        {
            return HandleResult(await Mediator.Send(new Application.Features.User.Transactions.BankAccount.CreateAccount.Command(dto)));
        }       

    }
}