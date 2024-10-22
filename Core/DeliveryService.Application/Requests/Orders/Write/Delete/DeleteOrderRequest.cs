using MediatR;

namespace DeliveryService.Application.Requests.Orders.Write.Delete
{
    public class DeleteOrderRequest : IRequest<DeleteOrderResponse>
    {
        public Guid Id { get; set; }
    }
}
