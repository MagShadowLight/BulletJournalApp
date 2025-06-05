using BulletJournalApp.Core.Models;
using BulletJournalApp.Core.Models.Enum;
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
        public List<Tasks> ListTasksByPriority(Priority priority);
        public List<Tasks> ListTasksByCategory(Category category);
        public List<Tasks> ListTasksByStatus(TasksStatus status);
        public List<Tasks> ListTasksBySchedule(Schedule schedule);
        public Tasks FindTasksByTitle(string title);
        public void MarkTasksComplete(string title);
        public void UpdateTask(string oldTitle, string newTitle, string newDescription, string newNote, DateTime? newDueDate);
        public void ChangePriority(string title, Priority priority);
        public void ChangeCategory(string title, Category category);
        public void ChangeStatus(string title, TasksStatus status);
        public void ChangeSchedule(string title, Schedule schedule);
        public void DeleteTask(string title);
        public void SaveTasks(string filename, List<Tasks> tasks);
        public List<Tasks> LoadTasks(string filename);
    }
}
