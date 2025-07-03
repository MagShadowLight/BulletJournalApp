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
    public class ScheduleServiceTest
    {
        [Fact]
        public void When_Task_Change_Schedule_Then_It_Should_Succeed()
        {
            // Arrange
            var service1 = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var service2 = new ScheduleService(new Formatter(), new ConsoleLogger(), new FileLogger(), service1);
            var task1 = new Tasks(DateTime.Now, "Test 1", "Test", Schedule.Monthly);
            service1.AddTask(task1);
            // Act
            service2.ChangeSchedule("Test 1", Schedule.Daily);
            var task = service2.ListTasksBySchedule(Schedule.Daily);
            // Assert
            Assert.Contains(task1, task);
            Assert.Throws<Exception>(() => service2.ChangeSchedule("", Schedule.Daily));
        }
    }
}
