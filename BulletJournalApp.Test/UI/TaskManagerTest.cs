using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Models;
using BulletJournalApp.Core.Models.Enum;
using BulletJournalApp.UI;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.UI
{
    public class TaskManagerTest
    {
        private Mock<IConsoleLogger> consoleLoggerMock = new();
        private Mock<IFileLogger> fileLoggerMock = new();
        private Mock<IFormatter> formatterMock = new();
        [Fact]
        public void When_Tasks_Were_Added_Then_It_Should_Succeed()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(mockService.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object);
            using var input = new StringReader("1\nTest Task\nmeow\nJun 10, 2025\nL\nN\nM\n\n0\nn");
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            mockService.Verify(user => user.AddTask(It.Is<Tasks>(task => task.Title == "Test Task" && task.Description == "meow")), Times.Once());
        }
        //[Fact]
        //public void When_User_Select_List_All_Tasks_Then_It_Should_Returns_List_Of_Tasks()
        //{
        //    // Arrange
        //    var mockService = new Mock<ITaskService>();
        //    var tasks = new List<Tasks>();
        //    var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
        //    tasks.Add(task1);
        //    var taskManager = new TaskManager(mockService.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object);
        //    using var input = new StringReader("2\n0\nn");
        //    Console.SetIn(input);
        //    // Act
        //    taskManager.TaskManagerUI();
        //    // Assert
        //    mockService.Verify(user => user.ListAllTasks(), Times.Once());
        //}
        
    }
}
