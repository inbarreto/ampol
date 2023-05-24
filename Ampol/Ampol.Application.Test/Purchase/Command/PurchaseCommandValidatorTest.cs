using Application.CalculatePurchase.Command;
using FluentAssertions;
using FluentValidation;

namespace Ampol.Application.Test.Purchase.Command
{
    [TestFixture]
    public class PurchaseCommandValidatorTest
    {
        [Test]
        public void Should_NotPassValidation_When_EmptyBasket()
        {
            var validator = new PurchaseCommandValidator();
            var purchaseCommand = new PurchaseCommand()
            {
                CustomerId = Guid.NewGuid(),
                LoyaltyCard = "1234567890",
                TransactionDate = DateTime.Now,
                BasketItems = new List<Basket>()
                
            };
            validator.Validate(purchaseCommand).IsValid.Should().BeFalse();
        }

        [Test]
        public void Should_PassValidation_When_Basket()
        {
            var validator = new PurchaseCommandValidator();
            var purchaseCommand = new PurchaseCommand()
            {
                CustomerId = Guid.NewGuid(),
                LoyaltyCard = "123",
                TransactionDate = DateTime.Now,
                BasketItems = new List<Basket>
                { new Basket
                    {
                        ProductId = "1234",
                        UnitPrice = 1.00m,
                        Quantity = 1
                    }
                }

            };
            validator.Validate(purchaseCommand).IsValid.Should().BeTrue();
        }
    }
}