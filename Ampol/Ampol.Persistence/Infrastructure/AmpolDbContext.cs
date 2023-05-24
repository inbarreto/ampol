using Ampol.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ampol.Persistence.Infrastructure
{
    public class AmpolDbContext : DbContext
    {
        public AmpolDbContext(DbContextOptions<AmpolDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Ampol");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedProductDetails(modelBuilder);
            SeedPointsPromotion(modelBuilder);
            SeedDiscountPromotionProducts(modelBuilder);
            SeedDiscountPromotion(modelBuilder);
        }

        public DbSet<ProductDetails> ProductDetails { get; set; }

        public DbSet<DiscountPromotion> DiscountPromotion { get; set; }
        public DbSet<DiscountPromotionProducts> DiscountPromotionProducts { get; set; }
        public DbSet<PointsPromotion> PointsPromotion { get; set; }

        private static void SeedProductDetails(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDetails>().HasData(
                 new ProductDetails()
                 {
                     ProductId = "PRD01",
                     ProductName = "Vortex 95",
                     Category = Domain.Enums.CategoryEnum.Fuel,
                     UnitPrice = 20m
                 },
            new ProductDetails()
            {
                ProductId = "PRD02",
                ProductName = "Vortex 98",
                Category = Domain.Enums.CategoryEnum.Fuel,
                UnitPrice = 10m
            },
            new ProductDetails()
            {
                ProductId = "PRD03",
                ProductName = "Diesel",
                Category = Domain.Enums.CategoryEnum.Fuel,
                UnitPrice = 30m
            },
            new ProductDetails()
            {
                ProductId = "PRD04",
                ProductName = "Twix 55g",
                Category = Domain.Enums.CategoryEnum.Shop,
                UnitPrice = 150m
            }
            );
        }

        private static void SeedPointsPromotion(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PointsPromotion>().HasData(
            new PointsPromotion()
            {
                PointsPromotionId = "PP001",
                PromotionName = "New Year Promo",
                StartDate = DateTime.Parse("01/01/2020"),
                EndDate = DateTime.Parse("31/01/2024"),
                Category = Domain.Enums.CategoryEnum.Any,
                PointsPerDollar = 2
            },
            new PointsPromotion()
            {
                PointsPromotionId = "PP002",
                PromotionName = "Fuel Promo",
                StartDate = DateTime.Parse("05/02/2020"),
                EndDate = DateTime.Parse("15/02/2020"),
                Category = Domain.Enums.CategoryEnum.Fuel,
                PointsPerDollar = 3
            },
            new PointsPromotion()
            {
                PointsPromotionId = "PP003",
                PromotionName = "Shop Promo",
                StartDate = DateTime.Parse("01/03/2020"),
                EndDate = DateTime.Parse("20/03/2020"),
                Category = Domain.Enums.CategoryEnum.Shop,
                PointsPerDollar = 4
            }
            );
        }

        private static void SeedDiscountPromotionProducts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiscountPromotionProducts>().HasData(
             new DiscountPromotionProducts()
             {
                 Id = 1,
                 DiscountPromotionId = "DP001",
                 ProductId = "PRD01",
             },
            new DiscountPromotionProducts()
            {
                Id = 2,
                DiscountPromotionId = "DP001",
                ProductId = "PRD02",
            }
            );
        }

        private static void SeedDiscountPromotion(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiscountPromotion>().HasData(
            new DiscountPromotion()
            {
                DiscountPromotionId = "DP001",
                PromotionName = "Fuel Discount Promo",
                DiscountPercent = 20,
                StartDate = DateTime.Parse("01/01/2020"),
                EndDate = DateTime.Parse("15/02/2024")
            },
            new DiscountPromotion()
            {
                DiscountPromotionId = "DP002",
                PromotionName = "Happy Promo",
                DiscountPercent = 10,
                StartDate = DateTime.Parse("02/03/2020"),
                EndDate = DateTime.Parse("20/03/2020")
            }
            );
        }
    }
}