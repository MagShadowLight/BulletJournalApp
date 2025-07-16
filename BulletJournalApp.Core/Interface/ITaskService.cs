using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface ITaskService
    {
        public bool AddTask(Tasks task);
        public List<Tasks> ListAllTasks();
        public List<Tasks> ListIncompleteTasks();
        public Tasks FindTasksByTitle(string title);
        public void MarkTasksComplete(string title);
        public void UpdateTask(string oldTitle, string newTitle, string newDescription, string newNote, bool repeat, DateTime? newDueDate, int newRepeatDay = 7, DateTime newEndRepeatDate = new DateTime());
        public void DeleteTask(string title);
    }
}
