using Newtonsoft.Json;

namespace SetareSazBot.Domain.Model
{
    public class ButtonCallModel
    {
        [JsonProperty("phone_number")] public string PhoneNumber { get; set; }

    }
}