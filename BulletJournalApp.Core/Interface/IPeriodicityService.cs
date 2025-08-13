using BulletJournalApp.Library.Enum;
using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface IPeriodicityService
    {
        public List<Items> ListItemsBySchedule(Periodicity schedule);
        public List<Tasks> ListTasksBySchedule(Periodicity schedule);
        public List<Routines> ListRoutinesByPeriodicity(Periodicity periodicity);
        public void ChangeSchedule(string title, Entries entries, Periodicity schedule);
    }
}
