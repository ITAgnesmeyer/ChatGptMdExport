using System.Text.Json.Serialization;

namespace ChatGptMdExport.Model
{
    public class Metadata
    {
        public List<Citation>? citations { get; set; }
        public object? gizmo_id { get; set; }
        public object? message_type { get; set; }
        public string? model_slug { get; set; }
        public string? default_model_slug { get; set; }
        public string? pad { get; set; }
        public string? parent_id { get; set; }
        public object? finish_details { get; set; }
        public bool? is_complete { get; set; }
        public string? request_id { get; set; }

        [JsonPropertyName("timestamp_")]
        public string? timestamp { get; set; }

    }
}
