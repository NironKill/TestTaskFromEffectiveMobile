using MediatR;

namespace DeliveryService.Application.Requests.Orders.Read.GetByDeliveryAreaAndTime
{
    public class GetByDeliveryAreaAndTimeOrderRequest : IRequest<List<GetByDeliveryAreaAndTimeOrderResponse>>
    {
        public string DistrictName { get; set; }
        public DateTime FirstDeliveryDateTime { get; set; } 
    }
}
