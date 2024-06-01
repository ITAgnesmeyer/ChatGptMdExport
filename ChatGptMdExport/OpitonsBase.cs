namespace ChatGptMdExport
{
    internal class OpitonsBase
    {
        private Dictionary<string, string?> _Args;

        private static Dictionary<string, string?> BuidlArgs(string[] args)
        {
            //Dictionary<string, string?> returnArgs = args.ToDictionary(
            //    k => k.Split(new[] { ':' }, 2)[0].ToLower(),
            //    v => v.Split(new[] { ':' }, 2).Count() > 1
            //        ? v.Split(new[] { ':' }, 2)[1]
            //        : null);
            Dictionary<string, string?> returnArgs = new Dictionary<string, string?>();
            try
            {
                foreach (var arg in args)
                {
                    var parts = arg.Split(new[] { ':' }, 2);
                    string key = parts[0].ToLower();
                    string? value = parts.Length > 1 ? parts[1] : null;

                    returnArgs.Add(key, value);
                }

            }
            catch (Exception ex)
            {

                throw new ArgumentException("Invalid arguments", ex);
            }


            return returnArgs;
        }

        public OpitonsBase(string[] args)
        {
            this._Args = BuidlArgs(args);
        }

        protected bool HasArg(string arg)
        {
            return _Args.ContainsKey(arg);
        }

        protected string? GetArg(string arg)
        {
            if (!HasArg(arg))
                throw new Exception($"Arg not available arg={arg}");
            return this._Args[arg];
        }
    }
}
