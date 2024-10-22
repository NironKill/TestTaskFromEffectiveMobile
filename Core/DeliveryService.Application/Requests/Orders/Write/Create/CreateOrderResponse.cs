namespace DeliveryService.Application.Requests.Orders.Write.Create
{
    public class CreateOrderResponse
    {
        public Guid Id { get; set; }
        public int Weight { get; set; }
        public string DistrictName { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public string Status { get; set; }
    }
}
