using FluentValidation;

namespace Application.CalculatePurchase.Command
{
    public class PurchaseCommandValidator : AbstractValidator<PurchaseCommand>
    {
        public PurchaseCommandValidator()
        {
            RuleFor(v => v.CustomerId).NotEmpty();
            RuleFor(v => v.BasketItems).Must(BasketNotEmpty);
        }

        private bool BasketNotEmpty(IEnumerable<Basket> basketItems)
        {
            return basketItems.Any();
        }
    }
}