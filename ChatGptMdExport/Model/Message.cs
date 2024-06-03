using System.Text.Json.Serialization;

namespace ChatGptMdExport.Model
{

    public class Message
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("author")]
        public Author? Author { get; set; }
        [JsonPropertyName("create_time")]
        public double? CreateTime { get; set; }
        [JsonPropertyName("update_time")]
        public double? UpdateTime { get; set; }
        [JsonPropertyName("content")]
        public Content? Content { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("end_turn")]
        public bool? EndTurn { get; set; }
        [JsonPropertyName("weight")]
        public double? Weight { get; set; }
        [JsonPropertyName("metadata")]
        public Metadata? Metadata { get; set; }
        [JsonPropertyName("recipient")]
        public string? Recipient { get; set; }
    }
}
