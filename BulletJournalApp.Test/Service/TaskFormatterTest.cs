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
            var tasks = new Tasks(DateTime.MinValue, "Test Task", "meow", Schedule.Monthly, false);
            // Act
            var result = service.FormatTasks(tasks);
            // Assert
            Assert.Contains(tasks.Title, result);
        }
        [Fact]
        public void When_Task_Has_Description_Then_Formatter_Should_Display_Description_To_User()
        {
            // Arrange
            var service = new Formatter();
            var tasks = new Tasks(DateTime.MinValue, "Test Task", "meow", Schedule.Monthly, false);
            // Act
            var result = service.FormatTasks(tasks);
            // Assert
            Assert.Contains(tasks.Description, result);
        }
        [Fact]
        public void When_Task_Has_Priority_Then_Formatter_Should_Display_Priority_To_User()
        {
            // Arrange
            var service = new Formatter();
            var task = new Tasks(DateTime.MinValue, "Test Task", "meow", Schedule.Monthly, false, 7, new DateTime(), Priority.Medium);
            // Act
            var result = service.FormatTasks(task);
            // Assert
            Assert.Contains(task.Priority.ToString(), result);
        }
        [Fact]
        public void When_Task_Has_DueDate_Then_Formatter_Should_Display_DueDate_To_User()
        {
            // Arrange
            var service = new Formatter();
            var task = new Tasks(DateTime.Parse("6-10-2025"), "Test Task", "meow", Schedule.Monthly, false);
            // Act
            var result = service.FormatTasks(task);
            // Assert
            Assert.Contains(task.DueDate.ToString(), result);
        }
        [Fact]
        public void When_Task_Has_Default_Status_Then_Formatter_Should_Display_Status_To_User()
        {
            // Arrange
            var service = new Formatter();
            var task = new Tasks(DateTime.MinValue, "Test Task", "meow", Schedule.Monthly, false);
            // Act
            var result = service.FormatTasks(task);
            // Assert
            Assert.Contains(task.Status.ToString(), result);
        }
        [Fact]
        public void When_Task_Has_No_Category_Then_Formatter_Should_Display_Default_Category_To_User()
        {
            // Arrange
            var service = new Formatter();
            var task = new Tasks(DateTime.MinValue, "Test Task", "meow", Schedule.Monthly, false, 7, new DateTime(), Priority.Low);
            // Act
            var result = service.FormatTasks(task);
            // Assert
            Assert.Contains(task.Category.ToString(), result);
        }
        [Fact]
        public void When_Task_Has_Note_Then_Formatter_Should_Display_Note_To_User()
        {
            // Arrange
            var service = new Formatter();
            var task = new Tasks(DateTime.MinValue, "Test Task", "meow", Schedule.Monthly, false, 7, new DateTime(), Priority.Low, Category.None, "Test Note");
            // Act
            var result = service.FormatTasks(task);
            // Assert
            Assert.Contains(task.Notes, result);
        }
        [Fact]
        public void When_Task_Are_Incomplete_Then_Formatter_Should_Display_Incomplete_To_User()
        {
            // Assert
            var service = new Formatter();
            var task = new Tasks(DateTime.MinValue, "Test Task", "meow", Schedule.Monthly, false);
            // Act
            var message = task.IsCompleted ? "" : "Incomplete";
            var result = service.FormatTasks(task);
            // Assert
            Assert.Contains(message, result);
        }
        [Fact]
        public void When_Task_Are_Completed_Then_Formatter_Should_Display_String_Complete_To_User()
        {
            // Assert
            var service = new Formatter();
            var task = new Tasks(DateTime.MinValue, "Test Task", "meow", Schedule.Monthly, false);
            task.MarkComplete();
            // Act
            var message = task.IsCompleted ? "Completed" : "";
            var result = service.FormatTasks(task);
            // Assert
            Assert.Contains(message, result);
        }
        [Fact]
        public void When_Tasks_Are_Repeated_Then_Formatter_Should_Display_That_Task_Is_Repeating()
        {
            // Arrange
            var service = new Formatter();
            var task = new Tasks(DateTime.Parse("Jul 11, 2025"), "Test", "Test", Schedule.Monthly, true, 7, DateTime.MinValue);
            // Act
            var message = task.IsRepeatable ? "Repeating Task" : "";
            var result = service.FormatTasks(task);
            // Assert
            Assert.Contains(message, result);
        }
        [Fact]
        public void When_Tasks_Are_Not_Repeated_Then_Formatter_Should_Display_Empty_Line()
        {
            // Arrange
            var service = new Formatter();
            var task = new Tasks(DateTime.Parse("Jul 11, 2025"), "Test", "Test", Schedule.Monthly, false);
            // Act
            var message = task.IsRepeatable ? "Repeat" : "Repeat: N/A";
            var result = service.FormatTasks(task);
            // Assert
            Assert.Contains(message, result);
        }
        [Fact]
        public void When_Tasks_Are_Repeating_With_End_Date_Then_Formatter_Should_Display_That_It_Was_Repeating_Until_That_Date()
        {
            // Arrange
            var service = new Formatter();
            var task = new Tasks(DateTime.Parse("Jul 11, 2025"), "Test", "Test", Schedule.Monthly, true, 7, DateTime.Parse("Aug 8, 2025"));
            // Act
            var message = task.IsRepeatable ? "Repeating until" : "";
            var result = service.FormatTasks(task);
            // Assert
            Assert.Contains(message, result);
        }
    }
}
