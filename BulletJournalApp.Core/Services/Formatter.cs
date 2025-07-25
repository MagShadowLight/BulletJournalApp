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
        public string FormatItems(Items item)
        {

            return $"[{item.Id}]: {item.Name}\n" +
                $"- Description: {item.Description}\n" +
                $"- Quantity: {item.Quantity}\n" +
                $"- Category: {item.Category.ToString()}\n" +
                $"- Status: {item.Status.ToString()}\n" +
                $"- Note: {item.Notes}\n" +
                $"- Date Added: {item.DateAdded}\n" +
                $"{(item.DateBought != DateTime.MinValue ? $"- Date Bought: {item.DateBought}" : "")}";
        }

        public string FormatTasks(Tasks task)
        {
            return $"[{task.Id}]: {task.Title}\n" +
                $"- Description: {task.Description}\n" +
                $"- Priority: {task.Priority.ToString()}\n" +
                $"- Due Date: {task.DueDate.ToString()}\n" +
                $"- Status: {task.Status.ToString()}\n" +
                $"- Category: {task.Category.ToString()}\n" +
                $"- Note: {task.Notes}\n" +
                $"{(task.IsCompleted ? "Completed": "Incomplete")}\n" +
                $"{(task.IsRepeatable ? (task.EndRepeatDate == DateTime.MinValue ? "Repeating Task" : $"Repeating until {task.EndRepeatDate}") : "Repeat: N/A")}";
        }
    }
}
