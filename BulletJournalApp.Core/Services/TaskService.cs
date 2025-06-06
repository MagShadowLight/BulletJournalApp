using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Models;
using BulletJournalApp.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Services
{
    public class TaskService : ITaskService
    {

        private readonly IFormatter _formatter;
        private readonly IConsoleLogger _consolelogger;
        private readonly IFileLogger _filelogger;
        private readonly List<Tasks> tasks = new();

        public TaskService(IFormatter formatter, IConsoleLogger consolelogger, IFileLogger filelogger)
        {
            _formatter = formatter;
            _consolelogger = consolelogger;
            _filelogger = filelogger;
        }

        public bool AddTask(Tasks task)
        {
            if(task == null)
            {
                _consolelogger.Error("Failed to create assignment");
                _filelogger.Error("Failed to create assignment");
                throw new ArgumentNullException(nameof(task));
            }
            tasks.Add(task);
            return true;
        }

        public void ChangeCategory(string title, Category category)
        {
            var task = FindTasksByTitle(title);
            if (task == null)
                throw new Exception("Cannot find task");
            task.ChangeCategory(category);
        }

        public void ChangePriority(string title, Priority priority)
        {
            var task = FindTasksByTitle(title);
            if (task == null)
                throw new Exception("Cannot find task");
            task.ChangePriority(priority);
        }

        public void ChangeSchedule(string title, Schedule schedule)
        {
            var tasks = FindTasksByTitle(title);
            if (tasks == null)
                throw new Exception("Cannot find task");
            tasks.ChangeSchedule(schedule);
        }

        public void ChangeStatus(string title, TasksStatus status)
        {
            var task = FindTasksByTitle(title);
            if (task == null)
                throw new Exception("Cannot find task");
            task.ChangeStatus(status);
        }

        public void DeleteTask(string title)
        {
            var task = FindTasksByTitle(title);
            if (task == null)
                throw new Exception("Cannot find task");
            tasks.Remove(task);            
        }

        public Tasks FindTasksByTitle(string title)
        {
            return tasks.FirstOrDefault(a => a.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public List<Tasks> ListAllTasks()
        {
            return tasks;
        }

        public List<Tasks> ListIncompleteTasks()
        {
            return tasks.Where(incomplete => !incomplete.IsCompleted).ToList();
        }

        public List<Tasks> ListTasksByCategory(Category category)
        {
            return tasks.Where(cat => cat.Category == category).ToList();
        }

        public List<Tasks> ListTasksByPriority(Priority priority)
        {
            return tasks.Where(priorities => priorities.Priority == priority).ToList();
        }

        public List<Tasks> ListTasksBySchedule(Schedule schedule)
        {
            return tasks.Where(time => time.schedule == schedule).ToList();
        }

        public List<Tasks> ListTasksByStatus(TasksStatus status)
        {
            return tasks.Where(s => s.Status == status).ToList();
        }

        public void LoadTasks(string filename)
        {
            var path = Path.Combine("Data", "Tasks", $"{filename}.txt");
            StreamRead(path);
        }

        public void MarkTasksComplete(string title)
        {
            var task = FindTasksByTitle(title);
            if (task == null)
                throw new Exception("Cannot find task");
            task.IsCompleted = true;
        }

        public void SaveTasks(string filename, List<Tasks> tasks)
        {
            var dir = Path.Combine("Data", "Tasks");
            var path = Path.Combine( "Data", "Tasks", $"{filename}.txt");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (!File.Exists(path))
            {
                StreamWrite(tasks, path);
            }
            
        }

        

        public void UpdateTask(string oldTitle, string newTitle, string newDescription, string newNote, DateTime? newDueDate)
        {
            var task = FindTasksByTitle(oldTitle);
            if (task == null)
                throw new Exception("Cannot find task");
            task.Update(newDueDate, newTitle, newDescription, newNote);
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

        public void StreamRead(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    
                    var task = SaveAndLoadTasks.LoadTasks(sr);
                    AddTask(task);
                }
                sr.Dispose();
            }
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
            WriteText(fs, task.schedule.ToString());
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
