using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Services;
using BulletJournalApp.UI;
using BulletJournalApp.UI.Util;
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
            CreateFile();
            services.AddSingleton<ITaskService, TaskService>();
            services.AddSingleton<IFormatter, Formatter>();
            services.AddSingleton<IConsoleLogger, ConsoleLogger>();
            services.AddSingleton<IFileLogger, FileLogger>();
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IScheduleService, ScheduleService>();
            services.AddSingleton<IPriorityService, PriorityService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<ITasksStatusService, TasksStatusService>();
            services.AddSingleton<IItemStatusService, ItemStatusService>();
            services.AddSingleton<IUserInput, UserInput>();
            services.AddSingleton<IItemService, ItemService>();
            services.AddSingleton<ShopListManager>();
            services.AddSingleton<TaskManager>();
            services.AddSingleton<ConsoleUI>();

            var serviceProvider = services.BuildServiceProvider();
            var consoleUI = serviceProvider.GetRequiredService<ConsoleUI>();


            consoleUI.Run();
        }

        public static void CreateFile()
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
                num = IfFileExists(num, newPath);
                newPath = Path.Combine("Temp", $"Log{DateTime.Today.Month.ToString()}-{DateTime.Today.Day.ToString()}-{DateTime.Today.Year.ToString()}-{num}.txt");
                File.Copy(path, newPath);
                File.Delete(path);
                File.Create(path).Close();
            }
        }

        public static void CreateDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
        public static int IfFileExists(int num, string path)
        {
            if ((File.Exists(path)))
            {
                while (File.Exists(path))
                {
                    //Console.WriteLine("meow");
                    num++;
                    path = Path.Combine("Temp", $"Log{DateTime.Today.Month.ToString()}-{DateTime.Today.Day.ToString()}-{DateTime.Today.Year.ToString()}-{num}.txt");
                }
                return num;
            }
            return num;
        }
    }
}
