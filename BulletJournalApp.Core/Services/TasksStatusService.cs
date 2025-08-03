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
    public class TasksStatusService : ITasksStatusService
    {

        private readonly IFormatter _formatter;
        private readonly ITaskService _taskservice;
        private readonly IConsoleLogger _consolelogger;
        private readonly IFileLogger _filelogger;

        public TasksStatusService(ITaskService taskService, IConsoleLogger consolelogger, IFileLogger filelogger, IFormatter formatter)
        {
            _taskservice = taskService;
            _consolelogger = consolelogger;
            _filelogger = filelogger;
            _formatter = formatter;
        }

        public void ChangeStatus(string title, TasksStatus status)
        {
            var task = _taskservice.FindTasksByTitle(title);
            if (task == null)
                throw new ArgumentNullException("Cannot find task");
            task.ChangeStatus(status);
        }

        public List<Tasks> ListTasksByStatus(TasksStatus status)
        {
            var tasks = _taskservice.ListAllTasks();
            return tasks.Where(s => s.Status == status).ToList();
        }
    }
}
