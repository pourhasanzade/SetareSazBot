using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SetareSazBot.Domain.Enum;

namespace SetareSazBot.Domain.Model
{
    public class MessageModel
    {
        [JsonProperty("message_id")] public string MessageId { get; set; }
        [JsonProperty("chat_id")]  public string ChatId { get; set; }
        [JsonProperty("text")]  public string Text { get; set; }
        [JsonProperty("file_inline")]  public FileInlineModel FileInline { get; set; }
        [JsonProperty("reply_to_message_id")]  public string ReplyToMessageId { get; set; }
        [JsonProperty("time")]  public int Time { get; set; }
        [JsonProperty("type")] [JsonConverter(typeof(StringEnumConverter))] public MessageTypeEnum Type { get; set; }
        [JsonProperty("count_seen")]  public string CountSeen { get; set; }
        [JsonProperty("is_edited")]  public string IsEdited { get; set; }
        [JsonProperty("forwarded_from")]  public ForwardModel ForwardedFrom { get; set; }
        [JsonProperty("aux_data")]  public AuxObject AuxData { get; set; }
        [JsonProperty("bot_keypad")]  public KeypadModel Keypad { get; set; }
        [JsonProperty("reply_timeout ")]  public string ReplyTimeout { get; set; }
        [JsonProperty("location")]  public LocationModel Location { get; set; }
        
    }
}