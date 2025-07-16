using BulletJournalApp.Core.Services;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Service
{
    public class TasksStatusServiceTest
    {
        [Fact]
        public void When_Tasks_Change_Status_Then_It_Should_Succeed()
        {
            // Arrange
            var service1 = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var service2 = new TasksStatusService(service1, new ConsoleLogger(), new FileLogger(), new Formatter());
            var task1 = new Tasks(DateTime.Now, "Test 1", "Test", Schedule.Monthly, false);
            service1.AddTask(task1);
            // Act
            service2.ChangeStatus("Test 1", TasksStatus.Overdue);
            var task = service2.ListTasksByStatus(TasksStatus.Overdue);
            // Assert
            Assert.Contains(task1, task);
            Assert.Throws<Exception>(() => service2.ChangeStatus("", TasksStatus.InProgress));
        }
    }
}
