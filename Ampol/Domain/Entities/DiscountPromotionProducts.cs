namespace Ampol.Domain.Entities
{
    public class DiscountPromotionProducts
    {
        public int Id { get; set; }
        public string DiscountPromotionId { get; set; }

        public string ProductId { get; set; }

        public List<DiscountPromotion> DiscountPromotions { get; set; }
    }
}