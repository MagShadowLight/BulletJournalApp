using BulletJournalApp.Library.Enum;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Library
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public string? Notes { get; set; }
        public Category Category { get; set; }
        public TasksStatus Status { get; set; }
        public Schedule schedule { get; set; }
        public bool IsRepeatable { get; set; }
        public int RepeatDays { get; set; }
        public DateTime EndRepeatDate { get; set; }

        public Tasks(DateTime dueDate, string title, string description, Schedule timely, bool isrepeatable, int repeatdays = 7, DateTime endRepeatDate = new DateTime(), Priority priority = Priority.Medium, Category category = Category.None, string notes = "", TasksStatus status = TasksStatus.ToDo, int id = 0, bool iscompleted = false)
        {
            Validate(title, nameof(title));
            Validate(description, nameof(description));

            Id = id;
            DueDate = dueDate;
            Title = title;
            Description = description;
            Priority = priority;
            Status = status;
            schedule = timely;
            Notes = notes;
            Category = category;
            IsCompleted = iscompleted;
            IsRepeatable = isrepeatable;
            RepeatDays = repeatdays;
            EndRepeatDate = endRepeatDate;
        }

        public void Update(DateTime newDueDate, string newTitle, string newDescription, bool isrepeatable, string newNote = "", int repeatdays = 7, DateTime endrepeatdate = new DateTime())
        {
            Validate(newTitle, nameof(newTitle));
            Validate(newDescription, nameof(newDescription));

            Title = newTitle;
            Description = newDescription;
            Notes = newNote;
            DueDate = newDueDate;
            IsRepeatable = isrepeatable;
            RepeatDays = repeatdays;
            EndRepeatDate = endrepeatdate;
        }
        
        public void ChangePriority (Priority newpriority = Priority.Medium)
        {
            Priority = newpriority;
        }

        public void ChangeCategory(Category newcategory = Category.None)
        {
            Category = newcategory;
        }
        public void ChangeStatus(TasksStatus newstatus = TasksStatus.ToDo)
        {
            Status = newstatus;
        }
        public void MarkComplete()
        {
            IsCompleted = true;
        }
        public bool IsOverdue()
        {
            if (!IsCompleted && DueDate != DateTime.MinValue)
            {
                return DueDate < DateTime.Now;
            } else
            {
                return false;
            }
        }
        public void Validate(string input, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException($"{fieldName} cannot be blank or whitespace.");
        }

        public void ChangeSchedule(Schedule timely)
        {
            schedule = timely;
        }

        public void RepeatTask()
        {
            var newDueDate = DueDate;
            newDueDate = newDueDate.AddDays(RepeatDays);
            var remainder = EndRepeatDate.CompareTo(newDueDate);
            if (remainder < 0 && EndRepeatDate != DateTime.MinValue)
            {
                IsRepeatable = false;
                return;
            }
            DueDate = newDueDate;
            IsCompleted = false;
        }
    }

    
}
