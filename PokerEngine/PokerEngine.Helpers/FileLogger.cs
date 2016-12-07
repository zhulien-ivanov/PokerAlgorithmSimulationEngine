using System;
using System.IO;

using PokerEngine.Helpers.Contracts;

namespace PokerEngine.Helpers
{
    public class FileLogger : ILogger
    {
        private string path;

        public FileLogger()
        {
            this.path = Environment.CurrentDirectory + @"\history.txt";
        }

        public void Log(string message)
        {
            using (var sw = new StreamWriter(this.path, true))
            {
                sw.WriteLine(String.Format("[{0}] {1}", DateTime.Now, message));
            }
        }
    }
}
