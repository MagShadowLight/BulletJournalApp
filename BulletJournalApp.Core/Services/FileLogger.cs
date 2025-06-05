using BulletJournalApp.Core.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Services
{
    public class FileLogger : IFileLogger
    {
        public string path = Path.Combine("Temp", "Log.txt");
        public string directory = Path.Combine("Temp");
        public void Error(string message)
        {
            using (StreamWriter fs = File.AppendText(path))
            {
                fs.WriteLine($"[ERROR]: {message}");
                fs.Close();
            }
        }

        public void Log(string message)
        {
            using (StreamWriter fs = File.AppendText(path))
            {
                fs.WriteLine($"[LOG]: {message}");
                fs.Close();
            }
        }

        public void Warn(string message)
        {
            using (StreamWriter fs = File.AppendText(path))
            {
                fs.WriteLine($"[WARNING]: {message}");
                fs.Close();
            }
        }

        public void WriteText(FileStream fs, string usage, string message)
        {
            byte[] info = new UTF8Encoding(true).GetBytes($"[{usage}]: {message}\n");
            fs.Write(info, 0, info.Length);
        }
    }
}
