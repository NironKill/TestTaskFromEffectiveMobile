using DeliveryService.Application.Enums;
using DeliveryService.Application.Interfaces;
using DeliveryService.Domain;
using MediatR;

namespace DeliveryService.Application.Requests.Orders.Write.Create
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
    {
        private readonly IApplicationDbContext _context;

        public CreateOrderHandler(IApplicationDbContext context) => _context = context;

        public async Task<CreateOrderResponse> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            int orderCreationTS = Convert.ToInt32(((DateTimeOffset)request.DeliveryDateTime).ToUnixTimeSeconds());

            District district = (District)Enum.Parse(typeof(District), request.DistrictName, true);

            Order newOrder = new Order
            {
                Id = Guid.NewGuid(),
                Weight = request.Weight,
                DistrictId = (int)district,
                DeliveryTimestamp = orderCreationTS,
                Status = (int)StatusOrder.Processing
            };
            await _context.Order.AddAsync(newOrder, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            CreateOrderResponse response = new CreateOrderResponse
            {
                Id = newOrder.Id,
                Weight = newOrder.Weight,
                DistrictName = request.DistrictName,
                DeliveryDateTime = request.DeliveryDateTime,
                Status = StatusOrder.Processing.ToString()
            };

            return response;
        }
    }
}
