using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.UI.Util
{
    public interface IUserInput
    {
        public string GetStringInput(string prompt);
        public DateTime GetDateInput(string prompt);
        public Priority GetPriorityInput(string prompt);
        public Category GetCategoryInput(string prompt);
        public Schedule GetScheduleInput(string prompt);
        public TasksStatus GetTaskStatusInput(string prompt);
        public ItemStatus GetItemStatusInput(string prompt);
        public bool GetBooleanInput(string prompt);
        public int GetIntInput(string prompt);
        public DateTime GetOptionalDateInput(string prompt);
    }
}
