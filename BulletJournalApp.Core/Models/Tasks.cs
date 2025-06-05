using BulletJournalApp.Core.Models.Enum;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public string? Notes { get; set; }
        public Category Category { get; set; }
        public TasksStatus Status { get; set; }
        public Schedule schedule { get; set; }

        public Tasks(DateTime? dueDate, string title, string description, Schedule timely, Priority priority = Priority.Medium, Category category = Category.None, string notes = "", TasksStatus status = TasksStatus.ToDo, int id = 0, bool iscompleted = false)
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
        }

        public void Update(DateTime? newDueDate, string newTitle, string newDescription, string newNote = "")
        {
            Validate(newTitle, nameof(newTitle));
            Validate(newDescription, nameof(newDescription));

            Title = newTitle;
            Description = newDescription;
            Notes = newNote;
            DueDate = newDueDate;
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
            if (!IsCompleted && DueDate != null)
            {
                return DueDate.Value < DateTime.Now;
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
    }

    
}
