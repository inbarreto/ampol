using Ampol.Persistence.Infrastructure;

namespace Ampol.Application.Test
{
    public class DbContextTestBase : IDisposable
    {
        protected readonly AmpolDbContext _context;

        public DbContextTestBase() : base()
        {
            DbContextFactory contextFactory = new DbContextFactory();
            _context = contextFactory.Create();
        }

        public void Dispose()
        {
            DbContextFactory.Destroy(_context);
        }
    }
}