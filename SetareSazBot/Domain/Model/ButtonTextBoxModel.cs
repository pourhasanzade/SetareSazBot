using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SetareSazBot.Domain.Enum;

namespace SetareSazBot.Domain.Model
{
    public class ButtonTextBoxModel
    {
        [JsonProperty("type_line")] [JsonConverter(typeof(StringEnumConverter))] public ButttenTextBoxLineEnum LineType { get; set; }
        [JsonProperty("type_keypad")] [JsonConverter(typeof(StringEnumConverter))] public ButttenTextBoxKeypadEnum KeypadType { get; set; }

    }
}