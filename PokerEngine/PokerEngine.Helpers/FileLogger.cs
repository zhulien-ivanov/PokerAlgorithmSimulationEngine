using System;
using System.IO;

using PokerEngine.Helpers.Contracts;

namespace PokerEngine.Helpers
{
    public class FileLogger : ILogger
    {
        private string path;
        private string separator;

        public FileLogger(char separator, int separatorRepeatCount)
        {
            this.path = Environment.CurrentDirectory + @"\history.txt";

            this.separator = new String(separator, separatorRepeatCount);
        }
        
        public void Log(string message)
        {
            using (var sw = new StreamWriter(this.path, true))
            {
                sw.WriteLine(String.Format("[{0}] {1}", DateTime.Now, message));
            }
        }

        public void AddSeparator()
        {
            using (var sw = new StreamWriter(this.path, true))
            {
                sw.WriteLine(this.separator);
            }
        }
    }
}
