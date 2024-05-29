using ChatGptMdExport;
using Markdig;

namespace MdToHtml
{
    internal class Options : OpitonsBase
    {

        public Options(string[] args) : base(args)
        {

        }

        public bool SourceFolderExist => HasArg("/i");
        public string? SourceFolder => this.SourceFolderExist? GetArg("/i"): string.Empty;
        public bool DestFolderExist => HasArg("/o");
        public string? DestFolder => this.DestFolderExist? GetArg("/o"): string.Empty;
        public bool HasCreateDestiantion => HasArg("/c");
        public bool HasHelp => HasArg("/h");

        // erstelle eine Methode die die Hilfe ausgibt
        public static void PrintInfo()
        {
            Console.WriteLine("MdToHtml");
            Console.WriteLine("Options:");
            Console.WriteLine($"{"/i:source",-20} folder");
            Console.WriteLine($"{"/o:destination",-20} folder");
            Console.WriteLine($"{"/c",-20} create destination folder if not exist");
            Console.WriteLine($"{"/h",-20} help");
        }


    }

    internal class Program
    {
        static void Main(string[] args)
        {

            Options opt = new Options(args);
            if (opt.HasHelp)
            {
                Options.PrintInfo();
                return;
            }
            if(!opt.SourceFolderExist)
            {

               Console.WriteLine("Source folder is missing");
                Options.PrintInfo();
                return;
            }
            if(!opt.DestFolderExist)
            {
                Console.WriteLine("Destination folder is missing");
                Options.PrintInfo();
                return;
            }

            bool createDest = opt.HasCreateDestiantion;
            string sourceFolder = opt.SourceFolder!;
            string destFolder = opt.DestFolder!;
            if(!Directory.Exists(sourceFolder))
            {
                Console.WriteLine("Source folder does not exist");
                return;
            }

            if(!Directory.Exists(destFolder))
            {
                if(createDest)
                {
                    Directory.CreateDirectory(destFolder);
                }
                else
                {
                    Console.WriteLine("Destination folder does not exist");
                    return;
                }
            }
            var pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UsePipeTables()
                .UseAutoLinks()
                .UseTaskLists()
                .UseCustomContainers()
                .UseEmphasisExtras()
                .Build();
            // kopiere alle dateien die in dem Ordner tools\CSS liegen in das verzeichnis css im Zielverzeichnis
            // überpüfe ob der css ordner existiert            
            // kopiere die Dateien in das Zielverzeichnis wenn sie noch icht existiert
         
            string cssFolder = Path.Combine(destFolder, "css");
            if(!Directory.Exists(cssFolder))
            {
                Directory.CreateDirectory(cssFolder);
            }
            string cssToolsFolder = Path.Combine(GetTooPath(), "CSS");

            string[] cssFiles = Directory.GetFiles(cssToolsFolder);
            foreach (var file in cssFiles)
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(cssFolder, fileName);
                if(!File.Exists(destFile))
                {
                    File.Copy(file, destFile);
                    Console.WriteLine($"Copy=>{destFile}");
                }
            }


            // kopiere alle Dateien mit der Endung .md in das Zielverzeichnis
            // die Dateien sollen in das Zielverzeichnis kopiert werden und die Endung .md durch .html ersetzt werden
                        
            string[] files = Directory.GetFiles(sourceFolder, "*.md");
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destFolder, fileName.Replace(".md", ".html"));
                string content = File.ReadAllText(file);
                string htmlContent = Markdown.ToHtml(content, pipeline);
                string fullHtmlContent = $@"
<!DOCTYPE html>
<!--saved from url=(0011)about:blank-->
<html lang=""en-us"">

<head>
   <meta charset=""UTF-8"">
   <meta content=""text/html"">
    <!--<meta http-equiv=""Content-Type"" content=""text/html"">-->
<!--     <meta http-equiv=""X-UA-Compatible"" content=""IE=Edge""> -->
    <!-- <base href="".""> -->
    <base href=""."">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <!-- <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/github-markdown-css/5.5.0/github-markdown.css"" integrity=""sha512-LX/J+iRwkfRqaipVsfmi2B1S7xrqXNHdTb6o4tWe2Ex+//EN3ifknyLIbX5f+kC31zEKHon5l9HDEwTQR1H8cg=="" crossorigin=""anonymous"" referrerpolicy=""no-referrer"" /> -->
    <link rel=""stylesheet"" href=""./css/github-markdown.css"">
    <style type=""text/css"">
        .markdown-body {{
                box-sizing: border-box;
                min-width: 200px;
                max-width: 980px;
                margin: 0 auto;
                padding: 45px;
        }}

        @media (max-width: 767px) {{
                .markdown-body {{
                        padding: 15px;
                }}
        }}
    </style>
    <link rel=""stylesheet"" href=""./css/prism.css"">
    <title>Markdown Preview</title>
    <meta http-equiv=""cache-control"" content=""no-cache"">

</head>

<body>
    <article class=""markdown-body"">
        {htmlContent}
</article>
    <script src=""./css/prism.js""></script>
    <script src=""./css/mermaid.min.js""></script>
    <script>
        mermaid.initialize({{ 'securityLevel': 'loose', 'theme': 'forest', startOnLoad: true, flowchart: {{ htmlLabels: false }} }});
    </script>


</body>

</html>";

                File.WriteAllText(destFile, fullHtmlContent);
                Console.WriteLine($"Write=>{destFile}");
            }


        
        }

        static string GetTooPath()
        {


            var dir = AppContext.BaseDirectory;
            var toolsDir = Path.Combine(dir, "tools");
            return toolsDir;

        }
    }

   
}
