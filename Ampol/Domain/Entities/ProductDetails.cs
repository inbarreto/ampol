using Ampol.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ampol.Domain.Entities
{
    public class ProductDetails
    {
        [Key]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public CategoryEnum Category { get; set; }
        public decimal UnitPrice { get; set; }
    }
}