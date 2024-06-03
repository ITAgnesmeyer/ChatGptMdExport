namespace ChatGptMdExport.Model
{
    public class CitationExtra 
    { 
        public int? cited_message_idx { get; set; }
        public int? search_result_idx { get; set; }
        public string? evidence_text { get; set; }
    }
}
