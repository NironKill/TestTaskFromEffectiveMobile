using DeliveryService.Application.Enums;
using DeliveryService.Application.Interfaces;
using DeliveryService.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Application.Requests.Orders.Read.GetAll
{
    public class GetAllOrderHandler : IRequestHandler<GetAllOrderRequest, List<GetAllOrderResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllOrderHandler(IApplicationDbContext context) => _context = context;

        public async Task<List<GetAllOrderResponse>> Handle(GetAllOrderRequest request, CancellationToken cancellationToken)
        {
            List<Order> listOrder = await _context.Order.ToListAsync(cancellationToken);

            List<GetAllOrderResponse> responses = new List<GetAllOrderResponse>();
            foreach (Order order in listOrder)
            {
                DateTime orderDate = DateTime.UnixEpoch.AddSeconds(order.DeliveryTimestamp).ToLocalTime();
                District district = (District)order.DistrictId;
                StatusOrder status = (StatusOrder)order.Status;

                GetAllOrderResponse response = new GetAllOrderResponse
                {
                    Id = order.Id,
                    Weight = order.Weight,
                    DistrictName = district.ToString(),
                    DeliveryDateTime = orderDate,
                    Status = status.ToString()
                };
                responses.Add(response);
            }
            return responses;
        }
    }
}
