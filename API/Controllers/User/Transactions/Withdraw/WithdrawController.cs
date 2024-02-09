using API.Setup.Filters;
using API.SignalR;
using Application.Features.User;
using Application.Features.User.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers.User.Transactions.Withdraw
{
    [LicenseCheck(Package = "Withdraw")]
    public class WithdrawController : BaseApiController
    {
        [HttpPost("withdraw")]
        public async Task<IActionResult> CreateWithdraw([FromBody] WithdrawalDto dto)
        {
            return HandleResult(await Mediator.Send(new Application.Features.User.Transactions.CreateWithdrawal.Command(dto)));
        }       

    }
}