using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Models;
using BulletJournalApp.Core.Models.Enum;
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

        public ScheduleService(IFormatter formatter, IConsoleLogger consolelogger, IFileLogger filelogger, ITaskService taskService)
        {
            _formatter = formatter;
            _consolelogger = consolelogger;
            _filelogger = filelogger;
            _taskservice = taskService;
        }

        public void ChangeSchedule(string title, Schedule schedule)
        {
            var tasks = _taskservice.FindTasksByTitle(title);
            if (tasks == null)
                throw new Exception("Cannot find task");
            tasks.ChangeSchedule(schedule);
        }

        public List<Tasks> ListTasksBySchedule(Schedule schedule)
        {
            var tasks = _taskservice.ListAllTasks();
            return tasks.Where(time => time.schedule == schedule).ToList();
        }
    }
}
