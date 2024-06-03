using System.Text.Json.Serialization;

namespace ChatGptMdExport.Model
{
    public class Content
    {
        [JsonPropertyName("content_type")]
        public string? ContentType { get; set; }
        [JsonPropertyName("parts")]
        public List<object>? Parts { get; set; }
    }
}
