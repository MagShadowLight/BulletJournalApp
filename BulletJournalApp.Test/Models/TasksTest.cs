using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Models
{
    public class TasksTest
    {
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Assign_Id()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly, Priority.Low, Category.None, "");
            // Act // Assert
            Assert.Equal(0, task.Id);
        }
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Have_Title()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly, Priority.Low, Category.None, "");
            // Act // Assert
            Assert.Equal("Task", task.Title);
        }
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Have_Description()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly, Priority.Low, Category.None, "");
            // Act // Assert
            Assert.Equal("Do something", task.Description);
        }
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Have_Default_Priority()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly);
            // Act // Assert
            Assert.Equal(Priority.Medium, task.Priority);
        }
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Have_DueDate()
        {
            // Arrange
            var task = new Tasks(DateTime.Today, "Task", "Do something", Schedule.Monthly, Priority.Low, Category.None, "");
            // Act // Assert
            Assert.Equal(DateTime.Today, task.DueDate);
        }
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Assign_IsCompleted_To_False()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly, Priority.Low, Category.None, "");
            // Act // Assert
            Assert.False(task.IsCompleted);
        }
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Have_Empty_Note()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly, Priority.Low, Category.None, "");
            // Act // Assert
            Assert.Empty(task.Notes);
        }
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Have_Default_Category()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly, Priority.Low);
            // Act // Assert
            Assert.Equal(Category.None, task.Category);
        }
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Have_Default_Status()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly, Priority.Low);
            // Act // Assert
            Assert.Equal(TasksStatus.ToDo, task.Status);
        }
        [Fact]
        public void When_Task_Have_Blank_Title_Or_Description_When_Updated_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly);
            // Act // Assert
            Assert.Throws<ArgumentException>(() => task.Update(DateTime.Now, "", "do something", ""));
            Assert.Throws<ArgumentException>(() => task.Update(DateTime.Now, "Task", "", ""));
        }
        [Fact]
        public void When_Task_was_Created_Then_It_Should_Have_Schedule()
        {
            // Arrange
            var task = new Tasks(null, "Task", "Do something", Schedule.Monthly);
            // Act // Assert
            Assert.Equal(Schedule.Monthly, task.Schedule);
        }
    }
}
