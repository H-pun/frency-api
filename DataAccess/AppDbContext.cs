using Microsoft.EntityFrameworkCore;
using Frency.Base;
using Frency.DataAccess.Entities;
using Frency.DataAccess.Extensions;

namespace Frency.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var entitiesAssembly = typeof(User).Assembly;
            modelBuilder.RegisterAllEntities<BaseEntity>(entitiesAssembly);
        }
    }
}
