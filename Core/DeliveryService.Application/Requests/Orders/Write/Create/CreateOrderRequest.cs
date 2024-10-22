using MediatR;

namespace DeliveryService.Application.Requests.Orders.Write.Create
{
    public class CreateOrderRequest : IRequest<CreateOrderResponse>
    {
        public int Weight { get; set; }
        public string DistrictName { get; set; }
        public DateTime DeliveryDateTime { get; set; }
    }
}
