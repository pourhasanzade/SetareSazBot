using Newtonsoft.Json;

namespace SetareSazBot.Domain.Model
{
    public class ButtonAlertModel
    {
        [JsonProperty("message")] public string Message { get; set; }

    }
}