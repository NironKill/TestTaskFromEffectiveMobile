namespace DeliveryService.Application.Requests.Orders.Read.GetAll
{
    public class GetAllOrderResponse
    {
        public Guid Id { get; set; }
        public int Weight { get; set; }
        public string DistrictName { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public string Status { get; set; }
    }
}
