using BulletJournalApp.Core.Interface;
using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Services
{
    public class Formatter : IFormatter
    {
        public string Format(Tasks task)
        {
            return $"[{task.Id}]: {task.Title}\n" +
                $"- Description: {task.Description}\n" +
                $"- Priority: {task.Priority.ToString()}\n" +
                $"- Due Date: {task.DueDate.ToString()}\n" +
                $"- Status: {task.Status.ToString()}\n" +
                $"- Category: {task.Category.ToString()}\n" +
                $"- Note: {task.Notes}\n" +
                $"{(task.IsCompleted ? "Completed": "Incomplete")}";
        }
    }
}
