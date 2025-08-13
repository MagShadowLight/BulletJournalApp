using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Library
{
    public class Routines
    {
        public int Id;
        public string Name;
        public string Description;
        public Periodicity Periodicity;
        public Category Category;
        public string? Notes;
        public List<string> TaskList;

        public Routines(string name, string description, Category category, List<string> taskList, Periodicity periodicity = Periodicity.Monthly, string note = "", int id = 0)
        {
            Validate(name, nameof(name));
            Validate(description, nameof(description));
            ValidateList(taskList, nameof(taskList));
            Name = name;
            Description = description;
            Category = category;
            TaskList = taskList;
            Periodicity = periodicity;
            Notes = note;
            Id = id;
        }
        public void UpdateRoutine(string newname, string newdescription, string newnote = "")
        {
            Validate(newname, nameof(newname));
            Validate(newdescription, nameof(newdescription));
            Name = newname;
            Description = newdescription;
            Notes = newnote;
        }
        public void ChangeCategory(Category newcategory)
        {
            Category = newcategory;
        }
        public void ChangePeriodicity(Periodicity newperiodicity)
        {
            Periodicity = newperiodicity;
        }
        public void ChangeTaskList(List<string> newtasklist)
        {
            ValidateList(newtasklist, nameof(newtasklist));
            TaskList = newtasklist;
        }
        private void Validate(string input, string fieldname)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException($"{fieldname} must not be null or empty");
        }
        private void ValidateList(List<string> list, string fieldname)
        {
            if (list.Count == 0)
                throw new FormatException($"{fieldname} must not be empty list");
        }
    }
}
