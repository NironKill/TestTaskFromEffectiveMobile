using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Domain
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        public int Weight { get; set; }

        public int DistrictId { get; set; }

        public int DeliveryTimestamp { get; set; }

        /// <summary>
        /// 0 - order in process.
        /// 1 - order completed.
        /// </summary>
        public int Status { get; set; }
    }
}
