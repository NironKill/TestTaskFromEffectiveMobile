namespace DeliveryService.Application.Requests.Orders.Read.GetById
{
    public class GetByIdOrderResponse
    {
        public Guid Id { get; set; }
        public int Weight { get; set; }
        public string DistrictName { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public string Status { get; set; }
    }
}
