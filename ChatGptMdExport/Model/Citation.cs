namespace ChatGptMdExport.Model
{
    public class Citation
    {
        public int? start_ix { get; set; }
        public int? end_ix { get; set; }
        public string? citation_format_type { get; set; }
        public CitationMetaData? metadata { get; set; }


    }
}
