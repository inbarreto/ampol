using Application.Model;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.CalculatePurchase.Command
{
    public class PurchaseCommand : IRequest<PurchaseDto>
    {
        public Guid CustomerId { get; set; }
        public string LoyaltyCard { get; set; }

        public DateTime TransactionDate { get; set; }

        [JsonPropertyName("Basket")]
        public IEnumerable<Basket> BasketItems { get; set; } = Enumerable.Empty<Basket>();
    }

    public class Basket
    {
        public string ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}