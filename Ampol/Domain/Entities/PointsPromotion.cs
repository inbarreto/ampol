using Ampol.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ampol.Domain.Entities
{
    public class PointsPromotion
    {
        [Key]
        public string PointsPromotionId { get; set; }
        public string PromotionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CategoryEnum Category { get; set; }
        public int PointsPerDollar { get; set; }
    }
}