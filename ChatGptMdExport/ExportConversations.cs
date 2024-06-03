using ChatGptMdExport.Model;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ChatGptMdExport
{
    internal class ExportConversations : ExportActionBase
    {
        public ExportConversations(Options options):base(options)
        {
            
        }
        protected override string TargetFile => "conversations.json";
        static string FilterInvalidCharacters(string input)
        {
            // erstelle eine Regex die alle in Dateinamen ungültigen Zeichen erkennt und entferne sie
            // vorgausgesetzt wird , dass die Datei auf einem Windows-System gespeichert wird
            // es sollen auch zeichen win \t \n \r entfernt werden

            string resultText = Regex.Replace(input, @"[^\w\s\-_\.~!#$%&'(){}[\]+\,\;=@]+", "");
            resultText = resultText.Replace("\t", "");
            resultText = resultText.Replace("\n", "");
            resultText = resultText.Replace("\r", "");
            return resultText;

        }
        protected override void Execute()
        {
            using (FileStream stream = File.OpenRead(this.SourceFile!))
            {
                var conversations = JsonSerializer.Deserialize<Conversation[]>(stream, GptJasonSerializerContext.Default.ConversationArray);
                if (conversations is null)
                {
                    throw new InvalidOperationException($"{nameof(ExportConversations)} Error: Conversations not loaded!");
                }

                foreach (var conversation in conversations)
                {
                    string title = conversation.Title ?? DateTime.Now.ToString("yyyyMMddHHmmss");
                    string fileName = Path.Combine(this.DestinationFolder!, $"{FilterInvalidCharacters(title)}.md");

                    var content = BuildContent(conversation);
                    File.WriteAllText(fileName, content);
                    Log($"Write=>{fileName}");
                }
            }
        }
        private string BuildContent(Conversation conversation)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"# {conversation.Title}");
          
            string lastRole = string.Empty;
            if (conversation.Mapping != null)
            {
                foreach (var mapping in conversation.Mapping.Values)
                {
                    if (mapping?.Message != null)
                    {
    
                        string currentRole = mapping.Message.Author?.Role!;
                        if (lastRole == "assistant" && currentRole == "assistant")
                        {
                            for (int i = sb.Length - 1; i >= 0; i--)
                            {
                                if (sb[i] == '\r' || sb[i] == '\n')
                                {
                                    sb.Length = i;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            sb.AppendLine(FormatMessageContent(mapping.Message.Content, mapping.Message.Metadata));
                        }
                        else 
                        {
                            sb.AppendLine($"## {mapping.Message.Author?.Role}");
                            sb.AppendLine(FormatMessageContent(mapping.Message.Content, mapping.Message.Metadata));
                            
                        }
                        
                        lastRole = mapping.Message.Author?.Role!;
                    }
                }
            }
            return sb.ToString();
        }

        private string FormatMessageContent(Content? content, Metadata? metadata)
        {
            if (content?.Parts == null) return string.Empty;
            if(metadata == null)
            {
                return string.Join(Environment.NewLine, content.Parts.Select(p => p)); 
            }

            if(metadata.citations == null)
            {
                return string.Join(Environment.NewLine, content.Parts.Select(p => p));
            }

            if(metadata.citations.Count() == 0)
            {
                return string.Join(Environment.NewLine, content.Parts.Select(p => p));
            }

            string result = string.Join(Environment.NewLine, content.Parts.Select(p => p));
          
            string pattern = @"【(\d+)†source】";
            Regex regex = new Regex(pattern);
            

            var regResult =  regex.Matches(result);
            if(regResult.Count > 0)
            {
                foreach (Match item in regResult)
                {
                    foreach (Citation citation in metadata.citations)
                    {
                        if(citation.metadata != null)
                        {
                            if(citation.metadata.extra != null)
                            {
                                if(citation.metadata.extra.cited_message_idx != null)
                                {
                                    if (item.Groups[1].Value == citation.metadata.extra.cited_message_idx.ToString())
                                    {
                                        string link = $" ([{citation.metadata.title}]({citation.metadata.url})) ";
                                        result = result.Replace(item.Value, link);
                                    }
                                }
                            }
                        }
                    }
                }
            }



           


            return result;
        }
      
    }
}
