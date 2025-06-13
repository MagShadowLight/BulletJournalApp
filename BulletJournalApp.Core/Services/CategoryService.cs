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
    public class CategoryService : ICategoryService
    {

        private readonly IFormatter _formatter;
        private readonly ITaskService _taskservice;
        private readonly IConsoleLogger _consolelogger;
        private readonly IFileLogger _filelogger;

        public CategoryService(ITaskService taskService, IConsoleLogger consolelogger, IFileLogger filelogger, IFormatter formatter)
        {
            _taskservice = taskService;
            _consolelogger = consolelogger;
            _filelogger = filelogger;
            _formatter = formatter;
            
        }

        public void ChangeCategory(string title, Category category)
        {
            var task = _taskservice.FindTasksByTitle(title);
            if (task == null)
                throw new Exception("Cannot find task");
            task.ChangeCategory(category);
        }

        public List<Tasks> ListTasksByCategory(Category category)
        {
            var tasks = _taskservice.ListAllTasks();
            return tasks.Where(cat => cat.Category == category).ToList();
        }
    }
}
