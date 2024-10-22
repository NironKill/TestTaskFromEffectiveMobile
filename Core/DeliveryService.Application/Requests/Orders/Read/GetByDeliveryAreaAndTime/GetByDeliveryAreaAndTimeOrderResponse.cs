namespace DeliveryService.Application.Requests.Orders.Read.GetByDeliveryAreaAndTime
{
    public class GetByDeliveryAreaAndTimeOrderResponse
    {
        public Guid Id { get; set; }
        public int Weight { get; set; }
        public string DistrictName { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public string Status { get; set; }
    }
}
