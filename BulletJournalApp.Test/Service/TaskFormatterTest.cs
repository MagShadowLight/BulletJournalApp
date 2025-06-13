using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Service
{
    public class TaskFormatterTest
    {
        [Fact]
        public void When_Task_Has_Title_Then_Formatter_Should_Display_Title_To_User()
        {
            // Arrange
            var service = new Formatter();
            var tasks = new Tasks(null, "Test Task", "meow", Schedule.Monthly);
            // Act
            var result = service.Format(tasks);
            // Assert
            Assert.Contains(tasks.Title, result);
        }
        [Fact]
        public void When_Task_Has_Description_Then_Formatter_Should_Display_Description_To_User()
        {
            // Arrange
            var service = new Formatter();
            var tasks = new Tasks(null, "Test Task", "meow", Schedule.Monthly);
            // Act
            var result = service.Format(tasks);
            // Assert
            Assert.Contains(tasks.Description, result);
        }
        [Fact]
        public void When_Task_Has_Priority_Then_Formatter_Should_Display_Priority_To_User()
        {
            // Arrange
            var service = new Formatter();
            var task = new Tasks(null, "Test Task", "meow", Schedule.Monthly, Priority.Medium);
            // Act
            var result = service.Format(task);
            // Assert
            Assert.Contains(task.Priority.ToString(), result);
        }
        [Fact]
        public void When_Task_Has_DueDate_Then_Formatter_Should_Display_DueDate_To_User()
        {
            // Arrange
            var service = new Formatter();
            var task = new Tasks(DateTime.Parse("6-10-2025"), "Test Task", "meow", Schedule.Monthly);
            // Act
            var result = service.Format(task);
            // Assert
            Assert.Contains(task.DueDate.ToString(), result);
        }
        [Fact]
        public void When_Task_Has_Default_Status_Then_Formatter_Should_Display_Status_To_User()
        {
            // Arrange
            var service = new Formatter();
            var task = new Tasks(null, "Test Task", "meow", Schedule.Monthly);
            // Act
            var result = service.Format(task);
            // Assert
            Assert.Contains(task.Status.ToString(), result);
        }
        [Fact]
        public void When_Task_Has_No_Category_Then_Formatter_Should_Display_Default_Category_To_User()
        {
            // Arrange
            var service = new Formatter();
            var task = new Tasks(null, "Test Task", "meow", Schedule.Monthly, Priority.Low);
            // Act
            var result = service.Format(task);
            // Assert
            Assert.Contains(task.Category.ToString(), result);
        }
        [Fact]
        public void When_Task_Has_Note_Then_Formatter_Should_Display_Note_To_User()
        {
            // Arrange
            var service = new Formatter();
            var task = new Tasks(null, "Test Task", "meow", Schedule.Monthly, Priority.Low, Category.None, "Test Note");
            // Act
            var result = service.Format(task);
            // Assert
            Assert.Contains(task.Notes, result);
        }
        [Fact]
        public void When_Task_Are_Incomplete_Then_Formatter_Should_Display_Incomplete_To_User()
        {
            // Assert
            var service = new Formatter();
            var task = new Tasks(null, "Test Task", "meow", Schedule.Monthly);
            // Act
            var message = task.IsCompleted ? "" : "Incomplete";
            var result = service.Format(task);
            // Assert
            Assert.Contains(message, result);
        }
        [Fact]
        public void When_Task_Are_Completed_Then_Formatter_Should_Display_String_Complete_To_User()
        {
            // Assert
            var service = new Formatter();
            var task = new Tasks(null, "Test Task", "meow", Schedule.Monthly);
            task.MarkComplete();
            // Act
            var message = task.IsCompleted ? "Completed" : "";
            var result = service.Format(task);
            // Assert
            Assert.Contains(message, result);
        }
    }
}
