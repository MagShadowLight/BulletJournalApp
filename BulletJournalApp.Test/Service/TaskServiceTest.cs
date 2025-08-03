using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletJournalApp.Test.Data.Services;

namespace BulletJournalApp.Test.Service
{
    public class TaskServiceTest
    {
        private TaskService _taskService;
        private ItemService _itemService;
        private Formatter _formatter = new Formatter();
        private ConsoleLogger _conlogger = new ConsoleLogger();
        private FileLogger _filelogger = new FileLogger();
        private Tasks task1;
        private Tasks task2;
        private Tasks task3;
        private Tasks repeatingtask;
        private Tasks repeatingtaskwithend;

        public TaskServiceTest()
        {
            _taskService = new TaskService(_formatter, _conlogger, _filelogger);
            _itemService = new ItemService(_conlogger, _filelogger);
            task1 = new Tasks(DateTime.Today.AddDays(1), "Test 1", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.ToDo, 1, false);
            task2 = new Tasks(DateTime.Today.AddDays(1), "Test 2", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.ToDo, 2, false);
            task3 = new Tasks(DateTime.Today.AddDays(1), "Test 3", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.ToDo, 3, false);
            repeatingtask = new Tasks(DateTime.Today.AddDays(1), "Test 4", "Test", Schedule.Monthly, true, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.ToDo, 3, false);
            repeatingtaskwithend = new Tasks(DateTime.Today.AddDays(1), "Test 5", "Test", Schedule.Monthly, true, 7, DateTime.Today.AddDays(29), Priority.Medium, Category.None, "", TasksStatus.ToDo, 3, false);
        }


