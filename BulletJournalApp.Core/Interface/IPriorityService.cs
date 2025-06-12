using BulletJournalApp.Core.Models.Enum;
using BulletJournalApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface IPriorityService
    {
        public List<Tasks> ListTasksByPriority(Priority priority);
        public void ChangePriority(string title, Priority priority);
    }
}
