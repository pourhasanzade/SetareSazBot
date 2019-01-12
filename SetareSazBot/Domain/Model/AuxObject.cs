using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SetareSazBot.Domain.Enum;

namespace SetareSazBot.Domain.Model
{
    public class AuxObject
    {
        [JsonProperty("button_id")] public string ButtonId { get; set; }
        [JsonProperty("order_id")] public string OrderId { get; set; }
        [JsonProperty("order_status")] [JsonConverter(typeof(StringEnumConverter))] public OrderStatusEnum OrderStatus { get; set; }

    }
}