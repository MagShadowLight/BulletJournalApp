using BulletJournalApp.Core.Interface;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
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
        public List<Tasks> tasks = new();

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
        public void DeleteTask(string title)
        {
            var task = FindTasksByTitle(title);
            if (task == null)
                throw new ArgumentNullException("Cannot find task");
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
        public void MarkTasksComplete(string title)
        {
            var task = FindTasksByTitle(title);
            if (task == null)
                throw new ArgumentNullException("Cannot find task");
            task.IsCompleted = true;
            if (task.IsRepeatable)
                task.RepeatTask();
        }
        public void UpdateTask(string oldTitle, string newTitle, string newDescription, string newNote, bool repeat, DateTime newDueDate, int newRepeatDay = 7, DateTime newEndRepeatDate = new DateTime())
        {
            var task = FindTasksByTitle(oldTitle);
            if (task == null)
                throw new ArgumentNullException("Cannot find task");
            task.Update(newDueDate, newTitle, newDescription, repeat, newNote);
        }
    }
}
