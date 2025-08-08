using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.UI
{
    public class ConsoleUI
    {
        private readonly TaskManager _taskManager;
        private readonly IFileLogger _filelogger;
        private readonly IConsoleLogger _consolelogger;
        private readonly ShopListManager _shoplistmanager;
        private readonly MealPlanManager _mealplanmanager;

        public ConsoleUI(TaskManager taskManager, IFileLogger filelogger, IConsoleLogger consolelogger, ShopListManager shoplistmanager, MealPlanManager mealplanmanager)
        {
            _filelogger = filelogger;
            _taskManager = taskManager;
            _consolelogger = consolelogger;
            _shoplistmanager = shoplistmanager;
            _mealplanmanager = mealplanmanager;
        }
        public void Run()
        {
            _filelogger.Log("Starting Bullet Journal App");
            _filelogger.Log("Opening UI");
            while (true)
            {
                Console.WriteLine("Bullet Journal App");
                Console.WriteLine("Select which one to open");
                Console.WriteLine("1. To Do List");
                Console.WriteLine("2. Shopping List");
                Console.WriteLine("3. Meal Plan");
                Console.WriteLine("0: Quit");
                Console.Write("Choose an option: ");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        _filelogger.Log("Opening Task Manager");
                        Console.WriteLine("Opening Task Manager");
                        _taskManager.TaskManagerUI();
                        _filelogger.Log("Task manager closed");
                        break;
                    case "2":
                        _filelogger.Log("Opening Shopping List Manager");
                        Console.WriteLine("Opening Shopping List");
                        _shoplistmanager.UI();
                        _filelogger.Log("Shopping List Manager Closed");
                        break;
                    case "3":
                        _filelogger.Log("Opening Meal Plan Manager");
                        Console.WriteLine("Opening Meal Plan");
                        _mealplanmanager.MealPlanUI();
                        _filelogger.Log("Meal Plan Manager Closed");
                        break;
                    case "0":
                        _filelogger.Log("Quitting");
                        Console.WriteLine("Goodbye");
                        _filelogger.Log($"Exited Bullet Journal App at {DateTime.Now.ToString()}");
                        return;
                    default:
                        _consolelogger.Error("Invalid Choice. Please Try Again");
                        _filelogger.Error("Invalid Choice. Please Try Again");
                        break;
                }
            }
        }
    }
}
