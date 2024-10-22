using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DeliveryService.Application.Requests.Orders.Write.Create
{
    public class CreateOrderDTO
    {
        public int Weight { get; set; }
        public DateTime DeliveryDateTime { get; set; }
    }
}
