using System.Collections.Generic;
using Newtonsoft.Json;

namespace SetareSazBot.Domain.Model
{
    public class KeypadModel
    {
        [JsonProperty("rows")]  public List<KeypadRowModel> Rows { get; set; }
    }
}