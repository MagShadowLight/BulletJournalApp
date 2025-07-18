using BulletJournalApp.Core.Interface;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Services
{
    public class FileService : IFileService
    {
        private readonly IFormatter _formatter;
        private readonly ITaskService _taskservice;
        private readonly IConsoleLogger _consolelogger;
        private readonly IFileLogger _filelogger;
        private readonly IItemService _itemservice;
        private readonly List<Tasks> tasks = new();
        public FileService(IFormatter formatter, IConsoleLogger consolelogger, IFileLogger filelogger, ITaskService taskService, IItemService itemservice)
        {
            _formatter = formatter;
            _consolelogger = consolelogger;
            _filelogger = filelogger;
            _taskservice = taskService;
            _itemservice = itemservice;
        }
        public void LoadFunction(string filename, Entries entries)
        {
            Validate(filename, nameof(filename));
            var path = Path.Combine("Data", entries.ToString(), $"{filename}.txt");
            StreamRead(path, entries);
        }
        public void StreamRead(string path, Entries entries)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    switch (entries) {
                        case Entries.TASKS:
                            var task = DataManagement.LoadTasks(sr);
                            _taskservice.AddTask(task);
                            break;
                        case Entries.ITEMS:
                            var item = DataManagement.LoadItems(sr);
                            _itemservice.AddItems(item);
                            break;
                    }
                }
                sr.Dispose();
            }
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        public void SaveFunction(string filename, Entries entries, List<Tasks>? tasks, List<Items>? items)
        {
            var dir = Path.Combine("Data", entries.ToString());
            var path = Path.Combine("Data", entries.ToString(), $"{filename}.txt");
            Validate(filename, nameof(filename));
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (!File.Exists(path))
            {
                StreamWrite(tasks, items, entries, path);
            }
        }
        public void StreamWrite(List<Tasks>? tasks, List<Items>? items, Entries entries, string path)
        {
            switch (entries)
            {
                case Entries.TASKS:
                    using (FileStream fs = File.Create(path))
                    {
                        foreach (var task in tasks)
                        {
                            DataManagement.SaveTasks(task, fs);
                        }
                        fs.Close();
                    }
                    Console.SetIn(new StreamReader(Console.OpenStandardInput()));
                    break;
                case Entries.ITEMS:
                    using (FileStream fs = File.Create(path))
                    {
                        foreach (var item in items)
                        {
                            DataManagement.SaveItems(item, fs);
                        }
                        fs.Close();
                    }
                    Console.SetIn(new StreamReader(Console.OpenStandardInput()));
                    break;
            }
        }

        public void Validate(string input, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException($"{fieldName} cannot be blank");
        }
    }
    public class DataManagement
    {
        public DataManagement() { }

        public static void SaveTasks(Tasks task, FileStream fs)
        {
            WriteText(fs, task.Id.ToString());
            WriteText(fs, task.Title);
            WriteText(fs, task.Description);
            WriteText(fs, task.Priority.ToString());
            WriteText(fs, task.DueDate.ToString());
            WriteText(fs, task.IsCompleted.ToString());
            WriteText(fs, task.Notes);
            WriteText(fs, task.Category.ToString());
            WriteText(fs, task.Status.ToString());
            WriteText(fs, task.schedule.ToString());
            WriteText(fs, task.IsRepeatable.ToString());
            WriteText(fs, task.RepeatDays.ToString());
            WriteText(fs, task.EndRepeatDate.ToString());
        }

        public static void SaveItems(Items items, FileStream fs)
        {
            WriteText(fs, items.Id.ToString());
            WriteText(fs, items.Name);
            WriteText(fs, items.Description);
            WriteText(fs, items.Schedule.ToString());
            WriteText(fs, items.Category.ToString());
            WriteText(fs, items.Status.ToString());
            WriteText(fs, items.Notes);
            WriteText(fs, items.DateAdded.ToString());
            WriteText(fs, items.DateBought.ToString());
        }

        public static Tasks LoadTasks(StreamReader sr)
        {
            var id = int.Parse(sr.ReadLine());
            var title = sr.ReadLine();
            var description = sr.ReadLine();
            var priority = (Priority)Enum.Parse(typeof(Priority), sr.ReadLine());
            var dueDate = DateTime.TryParse(sr.ReadLine(), out DateTime parsedDate) ? parsedDate : (DateTime?)null;
            var isCompleted = bool.Parse(sr.ReadLine());
            var notes = sr.ReadLine();
            var category = (Category)Enum.Parse(typeof(Category), sr.ReadLine());
            var status = (TasksStatus)Enum.Parse(typeof(TasksStatus), sr.ReadLine());
            var schedule = (Schedule)Enum.Parse(typeof(Schedule), sr.ReadLine());
            var isRepeatable = bool.Parse(sr.ReadLine());
            var repeatDays = int.Parse(sr.ReadLine());
            var endRepeatDate = DateTime.TryParse(sr.ReadLine(), out DateTime parsedRepeatDate) ? parsedRepeatDate : DateTime.MinValue;
            return new Tasks(dueDate, title, description, schedule, isRepeatable, repeatDays, endRepeatDate, priority, category, notes, status, id, isCompleted);
        }

        public static Items LoadItems(StreamReader sr)
        {
            var id = int.Parse(sr.ReadLine());
            var name = sr.ReadLine();
            var description = sr.ReadLine();
            var schedule = (Schedule)Enum.Parse(typeof(Schedule), sr.ReadLine());
            var category = (Category)Enum.Parse(typeof(Category), sr.ReadLine());
            var status = (ItemStatus)Enum.Parse(typeof (ItemStatus), sr.ReadLine());
            var note = sr.ReadLine();
            var dateAdded = DateTime.TryParse(sr.ReadLine(), out DateTime parsedAddedDate) ? parsedAddedDate : DateTime.MinValue;
            var dateBought = DateTime.TryParse(sr.ReadLine(), out DateTime parsedBoughtDate) ? parsedBoughtDate : DateTime.MinValue;
            return new Items(name, description, schedule, id, 1, category, status, note, dateAdded, dateBought);
        }

        private static void WriteText(FileStream fs, string text)
        {
            byte[] info = new UTF8Encoding(true).GetBytes($"{text}\n");
            fs.Write(info, 0, info.Length);
        }
    }
}
