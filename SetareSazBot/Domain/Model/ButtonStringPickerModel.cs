using System.Collections.Generic;
using Newtonsoft.Json;

namespace SetareSazBot.Domain.Model
{
    public class ButtonStringPickerModel
    {
        [JsonProperty("items")] public List<string> Items { get; set; }
        [JsonProperty("default_value")] public string DefaultValue { get; set; }
    }
}