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
        private readonly List<Tasks> tasks = new();
        public FileService(IFormatter formatter, IConsoleLogger consolelogger, IFileLogger filelogger, ITaskService taskService)
        {
            _formatter = formatter;
            _consolelogger = consolelogger;
            _filelogger = filelogger;
            _taskservice = taskService;
        }
        public void LoadTasks(string filename)
        {
            Validate(filename, nameof(filename));
            var path = Path.Combine("Data", "Tasks", $"{filename}.txt");
            StreamRead(path);
        }
        public void StreamRead(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {

                    var task = SaveAndLoadTasks.LoadTasks(sr);
                    _taskservice.AddTask(task);
                }
                sr.Dispose();
            }
        }
        public void SaveTasks(string filename, List<Tasks> tasks)
        {
            var dir = Path.Combine("Data", "Tasks");
            var path = Path.Combine("Data", "Tasks", $"{filename}.txt");
            Validate(filename, nameof(filename));
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (!File.Exists(path))
            {
                StreamWrite(tasks, path);
            }
        }
        public void StreamWrite(List<Tasks> tasks, string path)
        {
            using (FileStream fs = File.Create(path))
            {
                foreach (var task in tasks)
                {
                    SaveAndLoadTasks.SaveTasks(task, fs);
                }
                fs.Close();
            }
        }

        public void Validate(string input, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException($"{fieldName} cannot be blank");
        }
    }
    public class SaveAndLoadTasks
    {
        public SaveAndLoadTasks() { }

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
            WriteText(fs, task.Schedule.ToString());
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
            return new Tasks(dueDate, title, description, schedule, priority, category, notes, status, id, isCompleted);
        }

        private static void WriteText(FileStream fs, string text)
        {
            byte[] info = new UTF8Encoding(true).GetBytes($"{text}\n");
            fs.Write(info, 0, info.Length);
        }
    }
}
