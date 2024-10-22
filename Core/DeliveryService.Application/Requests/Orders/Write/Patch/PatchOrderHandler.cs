using DeliveryService.Application.Enums;
using DeliveryService.Application.Interfaces;
using DeliveryService.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Application.Requests.Orders.Write.Patch
{
    public class PatchOrderHandler : IRequestHandler<PatchOrderRequest, PatchOrderResponse>
    {
        private readonly IApplicationDbContext _context;

        public PatchOrderHandler(IApplicationDbContext context) => _context = context;

        public async Task<PatchOrderResponse> Handle(PatchOrderRequest request, CancellationToken cancellationToken)
        {
            Order updateOrder = await _context.Order.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            updateOrder.Status = (int)StatusOrder.Completed;

            _context.Order.Update(updateOrder);
            await _context.SaveChangesAsync(cancellationToken);

            PatchOrderResponse response = new PatchOrderResponse
            {
                Id = updateOrder.Id,
                Status = StatusOrder.Completed.ToString()
            };

            return response;
        }
    }
}
