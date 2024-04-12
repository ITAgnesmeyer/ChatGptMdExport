namespace ChatGptMdExport
{
    internal abstract class ExportActionBase
    {
        protected abstract string TargetFile { get; }
        private Options _Options;
        private string? _SourceFile;
        private string? _DestinationFolder;
        protected Options Options=> this._Options;
        protected string? SourceFile=> this._SourceFile;
        protected string? DestinationFolder => this._DestinationFolder;
        public ExportActionBase(Options options)
        {
            this._Options = options;
        }
        private void BuidlSourceFile()
        {
            if (string.IsNullOrEmpty(this._Options.SourceFolder))
            {
                throw new Exception($"{nameof(ExportActionBase)} {nameof(Options)} error:source folder is empty!");
            }

            this._SourceFile = Path.Combine(this._Options.SourceFolder, TargetFile);

            if (!File.Exists(this._SourceFile))
            {
                throw new Exception($"{nameof(ExportActionBase)} {nameof(Options)} error:cannot find {this._SourceFile}!");
            }



        }
        private void CreateDestination(string destFolder)
        {
            Directory.CreateDirectory(destFolder);
        }
        private void BildDestinationFolder()
        {
            if (string.IsNullOrEmpty(this._Options.DestFolder))
            {
                throw new Exception($"{nameof(ExportActionBase)} {nameof(Options)} error:destination folder is empty!");
            }
            this._DestinationFolder = this._Options.DestFolder;
            if (!Directory.Exists(this._DestinationFolder))
            {
                if (!this._Options.HasCreateDestiantion)
                {
                    throw new Exception($"{nameof(ExportActionBase)} {nameof(Options)} error:destination folder does not exisxt!");
                }
                else
                {
                    CreateDestination(this._DestinationFolder);
                }


            }
            if (!Directory.Exists(this._DestinationFolder))
            {
                throw new Exception($"{nameof(ExportActionBase)} {nameof(Options)} error:destination folder does not exisxt!");
            }



        }

        private void CheckSourceAndDest()
        {
            if (!this._Options.HasSourceFolder)
            {
                throw new Exception($"{nameof(ExportActionBase)} {nameof(Options)} error:missing source Folder!");
            }
            if (!this._Options.HasDestFolder)
            {
                throw new Exception($"{nameof(ExportActionBase)} {nameof(Options)} error:missing destination Folder!");

            }



        }
        protected abstract void Execute();

        public void Run()
        {
            CheckSourceAndDest();
            BuidlSourceFile();
            BildDestinationFolder();
            Execute();

        }

        protected void Log(string message)
        {
            Console.WriteLine(message);
        }

    }
}
