using Application.CalculatePurchase.Command;
using Application.Model;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace WebUI.Controllers
{
    public class PurchaseController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(PurchaseDto), StatusCodes.Status200OK)]
        public async Task<PurchaseDto> CalculatePurchase(PurchaseCommand command)
        {
            var validator = new PurchaseCommandValidator();
            
            var validateC =  validator.Validate(command);

            if (!validateC.IsValid)
                throw new Ampol.Application.Exceptions.ValidationException(validateC.Errors);
            
            return await Mediator.Send(command);
        }
    }
}