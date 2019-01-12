using Newtonsoft.Json;

namespace SetareSazBot.API.Json.Output
{
    public class UploadOutput
    {
        [JsonProperty("file_path")]
        public string FilePath { get; set; }

        [JsonProperty("error")]
        public bool Error { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}