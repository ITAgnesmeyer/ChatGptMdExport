namespace ChatGptMdExport
{
    internal class Options:OpitonsBase
    {
        public Options(string[] args):base(args)
        {
            
        }
        public bool HasHelp => base.HasArg("/?");
        public bool HasSourceFolder => base.HasArg("/i");
        public string? SourceFolder => base.GetArg("/i");

        public bool HasDestFolder => base.HasArg("/o");
        public string? DestFolder => base.GetArg("/o");

        public bool HasCreateDestiantion => base.HasArg("/cd");
        public static void PrintInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("paramters:");
            Console.WriteLine("/?\t\tHelp");
            Console.WriteLine("/i:<folder>\tfolder containing ChatGPT export");
            Console.WriteLine("/o:<folder>\tfolder to export to");
            Console.WriteLine("/cd\t\tcreate destination folder");
            Console.WriteLine("");
            Console.WriteLine("example:");
            Console.WriteLine("ChatGptMdExport /i:\"c:\\temp\\gpt_export\" /o:\"c:\\temp\\md_folder\"");
            Console.ResetColor();
        }
    }
}
