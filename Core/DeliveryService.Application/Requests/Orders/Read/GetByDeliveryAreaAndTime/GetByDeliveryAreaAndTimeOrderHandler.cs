using DeliveryService.Application.Enums;
using DeliveryService.Application.Interfaces;
using DeliveryService.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Application.Requests.Orders.Read.GetByDeliveryAreaAndTime
{
    public class GetByDeliveryAreaAndTimeOrderHandler : IRequestHandler<GetByDeliveryAreaAndTimeOrderRequest, List<GetByDeliveryAreaAndTimeOrderResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetByDeliveryAreaAndTimeOrderHandler(IApplicationDbContext context) => _context = context;

        public async Task<List<GetByDeliveryAreaAndTimeOrderResponse>> Handle(GetByDeliveryAreaAndTimeOrderRequest request, CancellationToken cancellationToken)
        {
            District district = (District)Enum.Parse(typeof(District), request.DistrictName, true);
            
            int firstDeliveryTimestamp = Convert.ToInt32(((DateTimeOffset)request.FirstDeliveryDateTime).ToUnixTimeSeconds());
            int halfHourLaterTimestamp = Convert.ToInt32(((DateTimeOffset)request.FirstDeliveryDateTime).AddSeconds(1800).ToUnixTimeSeconds());

            List<Order> listOrder = await _context.Order
                .Where(x => x.DistrictId == (int)district && x.Status == (int)StatusOrder.Processing)
                .Where(x => x.DeliveryTimestamp >= firstDeliveryTimestamp && x.DeliveryTimestamp <= halfHourLaterTimestamp)
                .ToListAsync(cancellationToken);

            List<GetByDeliveryAreaAndTimeOrderResponse> responses = new List<GetByDeliveryAreaAndTimeOrderResponse>();
            foreach (Order order in listOrder)
            {
                DateTime orderDate = DateTime.UnixEpoch.AddSeconds(order.DeliveryTimestamp).ToLocalTime();
                StatusOrder status = (StatusOrder)order.Status;

                GetByDeliveryAreaAndTimeOrderResponse response = new GetByDeliveryAreaAndTimeOrderResponse
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
