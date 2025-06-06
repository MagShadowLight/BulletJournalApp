using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Services;
using BulletJournalApp.UI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace BulletJournalApp.Consoles
{
    internal class Program
    {


        static void Main(string[] args)
        {

            var services = new ServiceCollection();

            services.AddSingleton<ITaskService, TaskService>();
            services.AddSingleton<IFormatter, Formatter>();
            services.AddSingleton<IConsoleLogger, ConsoleLogger>();
            services.AddSingleton<IFileLogger, FileLogger>();
            services.AddSingleton<ConsoleUI>();
            services.AddSingleton<TaskManager>();

            var serviceProvider = services.BuildServiceProvider();
            var consoleUI = serviceProvider.GetRequiredService<ConsoleUI>();


            consoleUI.Run();
        }

        public void CreateFile()
        {
            string path = Path.Combine("Temp", "Log.txt");
            if (!File.Exists(path))
            {
                string dir = Path.Combine("Temp");
                CreateDirectory(dir);
                File.Create(path).Close();
            }
            else
            {
                int num = 0;
                string newPath = Path.Combine("Temp", $"Log{DateTime.Today.Month.ToString()}-{DateTime.Today.Day.ToString()}-{DateTime.Today.Year.ToString()}-{num}.txt");
                IfFileExists(num, newPath);
                File.Copy(path, newPath);
                File.Delete(path);
                File.Create(path).Close();
            }
        }

        public void CreateDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
        public void IfFileExists(int num, string path)
        {
            if ((File.Exists(path)))
            {
                while (File.Exists(path))
                {
                    //Console.WriteLine("meow");
                    num++;
                    path = Path.Combine("Temp", $"Log{DateTime.Today.Month.ToString()}-{DateTime.Today.Day.ToString()}-{DateTime.Today.Year.ToString()}-{num}.txt");
                }
            }
        }
    }
}
