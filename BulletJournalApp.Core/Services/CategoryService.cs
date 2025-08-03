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
        private readonly IItemService _itemservice;

        public CategoryService(IConsoleLogger consolelogger, IFileLogger filelogger, IFormatter formatter, ITaskService taskService,  IItemService itemservice)
        {
            _taskservice = taskService;
            _consolelogger = consolelogger;
            _filelogger = filelogger;
            _formatter = formatter;
            _itemservice = itemservice;
        }

        public void ChangeCategory(string title, Entries entries, Category category)
        {
            switch (entries)
            {
                case Entries.TASKS:
                    var task = _taskservice.FindTasksByTitle(title);
                    if (task == null)
                        throw new ArgumentNullException("Cannot find task");
                    task.ChangeCategory(category);
                    break;
                case Entries.ITEMS:
                    var item = _itemservice.FindItemsByName(title);
                    if (item == null)
                        throw new ArgumentNullException("Cannot find task");
                    item.ChangeCategory(category);
                    break;
            }
            
        }

        public List<Items> ListItemsByCategory(Category category)
        {
            var items = _itemservice.GetAllItems();
            return items.Where(cat => cat.Category == category).ToList();
        }

        public List<Tasks> ListTasksByCategory(Category category)
        {
            var tasks = _taskservice.ListAllTasks();
            return tasks.Where(cat => cat.Category == category).ToList();
        }
    }
}
