using API.Setup.Filters;
using API.SignalR;
using Application.Features.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers.User.Transactions.Deposit
{
    [LicenseCheck(Package = "Deposit")]
    public class DepositController : BaseApiController
    {
        [HttpPost()]
        public async Task<IActionResult> CreateDeposit([FromBody] DepositDto dto)
        {
            return HandleResult(await Mediator.Send(new Application.Features.User.Transactions.CreateDeposit.Command(dto)));
        }       

    }
}