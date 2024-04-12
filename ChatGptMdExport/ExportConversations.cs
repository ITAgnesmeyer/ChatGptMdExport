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
            byte[] json = File.ReadAllBytes(this.SourceFile!);
            string jsStr = Encoding.UTF8.GetString(json);
            var conversations = JsonSerializer.Deserialize<Conversation[]>(jsStr, GptJasonSerializerContext.Default.ConversationArray);

            if (conversations == null) 
            {
                throw new Exception($"{nameof(ExportConversations)} Error:Conversations not loaded!");
            }

            foreach (var conversation in conversations) 
            {
                string title = DateTime.Now.ToString();
                if (!string.IsNullOrEmpty(conversation.Title)) 
                { 
                    title = conversation.Title;
                }
                string fileName = FilterInvalidCharacters(title);
                fileName += ".md";
                fileName = Path.Combine(this.DestinationFolder!, fileName);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"# {conversation.Title}");
                if (conversation.Mapping != null) 
                {
                    foreach (var mapping in conversation.Mapping.Values) 
                    { 
                        if(mapping?.Message != null)
                        {
                            if(mapping.Message.Author != null)
                            {
                                sb.AppendLine($"## {mapping.Message.Author.Role}");
                            }
                            if (mapping.Message.Content != null) 
                            { 
                                if(mapping.Message.Content.Parts != null)
                                {
                                    foreach(var item in mapping.Message.Content.Parts)
                                    {
                                        sb.AppendLine(item);
                                    }
                                }
                            }
                        }
                    }
                }
                sb.AppendLine("");
                File.WriteAllText(fileName, sb.ToString());
                Log($"Write=>{fileName}");
            }
        }
    }
}
