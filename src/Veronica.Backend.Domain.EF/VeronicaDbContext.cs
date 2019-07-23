using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Veronica.Backend.Domain.EF.Mapping;

namespace Veronica.Backend.Domain.EF
{
    public class VeronicaDbContext : DbContext, IUnitOfWork, IQueryContext
    {
        public VeronicaDbContext(DbContextOptions<VeronicaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new UserMap());
            modelBuilder.AddConfiguration(new DishMap());
        }

        public Task CommitAsync()
        {
            return SaveChangesAsync();
        }
    }

    public static class DbContextExtensions
    {
        public static IQueryContext AsQueryContext(this VeronicaDbContext context)
        {
            context.Database.AutoTransactionsEnabled = false;
            context.ChangeTracker.AutoDetectChangesEnabled = false;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return context;
        }
    }
}
