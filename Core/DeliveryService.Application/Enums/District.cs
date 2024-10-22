using System.Text.Json.Serialization;

namespace DeliveryService.Application.Enums
{
    /// <summary>
    /// districts of Minsk
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum District
    {
        October = 123847,
        Lenin = 139564,
        Moscow = 195636,
        Partisans = 183541,
        Pieršamajski = 176902,
        Soviet = 117684,
        Factory = 134563
    }
}
