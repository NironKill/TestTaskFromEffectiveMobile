using System.Text.Json.Serialization;

namespace DeliveryService.Application.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusOrder
    {
        /// <summary>
        /// The order is being processed
        /// </summary>
        Processing = 0,

        /// <summary>
        /// Order successfully processed
        /// </summary>
        Completed = 1,
    }
}
