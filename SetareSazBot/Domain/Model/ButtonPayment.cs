using Newtonsoft.Json;

namespace SetareSazBot.Domain.Model
{
    public class ButtonPayment
    {
        [JsonProperty("button_payment_token")] public string ButtonPaymentToken { get; set; }

    }
}