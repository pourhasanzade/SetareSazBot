using Newtonsoft.Json;
using SetareSazBot.Domain.Enum;
using SetareSazBot.Domain.Model;

namespace SetareSazBot.API.Json.Input
{
    public class GetMessagesInput
    {
        [JsonProperty("message")] public MessageModel Mesage { get; set; }
        [JsonProperty("type")] public MessageBehaviourTypeEnum Type { get; set; }

    }
}