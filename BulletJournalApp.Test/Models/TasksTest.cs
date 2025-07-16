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
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly, false, 7, new DateTime(), Priority.Low, Category.None, "");
            // Act // Assert
            Assert.Equal(0, task.Id);
        }
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Have_Title()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly, false, 7, new DateTime(), Priority.Low, Category.None, "");
            // Act // Assert
            Assert.Equal("Task", task.Title);
        }
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Have_Description()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly, false, 7, new DateTime(), Priority.Low, Category.None, "");
            // Act // Assert
            Assert.Equal("Do something", task.Description);
        }
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Have_Default_Priority()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly, false);
            // Act // Assert
            Assert.Equal(Priority.Medium, task.Priority);
        }
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Have_DueDate()
        {
            // Arrange
            var task = new Tasks(DateTime.Today, "Task", "Do something", Schedule.Monthly, false, 7, new DateTime(), Priority.Low, Category.None, "");
            // Act // Assert
            Assert.Equal(DateTime.Today, task.DueDate);
        }
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Assign_IsCompleted_To_False()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly, false, 7, new DateTime(), Priority.Low, Category.None, "");
            // Act // Assert
            Assert.False(task.IsCompleted);
        }
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Have_Empty_Note()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly, false, 7, new DateTime(), Priority.Low, Category.None, "");
            // Act // Assert
            Assert.Empty(task.Notes);
        }
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Have_Default_Category()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly, false, 7, new DateTime(), Priority.Low);
            // Act // Assert
            Assert.Equal(Category.None, task.Category);
        }
        [Fact]
        public void When_Task_Was_Created_Then_It_Should_Have_Default_Status()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly, false, 7, new DateTime(), Priority.Low);
            // Act // Assert
            Assert.Equal(TasksStatus.ToDo, task.Status);
        }
        [Fact]
        public void When_Task_Have_Blank_Title_Or_Description_When_Updated_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Task", "Do something", Schedule.Monthly, false);
            // Act // Assert
            Assert.Throws<ArgumentException>(() => task.Update(DateTime.Now, "", "do something", false, ""));
            Assert.Throws<ArgumentException>(() => task.Update(DateTime.Now, "Task", "", false, ""));
        }
        [Fact]
        public void When_Task_was_Created_Then_It_Should_Have_Schedule()
        {
            // Arrange
            var task = new Tasks(null, "Task", "Do something", Schedule.Monthly, false);
            // Act // Assert
            Assert.Equal(Schedule.Monthly, task.schedule);
        }
        [Fact]
        public void When_Task_Were_Created_With_Repeatable_Then_Tasks_Should_Have_IsRepeatable_Set_To_True()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Test", "Test", Schedule.Monthly, true);
            // Act // Assert
            Assert.True(task.IsRepeatable);
        }
        [Fact]
        public void When_Task_Were_Created_With_No_Repeat_Then_Tasks_Should_Have_IsRepeatable_As_False()
        {
            // Arrange
            var task = new Tasks(DateTime.Now, "Test", "Test", Schedule.Monthly, false);
            // Act // Arrange
            Assert.False(task.IsRepeatable);
        }
        [Fact]
        public void When_Tasks_Is_Repeatable_Then_New_Due_Date_Should_Be_Set()
        {
            // Arrange
            var oldTasks = new Tasks(DateTime.Today, "Test", "Test", Schedule.Monthly, true);
            var newTasks = new Tasks(oldTasks.DueDate, oldTasks.Title, oldTasks.Description, oldTasks.schedule, oldTasks.IsRepeatable);
            // Act
            newTasks.RepeatTask();
            // Assert
            Assert.Equal(DateTime.Today, oldTasks.DueDate);
            Assert.Equal(DateTime.Today.AddDays(7), newTasks.DueDate);
        }
        [Fact]
        public void When_Tasks_Is_Repeatable_With_30_Days_Each_Then_New_Due_Date_Should_Be_Set_30_Days_After_Old_Due_Date()
        {
            // Arrange
            var oldTasks = new Tasks(DateTime.Today, "Test", "Test", Schedule.Monthly, true, 30);
            var newTasks = new Tasks(oldTasks.DueDate, oldTasks.Title, oldTasks.Description, oldTasks.schedule, oldTasks.IsRepeatable, oldTasks.RepeatDays);
            // Act
            newTasks.RepeatTask();
            // Assert
            Assert.Equal(DateTime.Today, oldTasks.DueDate);
            Assert.Equal(DateTime.Today.AddDays(30), newTasks.DueDate);
        }
        [Fact]
        public void When_Tasks_Were_Added_With_End_Repeat_Date_Then_Tasks_Should_Have_End_Repeat_Date()
        {
            // Arrange
            var task = new Tasks(DateTime.Today, "Test", "Test", Schedule.Monthly, true, 7, DateTime.Parse("July 31 2025"), Priority.Medium, Category.None, "", TasksStatus.ToDo, 0, false);
            // Act // Assert
            Assert.Equal(DateTime.Parse("July 31, 2025"), task.EndRepeatDate);
        }
    }
}
