namespace ChatGptMdExport
{
    internal class Program
    {
        private static void Run(string[] args)
        {
            try
            {
                Options opt = new Options(args);
                if (opt.HasHelp)
                {
                    Options.PrintInfo();
                    return;
                }

                ExportActionBase exporConf = new ExportConversations(opt);
                exporConf.Run();

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("ChatGptMdExport Error:");
                Console.Error.WriteLine(ex.Message);
                Console.ResetColor();
                Options.PrintInfo();
            }

        }
        static void Main(string[] args)
        {
            Run(args);
        }
    }
}
