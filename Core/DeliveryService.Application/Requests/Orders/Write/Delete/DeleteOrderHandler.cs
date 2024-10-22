using DeliveryService.Application.Enums;
using DeliveryService.Application.Interfaces;
using DeliveryService.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Application.Requests.Orders.Write.Delete
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderRequest, DeleteOrderResponse>
    {
        private readonly IApplicationDbContext _context;

        public DeleteOrderHandler(IApplicationDbContext context) => _context = context;

        public async Task<DeleteOrderResponse> Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
        {
            Order order = await _context.Order.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            StatusOrder status = (StatusOrder)order.Status;

            DeleteOrderResponse response = new DeleteOrderResponse
            {
                Id = order.Id,
                Status = status.ToString()
            };

            _context.Order.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
