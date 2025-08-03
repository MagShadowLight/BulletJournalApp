using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Test.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Service
{
    public class PriorityServiceTest
    {
        private Formatter _formatter = new Formatter();
        private FileLogger _filelogger = new FileLogger();
        private ConsoleLogger _consolelogger = new ConsoleLogger();
        private TaskService _taskService;
        private PriorityService _priorityService;

        public PriorityServiceTest()
        {
            _taskService = new TaskService(_formatter, _consolelogger, _filelogger);
            _priorityService = new PriorityService(_taskService, _consolelogger, _filelogger, _formatter);
            var data = new PriorityServiceData();
            data.SetUpTasks(_taskService);
        }

        [Theory]
        [MemberData(nameof(PriorityServiceData.GetStringAndPriorityValue), MemberType = typeof(PriorityServiceData))]
        public void Given_There_Are_Tasks_In_The_List_When_Changing_The_Priority_Then_Task_Should_Updated_With_New_Priority(string title, Priority priority)
        {
            // Arrange
            int num = 5;
            // Act
            _priorityService.ChangePriority(title, priority);
            var tasks = _taskService.ListAllTasks();
            var task = _taskService.FindTasksByTitle(title);
            // Assert
            Assert.Equal(num, tasks.Count);
            Assert.Equal(priority, task.Priority);
            Assert.Throws<ArgumentNullException>(() => _priorityService.ChangePriority(null, priority));
        }
        [Theory]
        [MemberData(nameof(PriorityServiceData.GetPriorityValue), MemberType = typeof(PriorityServiceData))]
        public void Given_There_Are_Tasks_In_The_List_When_Listing_Tasks_With_Priority_Value_Then_Tasks_Should_Returns_With_Just_Only_Priority(Priority priority, int num)
        {
            // Arrange // Act
            var tasks = _priorityService.ListTasksByPriority(priority);
            // Assert
            Assert.Equal(num, tasks.Count);
        }
    }
}
