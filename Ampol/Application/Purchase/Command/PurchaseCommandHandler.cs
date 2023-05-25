using Ampol.Domain.Entities;
using Ampol.Persistence.Infrastructure;
using Application.Exceptions;
using Application.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CalculatePurchase.Command
{
    public class PurchaseCommandHandler : IRequestHandler<PurchaseCommand, PurchaseDto>
    {
        private readonly AmpolDbContext _context;

        public PurchaseCommandHandler(AmpolDbContext context)
        {
            _context = context;
        }

        public async Task<PurchaseDto> Handle(PurchaseCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<Basket> basket = request.BasketItems;
            decimal totalAmount = 0m;
            string[] basketItemsProductIds = basket.Select(b => b.ProductId).ToArray();
            List<ProductDetails> productDetailsList = _context.ProductDetails
                 .Where(pd => basketItemsProductIds.Contains(pd.ProductId))
                 .ToList();

            if (productDetailsList == null || productDetailsList.Count == 0)
            {
                throw new BadRequestException("Invalid Product Ids");
            }

            DiscountPromotion discountPromotions = await _context.DiscountPromotion
                .Where(dp => dp.StartDate <= request.TransactionDate && dp.EndDate >= request.TransactionDate)
                .SingleOrDefaultAsync();

            List<string> discountPromotionProductsId = new();
            if (discountPromotions != null)
            {
                discountPromotionProductsId = _context.DiscountPromotionProducts
                     .Where(dpp => string.Equals(dpp.DiscountPromotionId, discountPromotions.DiscountPromotionId))
                     .Select(d => d.ProductId)
                     .ToList();
            }

            PointsPromotion pointsPromotion = await _context.PointsPromotion
                .Where(pp => request.TransactionDate >= pp.StartDate && request.TransactionDate <= pp.EndDate).SingleOrDefaultAsync();

            decimal discountApplied = 0m;
            int pointsEarned = 0;
            foreach (var basketItem in basket)
            {
                ProductDetails productDetail = productDetailsList.First(pd => string.Equals(pd.ProductId, basketItem.ProductId));
                totalAmount += basketItem.Quantity * productDetail.UnitPrice;

                if (discountPromotionProductsId.Exists(p => p == basketItem.ProductId))
                {
                    discountApplied += basketItem.Quantity * productDetail.UnitPrice
                        * discountPromotions.DiscountPercent / 100;
                }

                if (pointsPromotion != null &&
                    (pointsPromotion.Category == Ampol.Domain.Enums.CategoryEnum.Any || pointsPromotion.Category == productDetail.Category))
                {
                    pointsEarned += (int)(productDetail.UnitPrice * basketItem.Quantity * pointsPromotion.PointsPerDollar);
                }
            }

            await _context.SaveChangesAsync();

            return new PurchaseDto()
            {
                CustomerId = request.CustomerId,
                DiscountApplied = discountApplied,
                GrandTotal = totalAmount,
                PointsEarned = pointsEarned,
                LoyaltyCard = request.LoyaltyCard,
                TotalAmount = totalAmount - discountApplied,
                TransactionDate = request.TransactionDate,
            };
        }
    }
}