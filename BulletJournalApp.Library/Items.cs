using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Library
{
    public class Items
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Schedule Schedule { get; set; }
        public Category Category { get; set; }
        public ItemStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateBought { get; set; }

        public Items(string name, string description, Schedule schedule, int id = 0, Category category = Category.None, ItemStatus status = ItemStatus.NotBought, string note = "", DateTime? dateadded = null, DateTime? datebought = null)
        {
            Validate(name, nameof(name));
            Validate(description, nameof(description));
            Id = id;
            Name = name;
            Description = description;
            Schedule = schedule;
            Category = category;
            Status = status;
            Notes = note;
            DateAdded = dateadded ?? DateTime.Today;
            DateBought = datebought ?? DateTime.MinValue;
        }

        public void Update(string newname, string newdescription, string? newnote)
        {
            Validate(newnote, nameof(newnote));
            Validate(newdescription, nameof(newdescription));
            Name = newname;
            Description = newdescription;
            Notes = newnote;
        }

        public void ChangeCategory(Category newcategory)
        {
            Category = newcategory;
        }

        public void ChangeSchedule(Schedule newschedule)
        {
            Schedule = newschedule;
        }

        public void ChangeStatus(ItemStatus newstatus)
        {
            Status = newstatus;
        }

        public void MarkAsBought()
        {
            Status = ItemStatus.Bought;
            DateBought = DateTime.Today;
        }

        public void Validate(string input, string fieldname)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException($"{fieldname} cannot be blank or null");
        }
    }
}
