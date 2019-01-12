using Newtonsoft.Json;

namespace SetareSazBot.API.Json.Output
{
    public class PaymentRequestOutput
    {
        [JsonProperty("data")] public PaymentRequestOutputData Data { get; set; }
    }

    public class PaymentRequestOutputData
    {
        [JsonProperty("payment_token")] public string PaymentToken { get; set; }
    }
}