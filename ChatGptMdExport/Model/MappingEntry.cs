using System.Text.Json.Serialization;

namespace ChatGptMdExport.Model
{
    public class MappingEntry
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("message")]
        public Message? Message { get; set; }
        [JsonPropertyName("parent")]
        public string? Parent { get; set; }
        [JsonPropertyName("children")]
        public List<string>? Children { get; set; }
    }
}
