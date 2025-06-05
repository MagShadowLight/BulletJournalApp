using BulletJournalApp.Core.Models;
using BulletJournalApp.Core.Models.Enum;
using BulletJournalApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Service
{
    public class TaskServiceTest
    {
        [Fact]
        public void When_There_Are_Tasks_Then_It_Should_Return_Tasks_List()
        {
            // Arrange
            var service = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var task1 = new Tasks(DateTime.Now, "Task Test 1", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Task Test 2", "mrow", Schedule.Monthly);
            task2.MarkComplete();
            // Act
            service.AddTask(task1);
            service.AddTask(task2);
            var allTasks = service.ListAllTasks();
            // Assert
            Assert.Equal(2, allTasks.Count);
            Assert.Contains(task1, allTasks);
            Assert.Contains(task2, allTasks);
        }
        [Fact]
        public void When_There_Are_Incomplete_Tasks_Then_It_Should_Return_Incomplete_Tasks()
        {
            // Arrange
            var service = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var task1 = new Tasks(DateTime.Now, "Task Test 1", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Task Test 2", "mrow", Schedule.Monthly);
            var task3 = new Tasks(DateTime.Now, "Task Test 3", "mrrp", Schedule.Monthly);
            // Act
            service.AddTask(task1);
            service.AddTask(task2);
            service.AddTask(task3);
            service.MarkTasksComplete(task2.Title);
            var incompleteTasks = service.ListIncompleteTasks();
            // Assert
            Assert.Equal(2, incompleteTasks.Count);
            Assert.Contains(task1, incompleteTasks);
            Assert.Contains(task3, incompleteTasks);
        }
        [Fact]
        public void When_There_Are_Empty_Tasks_Then_It_Should_Return_Nothing()
        {
            // Arrange
            var service = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            // Act
            var tasks = service.ListAllTasks();
            // Assert
            Assert.Empty(tasks);
        }
        [Fact]
        public void When_There_Are_Tasks_With_Default_Priority_Then_It_Should_Return_Tasks_List_By_Default_Priority()
        {
            // Arrange
            var service = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var task1 = new Tasks(DateTime.Now, "Task Test 1", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Task Test 2", "mrow", Schedule.Monthly);
            var task3 = new Tasks(DateTime.Now, "Task Test 3", "mrrp", Schedule.Monthly);
            var task4 = new Tasks(DateTime.Now, "Task Test 4", "mriaw", Schedule.Monthly);
            service.AddTask(task1);
            service.AddTask(task2);
            service.AddTask(task3);
            service.AddTask(task4);
            // Act
            service.ChangePriority(task2.Title, Priority.High);
            service.ChangePriority(task4.Title, Priority.Low);
            var mediumTasks = service.ListTasksByPriority(Priority.Medium);
            // Assert
            Assert.Equal(2, mediumTasks.Count);
            Assert.Contains(task1, mediumTasks);
            Assert.Contains(task3, mediumTasks);
        }
        [Fact]
        public void When_There_Are_Tasks_With_Category_Then_It_Should_Return_Tasks_List_By_Category()
        {
            // Arrange
            var service = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var task1 = new Tasks(DateTime.Now, "Task Test 1", "meow", Schedule.Monthly, Priority.Medium, Category.None);
            var task2 = new Tasks(DateTime.Now, "Task Test 2", "mrow", Schedule.Monthly, Priority.Medium, Category.None);
            var task3 = new Tasks(DateTime.Now, "Task Test 3", "mrrp", Schedule.Monthly, Priority.Medium, Category.None);
            service.AddTask(task1);
            service.AddTask(task2);
            service.AddTask(task3);
            // Act
            service.ChangeCategory(task1.Title, Category.Personal);
            service.ChangeCategory(task2.Title, Category.Home);
            service.ChangeCategory(task3.Title, Category.Personal);
            var personalTasks = service.ListTasksByCategory(Category.Personal);
            // Assert
            Assert.Equal(2, personalTasks.Count);
            Assert.Contains(task1, personalTasks);
            Assert.Contains(task3, personalTasks);
        }
        [Fact]
        public void When_There_Are_Tasks_In_Progress_Then_It_Should_Returns_Only_In_Progress_Tasks()
        {
            // Arrange
            var service = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var task1 = new Tasks(DateTime.Now, "Task Test 1", "meow", Schedule.Monthly, Priority.Medium, Category.Personal);
            var task2 = new Tasks(DateTime.Now, "Task Test 2", "mrow", Schedule.Monthly, Priority.Medium, Category.Home);
            var task3 = new Tasks(DateTime.Now, "Task Test 3", "mrrp", Schedule.Monthly, Priority.Medium, Category.Personal);
            service.AddTask(task1);
            service.AddTask(task2);
            service.AddTask(task3);
            // Act
            service.ChangeStatus(task1.Title, TasksStatus.InProgress);
            service.ChangeStatus(task3.Title, TasksStatus.InProgress);
            var WIPTasks = service.ListTasksByStatus(TasksStatus.InProgress);
            // Assert
            Assert.Equal(2, WIPTasks.Count);
            Assert.Contains(task1, WIPTasks);
            Assert.Contains(task3, WIPTasks);
        }
        [Fact]
        public void When_There_Are_Tasks_For_The_Week_Then_It_Should_Return_Only_Tasks_For_The_Week()
        {
            // Arrange
            var service = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var task1 = new Tasks(DateTime.Now, "Task Test 1", "meow", Schedule.Monthly, Priority.Medium, Category.Personal);
            var task2 = new Tasks(DateTime.Now, "Task Test 2", "mrow", Schedule.Monthly, Priority.Medium, Category.Home);
            var task3 = new Tasks(DateTime.Now, "Task Test 3", "mrrp", Schedule.Monthly, Priority.Medium, Category.Personal);
            var task4 = new Tasks(DateTime.Now, "Task Test 4", "mriaw", Schedule.Monthly, Priority.Medium, Category.Personal);
            service.AddTask(task1);
            service.AddTask(task2);
            service.AddTask(task3);
            service.AddTask(task4);
            // Act
            service.ChangeSchedule(task1.Title, Schedule.Weekly);
            service.ChangeSchedule(task4.Title, Schedule.Weekly);
            var weeklyTasks = service.ListTasksBySchedule(Schedule.Weekly);
            // Assert
            Assert.Equal(2, weeklyTasks.Count);
            Assert.Contains(task1, weeklyTasks);
            Assert.Contains(task4, weeklyTasks);
        }
        [Fact]
        public void When_There_Are_Tasks_With_Specific_Title_Then_It_Should_Return_Only_Task()
        {
            // Assert
            var service = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var task1 = new Tasks(DateTime.Now, "Task Test 1", "meow", Schedule.Weekly, Priority.Medium, Category.Personal);
            var task2 = new Tasks(DateTime.Now, "Task Test 2", "mrow", Schedule.Monthly, Priority.Medium, Category.Home);
            var task3 = new Tasks(DateTime.Now, "Task Test 3", "mrrp", Schedule.Monthly, Priority.Medium, Category.Personal);
            var task4 = new Tasks(DateTime.Now, "Task Test 4", "mriaw", Schedule.Weekly, Priority.Medium, Category.Personal);
            service.AddTask(task1);
            service.AddTask(task2);
            service.AddTask(task3);
            service.AddTask(task4);
            // Act
            var task = service.FindTasksByTitle("Task Test 2");
            // Assert
            Assert.Contains(task2.Title, task.Title);
        }
    }
}
