using System.ComponentModel.DataAnnotations;

namespace Ampol.Domain.Entities
{
    public class DiscountPromotion
    {
        [Key]
        public string DiscountPromotionId { get; set; }
        public string PromotionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int DiscountPercent { get; set; }
    }
}