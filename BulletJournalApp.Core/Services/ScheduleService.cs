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
    public class ScheduleService : IScheduleService
    {

        private readonly IFormatter _formatter;
        private readonly ITaskService _taskservice;
        private readonly IConsoleLogger _consolelogger;
        private readonly IFileLogger _filelogger;
        private readonly IItemService _itemservice;
        private readonly Entries _entries;

        public ScheduleService(IFormatter formatter, IConsoleLogger consolelogger, IFileLogger filelogger, ITaskService taskService, IItemService itemService)
        {
            _formatter = formatter;
            _consolelogger = consolelogger;
            _filelogger = filelogger;
            _taskservice = taskService;
            _itemservice = itemService;
        }

        public void ChangeSchedule(string title, Entries entries, Schedule schedule)
        {
            switch(entries)
            {
                case Entries.TASKS:
                    var tasks = _taskservice.FindTasksByTitle(title);
                    if (tasks == null)
                        throw new Exception("Cannot find task");
                    tasks.ChangeSchedule(schedule);
                    break;
                case Entries.ITEMS:
                    var items = _itemservice.FindItemsByName(title);
                    if (items == null)
                        throw new Exception("Cannot find task");
                    items.ChangeSchedule(schedule);
                    break;
            }
        }

        public List<Items> ListItemsBySchedule(Schedule schedule)
        {
            List<Items> items = _itemservice.GetAllItems();
            return items.Where(time => time.Schedule == schedule).ToList();
        }

        public List<Tasks> ListTasksBySchedule(Schedule schedule)
        {
            List<Tasks> tasks = _taskservice.ListAllTasks();
            return tasks.Where(time => time.schedule == schedule).ToList();
        }
    }
}
