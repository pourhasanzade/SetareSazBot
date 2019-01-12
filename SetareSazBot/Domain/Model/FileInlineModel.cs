using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SetareSazBot.Domain.Enum;

namespace SetareSazBot.Domain.Model
{
    public class FileInlineModel
    {
        [JsonProperty("file_id")] public string FileId { get; set; }
        [JsonProperty("mime")] public string Mime { get; set; }
        [JsonProperty("dc_id")] public string DcId { get; set; }
        [JsonProperty("access_hash_rec")] public string AccessHashRec { get; set; }
        [JsonProperty("file_name")] public string FileName { get; set; }
        [JsonProperty("thumb_inline")] public string ThumbInline { get; set; }
        [JsonProperty("width")] public string Width { get; set; }
        [JsonProperty("height")] public string Height { get; set; }
        [JsonProperty("time")] public string Time { get; set; }
        [JsonProperty("size")] public string Size { get; set; }
        [JsonProperty("type")] [JsonConverter(typeof(StringEnumConverter))] public FileInlineTypeEnum Type { get; set; }
        [JsonProperty("file_url")] public string FileUrl { get; set; }

    }
}