namespace ChatGptMdExport.Model
{
    public class CitationMetaData
    {
        public string? type { get; set; }
        public string? title { get; set; }
        public string? url { get; set; }
        public string? text { get; set; }
        public object? pub_date { get; set; }
        public CitationExtra? extra { get; set; }  
    }
}
