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
        private Tasks task1;
        private Tasks task2;
        private Tasks task3;
        private Tasks task4;
        private Tasks task5;

        public PriorityServiceTest()
        {
            _taskService = new TaskService(_formatter, _consolelogger, _filelogger);
            _priorityService = new PriorityService(_taskService, _consolelogger, _filelogger, _formatter);
            task1 = new Tasks(DateTime.Today, "Test 1", "Test", Schedule.Monthly, false);
            task2 = new Tasks(DateTime.Today, "Test 2", "Test", Schedule.Monthly, false);
            task3 = new Tasks(DateTime.Today, "Test 3", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.High);
            task4 = new Tasks(DateTime.Today, "Test 4", "Test", Schedule.Monthly, false);
            task5 = new Tasks(DateTime.Today, "Test 5", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Low);
        }

        [Theory]
        [MemberData(nameof(PriorityServiceData.GetStringAndPriorityValue), MemberType = typeof(PriorityServiceData))]
        public void Given_There_Are_Tasks_In_The_List_When_Changing_The_Priority_Then_Task_Should_Updated_With_New_Priority(string title, Priority priority)
        {
            // Arrange
            int num = 5;
            _taskService.AddTask(task1);
            _taskService.AddTask(task2);
            _taskService.AddTask(task3);
            _taskService.AddTask(task4);
            _taskService.AddTask(task5);
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
            // Arrange
            _taskService.AddTask(task1);
            _taskService.AddTask(task2);
            _taskService.AddTask(task3);
            _taskService.AddTask(task4);
            _taskService.AddTask(task5);
            // Act
            var tasks = _priorityService.ListTasksByPriority(priority);
            // Assert
            Assert.Equal(num, tasks.Count);
        }


        /*
        [Fact]
        public void When_Tasks_Change_Priority_Then_It_Should_Succeed()
        {
            // Arrange
            var service1 = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var service2 = new PriorityService(service1, new ConsoleLogger(), new FileLogger(), new Formatter());
            var task1 = new Tasks(DateTime.Now, "Test 1", "Test", Schedule.Monthly, false);
            service1.AddTask(task1);
            // Act
            service2.ChangePriority("Test 1", Priority.Low);
            var task = service2.ListTasksByPriority(Priority.Low);
            // Assert
            Assert.Contains(task1, task);
            Assert.Throws<Exception>(() => service2.ChangePriority("", Priority.High));
        }*/
    }
}
