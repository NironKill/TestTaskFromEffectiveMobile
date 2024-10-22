using DeliveryService.Domain;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Order> Order { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
