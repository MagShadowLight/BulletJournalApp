using BulletJournalApp.Library.Enum;
using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface IScheduleService
    {
        public List<Items> ListItemsBySchedule(Schedule schedule);
        public List<Tasks> ListTasksBySchedule(Schedule schedule);
        public void ChangeSchedule(string title, Entries entries, Schedule schedule);
    }
}
