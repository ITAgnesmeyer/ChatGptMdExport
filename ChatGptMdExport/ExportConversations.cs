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
            // Regulärer Ausdruck, der alle Zeichen außer den erlaubten entfernt
            return Regex.Replace(input, @"[^\w\s\-_\.~!#$%&'(){}[\]+\,\;=@]+", "");
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

                            sb.AppendLine(FormatMessageContent(mapping.Message.Content));
                        }
                        else 
                        {
                            sb.AppendLine($"## {mapping.Message.Author?.Role}");
                            sb.AppendLine(FormatMessageContent(mapping.Message.Content));
                            
                        }
                        
                        lastRole = mapping.Message.Author?.Role!;
                    }
                }
            }
            return sb.ToString();
        }

        private string FormatMessageContent(Content? content)
        {
            if (content?.Parts == null) return string.Empty;

            return string.Join(Environment.NewLine, content.Parts.Select(p => p));
        }
      
    }
}
