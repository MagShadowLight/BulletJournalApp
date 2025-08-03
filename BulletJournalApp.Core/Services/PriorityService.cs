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
    public class PriorityService : IPriorityService
    {

        private readonly IFormatter _formatter;
        private readonly ITaskService _taskservice;
        private readonly IConsoleLogger _consolelogger;
        private readonly IFileLogger _filelogger;

        public PriorityService(ITaskService taskService, IConsoleLogger consoleLogger, IFileLogger fileLogger, IFormatter formatter)
        {
            _taskservice = taskService;
            _consolelogger = consoleLogger;
            _filelogger = fileLogger;
            _formatter = formatter;
        }

        public void ChangePriority(string title, Priority priority)
        {
            var task = _taskservice.FindTasksByTitle(title);
            if (task == null)
                throw new ArgumentNullException("Cannot find task");
            task.ChangePriority(priority);
        }

        public List<Tasks> ListTasksByPriority(Priority priority)
        {
            var tasks = _taskservice.ListAllTasks();
            return tasks.Where(priorities => priorities.Priority == priority).ToList();
        }
    }
}
