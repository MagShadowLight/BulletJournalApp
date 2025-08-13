using BulletJournalApp.Core.Interface;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Services
{
    public class PeriodicityService : IPeriodicityService
    {

        private readonly IFormatter _formatter;
        private readonly ITaskService _taskservice;
        private readonly IConsoleLogger _consolelogger;
        private readonly IFileLogger _filelogger;
        private readonly IItemService _itemservice;
        private readonly IRoutineService _routineservice;
        private readonly Entries _entries;

        public PeriodicityService(IFormatter formatter, IConsoleLogger consolelogger, IFileLogger filelogger, ITaskService taskService, IItemService itemService, IRoutineService routineservice)
        {
            _formatter = formatter;
            _consolelogger = consolelogger;
            _filelogger = filelogger;
            _taskservice = taskService;
            _itemservice = itemService;
            _routineservice = routineservice;
        }

        public void ChangeSchedule(string title, Entries entries, Periodicity schedule)
        {
            switch(entries)
            {
                case Entries.TASKS:
                    var tasks = _taskservice.FindTasksByTitle(title);
                    if (tasks == null)
                        throw new ArgumentNullException("Cannot find task");
                    tasks.ChangeSchedule(schedule);
                    break;
                case Entries.ITEMS:
                    var items = _itemservice.FindItemsByName(title);
                    if (items == null)
                        throw new ArgumentNullException("Cannot find item");
                    items.ChangeSchedule(schedule);
                    break;
                case Entries.ROUTINES:
                    var routines = _routineservice.FindRoutineByName(title);
                    if (routines == null)
                        throw new ArgumentNullException("Cannot find routine");
                    routines.ChangePeriodicity(schedule);
                    break;
            }
        }

        public List<Items> ListItemsBySchedule(Periodicity schedule)
        {
            List<Items> items = _itemservice.GetAllItems();
            return items.Where(time => time.Schedule == schedule).ToList();
        }

        public List<Routines> ListRoutinesByPeriodicity(Periodicity periodicity)
        {
            List<Routines> routines = _routineservice.GetAllRoutines();
            return routines.Where(time => time.Periodicity == periodicity).ToList();
        }

        public List<Tasks> ListTasksBySchedule(Periodicity schedule)
        {
            List<Tasks> tasks = _taskservice.ListAllTasks();
            return tasks.Where(time => time.schedule == schedule).ToList();
        }
    }
}
