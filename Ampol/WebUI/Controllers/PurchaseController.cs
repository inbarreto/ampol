using Application.CalculatePurchase.Command;
using Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class PurchaseController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(PurchaseDto), StatusCodes.Status200OK)]
        public async Task<PurchaseDto> CalculatePurchase(PurchaseCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}