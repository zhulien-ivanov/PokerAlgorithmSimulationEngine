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
            var currentTime = DateTime.Now;
            var currentDate = String.Format("{0}.{1}.{2}-T-{3}.{4}.{5}", currentTime.Day, currentTime.Month, currentTime.Year, currentTime.Hour, currentTime.Minute, currentTime.Second);

            var fileName = String.Format(@"\history-{0}.txt", currentDate);

            this.path = Environment.CurrentDirectory + fileName;

            this.separator = new String(separator, separatorRepeatCount);
        }
        
        public void Log(string message)
        {
            using (var sw = new StreamWriter(this.path, true))
            {
                sw.WriteLine(String.Format("[{0}] {1}", DateTime.Now, message));
            }
        }

        public void LogError(string message)
        {
            this.Log(String.Format("ERROR: {0}", message));
        }

        public void LogInfo(string message)
        {
            this.Log(String.Format("INFO: {0}", message));
        }

        public void LogWarning(string message)
        {
            this.Log(String.Format("WARNING: {0}", message));
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
