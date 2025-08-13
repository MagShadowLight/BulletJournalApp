using BulletJournalApp.Core.Services;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletJournalApp.Test.Core.Data;

namespace BulletJournalApp.Test.Core.Service
{
    public class TasksStatusServiceTest
    {
        private Formatter _formatter = new Formatter();
        private ConsoleLogger _consolelogger = new ConsoleLogger();
        private FileLogger _filelogger = new FileLogger();
        private TaskService _taskService;
        private TasksStatusService _tasksStatusService;

        public TasksStatusServiceTest()
        {
            _taskService = new TaskService(_formatter, _consolelogger, _filelogger);
            _tasksStatusService = new TasksStatusService(_taskService, _consolelogger, _filelogger, _formatter);
            var data = new TasksStatusServiceData();
            data.SetUpTasks(_taskService);
        }

        [Theory]
        [MemberData(nameof(TasksStatusServiceData.GetStatusAndStringValue), MemberType =typeof(TasksStatusServiceData))]
        public void Given_There_Are_Tasks_In_The_List_When_Changing_The_Status_Then_Task_Should_Be_Changed_With_New_Status(string title, TasksStatus status)
        {
            // Arrange
            int num = 5;
            // Act
            _tasksStatusService.ChangeStatus(title, status);
            var tasks = _taskService.ListAllTasks();
            var task = _taskService.FindTasksByTitle(title);
            // Assert
            Assert.Equal(num, tasks.Count);
            Assert.Equal(status, task.Status);
            Assert.Throws<ArgumentNullException>(() => _tasksStatusService.ChangeStatus(null, status));
        }
        [Theory]
        [MemberData(nameof(TasksStatusServiceData.GetStatusValue), MemberType =typeof(TasksStatusServiceData))]
        public void Given_There_Are_Tasks_In_The_List_When_Listing_The_Tasks_With_Specific_Status_Then_It_Should_Return_List_Of_Tasks_With_Status_Value(int num, TasksStatus status)
        {
            // Arrange // Act
            var tasks = _tasksStatusService.ListTasksByStatus(status);
            // Assert
            Assert.Equal(num, tasks.Count);
        }
    }
}
