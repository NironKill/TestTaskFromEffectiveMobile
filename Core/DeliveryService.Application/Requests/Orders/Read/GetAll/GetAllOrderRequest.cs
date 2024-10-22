using MediatR;

namespace DeliveryService.Application.Requests.Orders.Read.GetAll
{
    public class GetAllOrderRequest : IRequest<List<GetAllOrderResponse>>
    {
    }
}
