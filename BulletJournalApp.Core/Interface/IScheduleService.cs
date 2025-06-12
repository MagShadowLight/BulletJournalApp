using BulletJournalApp.Core.Models.Enum;
using BulletJournalApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface IScheduleService
    {
        public List<Tasks> ListTasksBySchedule(Schedule schedule);
        public void ChangeSchedule(string title, Schedule schedule);
    }
}
