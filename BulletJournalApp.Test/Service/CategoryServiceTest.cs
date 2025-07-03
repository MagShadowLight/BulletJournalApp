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
    public class CategoryServiceTest
    {
        [Fact]
        public void When_User_Try_To_Change_Category_With_Empty_Tasks_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var service = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var service2 = new CategoryService(service, new ConsoleLogger(), new FileLogger(), new Formatter());
            var task = new Tasks(DateTime.Now, "Test", "Test", Schedule.Monthly);
            service.AddTask(task);
            // Act // Assert
            Assert.Throws<Exception>(() => service2.ChangeCategory("", Category.Home));
        }
    }
}
