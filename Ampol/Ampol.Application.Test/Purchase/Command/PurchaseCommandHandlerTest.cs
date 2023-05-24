using Application.CalculatePurchase.Command;
using Application.Exceptions;
using FluentAssertions;

namespace Ampol.Application.Test.Purchase.Command
{
    [TestFixture]
    public class PurchaseCommandHandlerTest : DbContextTestBase
    {
        [Test]
        public async Task Handle_GivenInvalidProductId_ShouldReturnBadRequestException()
        {
            // Arrange
            var sut = new PurchaseCommandHandler(_context);
            var command = new PurchaseCommand
            {
                CustomerId = Guid.NewGuid(),
                LoyaltyCard = "1234567890",
                TransactionDate = DateTime.Now,
                BasketItems = new List<Basket>
                {
                    new Basket
                    {
                        ProductId = "1234",
                        UnitPrice = 1.00m,
                        Quantity = 1
                    }
                }
            };
            // Act

            Assert.ThrowsAsync<BadRequestException>(async () => await sut.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task Handle_GivenValidProducts_ShouldReturnDto()
        {
            // Arrange
            var sut = new PurchaseCommandHandler(_context);
            var command = new PurchaseCommand
            {
                CustomerId = Guid.NewGuid(),
                LoyaltyCard = "1234567890",
                TransactionDate = DateTime.Now,
                BasketItems = new List<Basket>
                {
                    new Basket
                    {
                        ProductId = "PRD01",
                        UnitPrice = 1.00m,
                        Quantity = 3
                    }
                }
            };

            var result = await sut.Handle(command, CancellationToken.None);

            result.DiscountApplied.Should().Be(12);
            result.GrandTotal.Should().Be(60);
            result.PointsEarned.Should().Be(120);
            result.TotalAmount.Should().Be(48);
        }

        [Test]
        public async Task Handle_GivenValidProducts_ShouldReturnValidDto()
        {
            // Arrange
            var sut = new PurchaseCommandHandler(_context);
            var command = new PurchaseCommand
            {
                CustomerId = Guid.NewGuid(),
                LoyaltyCard = "1234567890",
                TransactionDate = DateTime.Now,
                BasketItems = new List<Basket>
                {
                    new Basket
                    {
                        ProductId = "PRD01",
                        UnitPrice = 1.00m,
                        Quantity = 3
                    },
                     new Basket
                    {
                        ProductId = "PRD02",
                        UnitPrice = 1.00m,
                        Quantity = 1
                    }
                }
            };

            var result = await sut.Handle(command, CancellationToken.None);

            result.DiscountApplied.Should().Be(14);
            result.GrandTotal.Should().Be(70);
            result.PointsEarned.Should().Be(140);
            result.TotalAmount.Should().Be(56);
        }
    }
}