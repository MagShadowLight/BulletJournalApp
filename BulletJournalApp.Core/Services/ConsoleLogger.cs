using BulletJournalApp.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Services
{
    public class ConsoleLogger : IConsoleLogger
    {
        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR]: {message}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void Log(string message)
        {
            Console.WriteLine($"[LOG]: {message}");
        }

        public void Warn(string message)
        {
            Console.ForegroundColor= ConsoleColor.Yellow;
            Console.WriteLine($"[WARNING]: {message}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
