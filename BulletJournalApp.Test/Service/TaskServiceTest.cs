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
    public class TaskServiceTest
    {
        private TaskService _taskService = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
        private ItemService _itemService = new ItemService(new ConsoleLogger(), new FileLogger());
        [Fact]
        public void When_There_Are_Tasks_Then_It_Should_Return_Tasks_List()
        {
            // Arrange
            var service = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var task1 = new Tasks(DateTime.Now, "Task Test 1", "meow", Schedule.Monthly, false);
            var task2 = new Tasks(DateTime.Now, "Task Test 2", "mrow", Schedule.Monthly, false);
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
            var task1 = new Tasks(DateTime.Now, "Task Test 1", "meow", Schedule.Monthly, false);
            var task2 = new Tasks(DateTime.Now, "Task Test 2", "mrow", Schedule.Monthly, false);
            var task3 = new Tasks(DateTime.Now, "Task Test 3", "mrrp", Schedule.Monthly, false);
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
            var taskservice = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var priorityservice = new PriorityService(taskservice, new ConsoleLogger(), new FileLogger(), new Formatter());
            var task1 = new Tasks(DateTime.Now, "Task Test 1", "meow", Schedule.Monthly, false);
            var task2 = new Tasks(DateTime.Now, "Task Test 2", "mrow", Schedule.Monthly, false);
            var task3 = new Tasks(DateTime.Now, "Task Test 3", "mrrp", Schedule.Monthly, false);
            var task4 = new Tasks(DateTime.Now, "Task Test 4", "mriaw", Schedule.Monthly, false);
            taskservice.AddTask(task1);
            taskservice.AddTask(task2);
            taskservice.AddTask(task3);
            taskservice.AddTask(task4);
            // Act
            priorityservice.ChangePriority(task2.Title, Priority.High);
            priorityservice.ChangePriority(task4.Title, Priority.Low);
            var mediumTasks = priorityservice.ListTasksByPriority(Priority.Medium);
            // Assert
            Assert.Equal(2, mediumTasks.Count);
            Assert.Contains(task1, mediumTasks);
            Assert.Contains(task3, mediumTasks);
        }
        [Fact]
        public void When_There_Are_Tasks_With_Category_Then_It_Should_Return_Tasks_List_By_Category()
        {
            // Arrange
            var categoryservice = new CategoryService(new ConsoleLogger(), new FileLogger(), new Formatter(), _taskService, _itemService);
            var task1 = new Tasks(DateTime.Now, "Task Test 1", "meow", Schedule.Monthly, false, 7, new DateTime(), Priority.Medium, Category.None);
            var task2 = new Tasks(DateTime.Now, "Task Test 2", "mrow", Schedule.Monthly, false, 7, new DateTime(), Priority.Medium, Category.None);
            var task3 = new Tasks(DateTime.Now, "Task Test 3", "mrrp", Schedule.Monthly, false, 7, new DateTime(), Priority.Medium, Category.None);
            _taskService.AddTask(task1);
            _taskService.AddTask(task2);
            _taskService.AddTask(task3);
            // Act
            categoryservice.ChangeCategory(task1.Title, Entries.TASKS, Category.Personal);
            categoryservice.ChangeCategory(task2.Title, Entries.TASKS, Category.Home);
            categoryservice.ChangeCategory(task3.Title, Entries.TASKS, Category.Personal);
            var personalTasks = categoryservice.ListTasksByCategory(Category.Personal);
            // Assert
            Assert.Equal(2, personalTasks.Count);
            Assert.Contains(task1, personalTasks);
            Assert.Contains(task3, personalTasks);
        }
        [Fact]
        public void When_There_Are_Tasks_In_Progress_Then_It_Should_Returns_Only_In_Progress_Tasks()
        {
            // Arrange
            var taskservice = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var statusservice = new TasksStatusService(taskservice, new ConsoleLogger(), new FileLogger(), new Formatter());
            var task1 = new Tasks(DateTime.Now, "Task Test 1", "meow", Schedule.Monthly, false, 7, new DateTime(), Priority.Medium, Category.Personal);
            var task2 = new Tasks(DateTime.Now, "Task Test 2", "mrow", Schedule.Monthly, false, 7, new DateTime(), Priority.Medium, Category.Home);
            var task3 = new Tasks(DateTime.Now, "Task Test 3", "mrrp", Schedule.Monthly, false, 7, new DateTime(), Priority.Medium, Category.Personal);
            taskservice.AddTask(task1);
            taskservice.AddTask(task2);
            taskservice.AddTask(task3);
            // Act
            statusservice.ChangeStatus(task1.Title, TasksStatus.InProgress);
            statusservice.ChangeStatus(task3.Title, TasksStatus.InProgress);
            var WIPTasks = statusservice.ListTasksByStatus(TasksStatus.InProgress);
            // Assert
            Assert.Equal(2, WIPTasks.Count);
            Assert.Contains(task1, WIPTasks);
            Assert.Contains(task3, WIPTasks);
        }
        [Fact]
        public void When_There_Are_Tasks_For_The_Week_Then_It_Should_Return_Only_Tasks_For_The_Week()
        {
            // Arrange
            var scheduleservice = new ScheduleService(new Formatter(), new ConsoleLogger(), new FileLogger(), _taskService, _itemService);
            var task1 = new Tasks(DateTime.Now, "Task Test 1", "meow", Schedule.Monthly, false, 7, new DateTime(), Priority.Medium, Category.Personal);
            var task2 = new Tasks(DateTime.Now, "Task Test 2", "mrow", Schedule.Monthly, false, 7, new DateTime(), Priority.Medium, Category.Home);
            var task3 = new Tasks(DateTime.Now, "Task Test 3", "mrrp", Schedule.Monthly, false, 7, new DateTime(), Priority.Medium, Category.Personal);
            var task4 = new Tasks(DateTime.Now, "Task Test 4", "mriaw", Schedule.Monthly, false, 7, new DateTime(), Priority.Medium, Category.Personal);
            _taskService.AddTask(task1);
            _taskService.AddTask(task2);
            _taskService.AddTask(task3);
            _taskService.AddTask(task4);
            // Act
            scheduleservice.ChangeSchedule(task1.Title, Entries.TASKS, Schedule.Weekly);
            scheduleservice.ChangeSchedule(task4.Title, Entries.TASKS, Schedule.Weekly);
            var weeklyTasks = scheduleservice.ListTasksBySchedule(Schedule.Weekly);
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
            var task1 = new Tasks(DateTime.Now, "Task Test 1", "meow", Schedule.Weekly, false, 7, new DateTime(), Priority.Medium, Category.Personal);
            var task2 = new Tasks(DateTime.Now, "Task Test 2", "mrow", Schedule.Monthly, false, 7, new DateTime(), Priority.Medium, Category.Home);
            var task3 = new Tasks(DateTime.Now, "Task Test 3", "mrrp", Schedule.Monthly, false, 7, new DateTime(), Priority.Medium, Category.Personal);
            var task4 = new Tasks(DateTime.Now, "Task Test 4", "mriaw", Schedule.Weekly, false, 7, new DateTime(), Priority.Medium, Category.Personal);
            service.AddTask(task1);
            service.AddTask(task2);
            service.AddTask(task3);
            service.AddTask(task4);
            // Act
            var task = service.FindTasksByTitle("Task Test 2");
            // Assert
            Assert.Contains(task2.Title, task.Title);
        }

        [Fact]
        public void When_Creating_Tasks_Then_The_Tasks_Should_Be_Added()
        {
            // Arrange
            var service = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var task1 = new Tasks(DateTime.Now, "Test", "Test", Schedule.Monthly, false);
            // Act
            service.AddTask(task1);
            var task = service.FindTasksByTitle("Test");
            // Assert
            Assert.Contains(task1.Title, task.Title);
            Assert.Throws<ArgumentNullException>(() => service.AddTask(null));
        }
        [Fact]
        public void When_Tasks_Marked_As_Complete_Then_It_Should_Succeed()
        {
            // Arrange
            var service = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var task1 = new Tasks(DateTime.Now, "Test 1", "Test", Schedule.Monthly, false);
            var task2 = new Tasks(DateTime.Now, "Test 2", "Test", Schedule.Daily, false);
            var task3 = new Tasks(DateTime.Now, "Test 3", "Test", Schedule.Weekly, false);
            var repeatingTask = new Tasks(DateTime.Today, "Repeating Test Task", "Test", Schedule.Monthly, true, 7, new DateTime(), Priority.Medium, Category.None, "", TasksStatus.ToDo, 0, false);
            var repeatingTaskWithEnd = new Tasks(DateTime.Today, "RepeatingTestTaskWithEnd", "Test", Schedule.Monthly, true, 7, DateTime.Today.AddDays(28), Priority.Medium, Category.None, "", TasksStatus.ToDo, 0, false);
            service.AddTask(task1);
            service.AddTask(task2);
            service.AddTask(task3);
            service.AddTask(repeatingTask);
            service.AddTask(repeatingTaskWithEnd);
            // Act
            service.MarkTasksComplete("Test 2");
            service.MarkTasksComplete("Repeating Test Task");
            service.MarkTasksComplete("RepeatingTestTaskWithEnd");
            service.MarkTasksComplete("RepeatingTestTaskWithEnd");
            service.MarkTasksComplete("RepeatingTestTaskWithEnd");
            service.MarkTasksComplete("RepeatingTestTaskWithEnd");
            service.MarkTasksComplete("RepeatingTestTaskWithEnd");
            var allTasks = service.ListAllTasks();
            var incompleteTasks = service.ListIncompleteTasks();
            // Assert
            Assert.True(task2.IsCompleted);
            Assert.Equal(5, allTasks.Count);
            Assert.Equal(3, incompleteTasks.Count);
            Assert.Equal(DateTime.Today.AddDays(7), repeatingTask.DueDate);
            Assert.False(repeatingTask.IsCompleted);
            Assert.Equal(repeatingTaskWithEnd.EndRepeatDate, repeatingTaskWithEnd.DueDate);
            Assert.True(repeatingTaskWithEnd.IsCompleted);
            Assert.False(repeatingTaskWithEnd.IsRepeatable);
            Assert.Throws<Exception>(() => service.MarkTasksComplete("Made Up Test"));
        }
        [Fact]
        public void When_Tasks_Were_Updated_Then_It_Should_Succeed()
        {
            // Arrange
            var service = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var task1 = new Tasks(DateTime.Now, "Test 1", "Test", Schedule.Monthly, false);
            var task2 = new Tasks(DateTime.Now, "Test 2", "Test", Schedule.Daily, false);
            var task3 = new Tasks(DateTime.Now, "Test 3", "Test", Schedule.Weekly, false);
            service.AddTask(task1);
            service.AddTask(task2);
            service.AddTask(task3);
            // Act
            service.UpdateTask("Test 2", "Updated Test", "Test with Updated Date", "", false, DateTime.Now);
            var tasks = service.ListAllTasks();
            var updatedTask = service.FindTasksByTitle("Updated Test");
            // Assert
            Assert.Contains("Updated Test", updatedTask.Title);
            Assert.Equal(3, tasks.Count);
            Assert.Throws<Exception>(() => service.UpdateTask("Fake Test", "Test", "Test", "", false, DateTime.Now));
        }
        [Fact]
        public void When_Tasks_Were_Deleted_Then_It_Should_Succeed()
        {
            // Arrange
            var service = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var task1 = new Tasks(DateTime.Now, "Test 1", "Test", Schedule.Monthly, false);
            var task2 = new Tasks(DateTime.Now, "Test 2", "Test", Schedule.Daily, false);
            var task3 = new Tasks(DateTime.Now, "Test 3", "Test", Schedule.Weekly, false);
            service.AddTask(task1);
            service.AddTask(task2);
            service.AddTask(task3);
            // Act
            service.DeleteTask("Test 3");
            var tasks = service.ListAllTasks();
            // Assert
            Assert.Equal(2, tasks.Count());
            Assert.Contains(task1, tasks);
            Assert.Contains(task2, tasks);
            Assert.DoesNotContain(task3, tasks);
            Assert.Throws<Exception>(() => service.DeleteTask("Fake Test"));
        }
    }
}
