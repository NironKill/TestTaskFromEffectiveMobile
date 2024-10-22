using DeliveryService.Application.Interfaces;
using DeliveryService.Domain;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Order> Order { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
    }
}
