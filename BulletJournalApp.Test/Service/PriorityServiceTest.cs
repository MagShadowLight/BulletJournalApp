using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Service
{
    public class PriorityServiceTest
    {
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
        }
    }
}
