using System.Collections.Generic;
using Newtonsoft.Json;
using SetareSazBot.Domain.Model;

namespace SetareSazBot.API.Json.Output
{
    public class GetMessageOutput
    {
        [JsonProperty("data")] public GetMessageOutputData Data { get; set; }

    }

    public class GetMessageOutputData
    {
        [JsonProperty("messages")] public List<MessageModel> MessageList { get; set; }

    }
}