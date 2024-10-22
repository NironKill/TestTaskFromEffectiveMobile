using DeliveryService.Application.Enums;
using DeliveryService.Application.Interfaces;
using DeliveryService.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Application.Requests.Orders.Read.GetById
{
    public class GetByIdOrderHandler : IRequestHandler<GetByIdOrderRequest, GetByIdOrderResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetByIdOrderHandler(IApplicationDbContext context) => _context = context;

        public async Task<GetByIdOrderResponse> Handle(GetByIdOrderRequest request, CancellationToken cancellationToken)
        {
            Order order = await _context.Order.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            DateTime orderDate = DateTime.UnixEpoch.AddSeconds(order.DeliveryTimestamp).ToLocalTime();
            District district = (District)order.DistrictId;
            StatusOrder status = (StatusOrder)order.Status;

            GetByIdOrderResponse response = new GetByIdOrderResponse
            {
                Id = order.Id,
                Weight = order.Weight,
                DistrictName = district.ToString(),
                DeliveryDateTime = orderDate,
                Status = status.ToString()
            };

            return response;
        }
    }
}
