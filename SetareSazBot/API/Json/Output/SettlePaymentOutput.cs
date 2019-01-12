using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SetareSazBot.Domain.Enum;

namespace SetareSazBot.API.Json.Output
{
    public class SettlePaymentOutput
    {
        [JsonProperty("data")] public SettlePaymentOutputData Data { get; set; }

    }

    public class SettlePaymentOutputData
    {
        [JsonProperty("status")] [JsonConverter(typeof(StringEnumConverter))] public SettleStatusEnum SettleStatus { get; set; }

    }
}