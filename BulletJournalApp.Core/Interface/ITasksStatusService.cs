using BulletJournalApp.Library.Enum;
using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface ITasksStatusService
    {
        public List<Tasks> ListTasksByStatus(TasksStatus status);
        public void ChangeStatus(string title, TasksStatus status);
    }
}
