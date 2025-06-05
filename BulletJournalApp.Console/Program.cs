using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Services;
using BulletJournalApp.UI;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace BulletJournalApp.Consoles
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            string path = Path.Combine("Temp", "Log.txt");
            if (!File.Exists(path))
            {
                //Console.WriteLine($"File Creating: {Path.GetFullPath(path)}");
                string dir = Path.Combine("Temp");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                File.Create(path).Close();
            }
            else
            {
                int num = 0;
                string newPath = Path.Combine("Temp", $"Log{DateTime.Today.Month.ToString()}-{DateTime.Today.Day.ToString()}-{DateTime.Today.Year.ToString()}-{num}.txt");
                if ((File.Exists(newPath)))
                {
                    while (File.Exists(newPath))
                    {
                        //Console.WriteLine("meow");
                        num++;
                        newPath = Path.Combine("Temp", $"Log{DateTime.Today.Month.ToString()}-{DateTime.Today.Day.ToString()}-{DateTime.Today.Year.ToString()}-{num}.txt");
                    }
                }
                File.Copy(path, newPath);
                File.Delete(path);
                File.Create(path).Close();
            }
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
    }
}
