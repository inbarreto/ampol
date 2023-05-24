using Ampol.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Ampol.Application.Test
{
    public class DbContextFactory
    {
        private DbContextOptions<AmpolDbContext> options = new DbContextOptionsBuilder<AmpolDbContext>()
            .UseInMemoryDatabase("Ampol")
            .Options;

        public AmpolDbContext Create()
        {
            AmpolDbContext context = new AmpolDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        public static void Destroy(AmpolDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}