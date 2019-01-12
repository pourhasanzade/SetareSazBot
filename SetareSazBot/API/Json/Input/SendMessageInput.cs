using System;
using Newtonsoft.Json;
using SetareSazBot.Domain.Model;

namespace SetareSazBot.API.Json.Input
{
    public class SendMessageInput
    {
        public SendMessageInput()
        {
            var random = new Random();
            Random = random.Next(100000000, 1000000000);
        }
        [JsonProperty("chat_id")] public string ChatId { get; set; }
        [JsonProperty("text")] public string Text { get; set; }
        [JsonProperty("file_inline")] public FileInlineModel FileInline { get; set; }
        [JsonProperty("reply_to_message_id")] public string ReplyToMessageId { get; set; }
        [JsonProperty("rnd")] public int Random { get; set; }
        [JsonProperty("location")] public LocationModel Location { get; set; }
        [JsonProperty("bot_keypad")] public KeypadModel Keypad { get; set; }
        [JsonProperty("reply_timeout")] public string ReplyTimeout { get; set; }
    }
}