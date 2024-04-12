using System.Text.Json.Serialization;

namespace ChatGptMdExport.Model
{
    public class Conversation
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("create_time")]
        public double? CreateTime { get; set; }
        [JsonPropertyName("update_time")]
        public double? UpdateTime { get; set; }
        [JsonPropertyName("mapping")]
        public Dictionary<string, MappingEntry>? Mapping { get; set; }
        //public List<Message>? Messages { get; set; }
    }

    [JsonSerializable(typeof(Conversation[]))]
    internal partial class GptJasonSerializerContext : JsonSerializerContext
    {

    }
}