        [Fact]
        public void Given_There_Are_No_Tasks_In_List_When_Adding_A_Task_Then_It_Should_Be_Added_To_List()
        {
            // Arrange 
            int num = 3;
            // Act
            _taskService.AddTask(task1);
            _taskService.AddTask(task2);
            _taskService.AddTask(task3);
            // Assert
            Assert.Equal(num, _taskService.ListAllTasks().Count);
            Assert.Contains(task1, _taskService.ListAllTasks());
            Assert.Contains(task2, _taskService.ListAllTasks());
            Assert.Contains(task3, _taskService.ListAllTasks());
            Assert.Throws<ArgumentNullException>(() => _taskService.AddTask(null));
        }
        [Fact]
        public void Given_There_Are_Tasks_In_The_List_When_Listing_All_Tasks_Then_It_Should_Return_All_Tasks()
        {
            // Arrange
            int num = 3;
            _taskService.AddTask(task1);
            _taskService.AddTask(task2);
            _taskService.AddTask(task3);
            // Act
            var tasks = _taskService.ListAllTasks();
            // Assert
            Assert.Equal(3, tasks.Count);
            Assert.Contains(task1, tasks);
            Assert.Contains(task2, tasks);
            Assert.Contains(task3, tasks);
        }
        [Fact]
        public void Given_There_Are_Incomplete_Tasks_In_The_List_When_Getting_All_Incomplete_Tasks_Then_It_Should_Return_All_Incomplete_Tasks()
        {
            // Arrange
            int num = 2;
            _taskService.AddTask(task1);
            _taskService.AddTask(task2);
            _taskService.AddTask(task3);
            // Act
            task2.MarkComplete();
            var incompletetasks = _taskService.ListIncompleteTasks();
            // Assert
            Assert.Equal(num, incompletetasks.Count);
            Assert.Contains(task1, incompletetasks);
            Assert.Contains(task3, incompletetasks);
            Assert.DoesNotContain(task2, incompletetasks);
        }
        [Fact]
        public void Given_There_Are_Empty_List_When_Getting_All_Tasks_Then_It_Should_Return_Nothing()
        { 
            // Arrange // Act
            var tasks = _taskService.ListAllTasks();
            // Assert
            Assert.Empty(tasks);
            Assert.DoesNotContain(task1, tasks);
            Assert.DoesNotContain(task2, tasks);
            Assert.DoesNotContain(task3, tasks);
        }
        [Fact]
        public void Given_There_Are_Tasks_In_The_List_When_Finding_The_Tasks_With_Specific_Title_Then_It_Should_Find_That_Task()
        {
            // Arrange
            _taskService.AddTask(task1);
            _taskService.AddTask(task2);
            _taskService.AddTask(task3);
            // Act
            var task = _taskService.FindTasksByTitle("Test 2");
            // Assert
            Assert.Equal(task2, task);
            Assert.NotEqual(task1, task);
            Assert.NotEqual(task3, task);
        }
        [Theory]
        [MemberData(nameof(TasksServiceData.GetValueForUpdate), MemberType =typeof(TasksServiceData))]
        public void Given_There_Are_Tasks_In_The_List_When_Updating_The_Task_With_New_Values_Then_Task_Should_Be_Updated(string newtitle, string newdescription, string newnote, bool newisrepeat, DateTime newduedate)
        {
            // Arrange
            int num = 3;
            _taskService.AddTask(task1);
            _taskService.AddTask(task2);
            _taskService.AddTask(task3);
            // Act
            _taskService.UpdateTask(task2.Title, newtitle, newdescription, newnote, newisrepeat, newduedate);
            var tasks = _taskService.ListAllTasks();
            // Assert
            Assert.Equal(num, tasks.Count);
            Assert.Equal(newtitle, task2.Title);
            Assert.Equal(newdescription, task2.Description);
            Assert.Equal(newnote, task2.Notes);
            Assert.Equal(newisrepeat, task2.IsRepeatable);
            Assert.Equal(newduedate, task2.DueDate);
            Assert.Contains(task1, tasks);
            Assert.Contains(task2, tasks);
            Assert.Contains(task3, tasks);
            Assert.Throws<ArgumentNullException>(() => _taskService.UpdateTask(null, null, null, null, false, DateTime.MinValue));
        }
        [Theory]
        [MemberData(nameof(TasksServiceData.GetString), MemberType =typeof(TasksServiceData))]
        public void Given_There_Are_Tasks_In_The_List_When_Marking_A_Task_As_Complete_Then_It_Should_Marked_As_Complete(string title)
        {
            // Arrange
            int num = 3;
            _taskService.AddTask(task1);
            _taskService.AddTask(task2);
            _taskService.AddTask(task3);
            // Act
            _taskService.MarkTasksComplete(title);
            var tasks = _taskService.ListAllTasks();
            var task = _taskService.FindTasksByTitle(title);
            // Assert
            Assert.True(task.IsCompleted);
            Assert.Equal(num, tasks.Count);
            Assert.Contains(task, tasks);
            Assert.Throws<ArgumentNullException>(() => _taskService.MarkTasksComplete(null));
        }
        [Fact]
        public void Given_There_Are_Tasks_With_Repeats_In_The_List_When_Marking_A_Task_As_Complete_Then_It_Should_Set_A_New_Due_Date_Instead_Of_Marking_As_Complete()
        {
            // Arrange
            _taskService.AddTask(repeatingtask);
            // Act
            _taskService.MarkTasksComplete(repeatingtask.Title);
            var tasks = _taskService.ListAllTasks();
            // Assert
            Assert.False(repeatingtask.IsCompleted);
            Assert.True(repeatingtask.IsRepeatable);
            Assert.Single(tasks);
        }
        [Fact]
        public void Given_There_Are_Tasks_With_Repeats_And_End_Date_In_The_List_When_Marking_A_Task_As_Complete_Then_It_Should_Marks_As_Complete_And_Set_IsRepeatable_As_False()
        {
            // Arrange
            _taskService.AddTask(repeatingtaskwithend);
            // Act
            _taskService.MarkTasksComplete(repeatingtaskwithend.Title);
            _taskService.MarkTasksComplete(repeatingtaskwithend.Title);
            _taskService.MarkTasksComplete(repeatingtaskwithend.Title);
            _taskService.MarkTasksComplete(repeatingtaskwithend.Title);
            _taskService.MarkTasksComplete(repeatingtaskwithend.Title);
            var tasks = _taskService.ListAllTasks();
            // Assert
            Assert.True(repeatingtaskwithend.IsCompleted);
            Assert.False(repeatingtaskwithend.IsRepeatable);
            Assert.Single(tasks);
        }
        [Theory]
        [MemberData(nameof(TasksServiceData.GetString), MemberType = typeof(TasksServiceData))]
        public void Given_There_Are_Tasks_In_The_List_When_Deleting_A_Task_Then_It_Should_Be_Deleted(string title)
        {
            // Arrange
            int num = 2;
            _taskService.AddTask(task1);
            _taskService.AddTask(task2);
            _taskService.AddTask(task3);
            var task = _taskService.FindTasksByTitle(title);
            // Act
            _taskService.DeleteTask(title);
            var tasks = _taskService.ListAllTasks();
            // Assert
            Assert.Equal(num, tasks.Count);
            Assert.DoesNotContain(task, tasks);
            Assert.Throws<ArgumentNullException>(() => _taskService.DeleteTask(null));
        }

        /*
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
        */
    }
}
