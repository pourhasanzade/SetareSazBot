using Newtonsoft.Json;

namespace SetareSazBot.API.Json.Input
{
    public class GetSelectionInput
    {
        [JsonProperty("selection_id")] public string SelectionId { get; set; }
        [JsonProperty("chat_id")] public string ChatId { get; set; }
        [JsonProperty("first_index")] public int FirstIndex { get; set; }
        [JsonProperty("last_index")] public int LastIndex { get; set; }

    }
}