using BulletJournalApp.Core.Interface;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Core.Services;
using BulletJournalApp.UI;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletJournalApp.UI.Util;

namespace BulletJournalApp.Test.UI
{
    [Collection("Sequential")]
    public class TaskManagerTest
    {
        private Mock<IConsoleLogger> consoleLoggerMock = new();
        private Mock<IFileLogger> fileLoggerMock = new();
        private Mock<IFormatter> formatterMock = new();
        private Mock<IPriorityService> priorityMock = new();
        private Mock<ICategoryService> categoryMock = new();
        private Mock<IScheduleService> scheduleMock = new();
        private Mock<ITasksStatusService> statusMock = new();
        private Mock<IFileService> fileMock = new();
        private Mock<ITaskService> taskMock = new();
        private Mock<UserInput> inputMock = new();
        private UserInput userinput = new UserInput();
        [Fact]
        public async void When_Tasks_Were_Added_Then_It_Should_Succeed()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            // Act
            using var input = new StringReader("1\nTest Task\nmeow\nJun 10, 2025\nL\nN\nM\n\n0\nN");
            Console.SetIn(input);
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.AddTask(It.Is<Tasks>(task => task.Title == "Test Task" && task.Description == "meow")), Times.Once());
            //input.Close();
            ResetReader();
        }
        [Fact]
        public async void When_User_Select_List_All_Tasks_Then_It_Should_Returns_List_Of_Tasks()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test 1", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Test 2", "mrow", Schedule.Monthly);
            tasks.Add(task1);
            tasks.Add(task2);
            // Act
            taskMock.Setup(service => service.ListAllTasks()).Returns(tasks);
            using var userInput = new StringReader("2\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.ListAllTasks(), Times.Once());
            //userInput.Close();
            ResetReader();
        }
        [Fact]
        public async void When_User_Select_List_Incomplete_Tasks_Then_It_Should_Returns_List_Of_Incomplete_Tasks()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test 1", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Test 2", "mrow", Schedule.Monthly);
            var task3 = new Tasks(DateTime.Now, "Test 3", "mrrp", Schedule.Monthly);
            task2.MarkComplete();
            tasks.Add(task1);
            tasks.Add(task2);
            tasks.Add(task3);
            // Act
            taskMock.Setup(service => service.ListIncompleteTasks()).Returns(tasks);
            using var userInput = new StringReader("3\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.ListIncompleteTasks(), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        [Fact]
        public async void When_User_Select_Task_List_By_Low_Priority_Then_It_Should_Return_Tasks_With_Low_Priority()
        {
            // Arrange
            var priority = Priority.Low;
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test task 1", "meow", Schedule.Monthly, Priority.Low);
            var task2 = new Tasks(DateTime.Now, "Test task 2", "mrow", Schedule.Monthly, Priority.Low);
            var task3 = new Tasks(DateTime.Now, "Test task 3", "mrrp", Schedule.Monthly, Priority.Medium);
            tasks.Add(task1);
            tasks.Add(task2);
            tasks.Add(task3);
            // Act
            priorityMock.Setup(service => service.ListTasksByPriority(priority)).Returns(tasks.Where(t => t.Priority == priority).ToList());
            using var userInput = new StringReader("4\nL\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            priorityMock.Verify(user => user.ListTasksByPriority(priority), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        [Fact]
        public async void When_User_Select_Task_List_By_Personal_Category_Then_It_Should_Return_Task_List_With_Personal_Category()
        {
            // Arrange
            var category = Category.Personal;
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test task 1", "meow", Schedule.Monthly, Priority.Low, Category.Personal);
            var task2 = new Tasks(DateTime.Now, "Test task 2", "mrow", Schedule.Monthly, Priority.Low, Category.Personal);
            var task3 = new Tasks(DateTime.Now, "Test task 3", "mrrp", Schedule.Monthly, Priority.Medium, Category.Home);
            var task4 = new Tasks(DateTime.Now, "Test task 4", "mriaw", Schedule.Monthly, Priority.Medium, Category.Financial);
            tasks.Add(task1);
            tasks.Add(task2);
            tasks.Add(task3);
            tasks.Add(task4);
            // Act
            categoryMock.Setup(service => service.ListTasksByCategory(category)).Returns(tasks.Where(t => t.Category == category).ToList());
            using var userInput = new StringReader("5\nP\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            categoryMock.Verify(user => user.ListTasksByCategory(category), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        //[Fact]
        //public void When_User_Select_Task_List_By_Invalid_Category_Then_It_Should_Throws_Exception()
        //{
        //    // Arrange
        //    var category = Category.Personal;
        //    var mockService = new Mock<ITaskService>();
        //    var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, inputMock.Object);
        //    List<Tasks> tasks = new List<Tasks>();
        //    var task1 = new Tasks(DateTime.Now, "Test task 1", "meow", Schedule.Monthly, Priority.Low, Category.Personal);
        //    var task2 = new Tasks(DateTime.Now, "Test task 2", "mrow", Schedule.Monthly, Priority.Low, Category.Personal);
        //    var task3 = new Tasks(DateTime.Now, "Test task 3", "mrrp", Schedule.Monthly, Priority.Medium, Category.Home);
        //    var task4 = new Tasks(DateTime.Now, "Test task 4", "mriaw", Schedule.Monthly, Priority.Medium, Category.Financial);
        //    tasks.Add(task1);
        //    tasks.Add(task2);
        //    tasks.Add(task3);
        //    tasks.Add(task4);
        //    // Act
        //    categoryMock.Setup(service => service.ListTasksByCategory(category)).Returns(tasks.Where(t => t.Category == category).ToList());
        //    using var userInput = new StringReader("5\nFake\n0\nN\n0\n");
        //    Console.SetIn(userInput);
        //    taskManager.TaskManagerUI();
        //    // Assert
        //    categoryMock.Verify(user => user.ListTasksByCategory(category), Times.Never);
        //    Assert.Throws<NullReferenceException>(() => taskManager.TaskManagerUI());
        //    userInput.Close();
        //}
        [Fact]
        public async void When_User_Select_Task_List_By_Works_Category_Then_It_Should_Return_No_Tasks_With_Works_Category()
        {
            // Arrange
            var category = Category.Works;
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test task 1", "meow", Schedule.Monthly, Priority.Low, Category.Personal);
            var task2 = new Tasks(DateTime.Now, "Test task 2", "mrow", Schedule.Monthly, Priority.Low, Category.Personal);
            var task3 = new Tasks(DateTime.Now, "Test task 3", "mrrp", Schedule.Monthly, Priority.Medium, Category.Home);
            var task4 = new Tasks(DateTime.Now, "Test task 4", "mriaw", Schedule.Monthly, Priority.Medium, Category.Financial);
            tasks.Add(task1);
            tasks.Add(task2);
            tasks.Add(task3);
            tasks.Add(task4);
            // Act
            categoryMock.Setup(service => service.ListTasksByCategory(category)).Returns(tasks.Where(t => t.Category == category).ToList());
            using var userInput = new StringReader("5\nW\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            categoryMock.Verify(user => user.ListTasksByCategory(category), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        [Fact]
        public async void When_User_Select_Task_List_By_Overdue_Status_Then_It_Should_Return_Task_List_With_Overdue_Status()
        {
            // Arrange
            var status = TasksStatus.Overdue;
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test task 1", "meow", Schedule.Monthly, Priority.Low, Category.Personal);
            var task2 = new Tasks(DateTime.Now, "Test task 2", "mrow", Schedule.Monthly, Priority.Low, Category.Personal);
            var task3 = new Tasks(DateTime.Now, "Test task 3", "mrrp", Schedule.Monthly, Priority.Medium, Category.Home);
            var task4 = new Tasks(DateTime.Now, "Test task 3", "mrrp", Schedule.Monthly, Priority.Medium, Category.Home);
            task1.ChangeStatus(TasksStatus.Overdue);
            task3.ChangeStatus(TasksStatus.Overdue);
            task4.ChangeStatus(TasksStatus.Late);
            tasks.Add(task1);
            tasks.Add(task2);
            tasks.Add(task3);
            tasks.Add(task4);
            // Act
            statusMock.Setup(service => service.ListTasksByStatus(status)).Returns(tasks.Where(t => t.Status == status).ToList());
            using var userInput = new StringReader("6\nO\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            statusMock.Verify(user => user.ListTasksByStatus(status), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        //[Fact]
        //public void When_User_Select_Task_List_By_Invalid_Status_Then_It_Should_Throw_Exception()
        //{
        //    // Arrange
        //    var status = TasksStatus.Overdue;
        //    var mockService = new Mock<ITaskService>();
        //    var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, inputMock.Object);
        //    List<Tasks> tasks = new List<Tasks>();
        //    var task1 = new Tasks(DateTime.Now, "Test task 1", "meow", Schedule.Monthly, Priority.Low, Category.Personal);
        //    var task2 = new Tasks(DateTime.Now, "Test task 2", "mrow", Schedule.Monthly, Priority.Low, Category.Personal);
        //    var task3 = new Tasks(DateTime.Now, "Test task 3", "mrrp", Schedule.Monthly, Priority.Medium, Category.Home);
        //    var task4 = new Tasks(DateTime.Now, "Test task 3", "mrrp", Schedule.Monthly, Priority.Medium, Category.Home);
        //    task1.ChangeStatus(TasksStatus.Overdue);
        //    task3.ChangeStatus(TasksStatus.Overdue);
        //    task4.ChangeStatus(TasksStatus.Late);
        //    tasks.Add(task1);
        //    tasks.Add(task2);
        //    tasks.Add(task3);
        //    tasks.Add(task4);
        //    // Act
        //    statusMock.Setup(service => service.ListTasksByStatus(status)).Returns(tasks.Where(t => t.Status == status).ToList());
        //    using var userInput = new StringReader("6\nS\n0\nN\n0\n");
        //    Console.SetIn(userInput);
        //    taskManager.TaskManagerUI();
        //    // Assert
        //    statusMock.Verify(user => user.ListTasksByStatus(status), Times.Never);
        //    Assert.Throws<NullReferenceException>(() => taskManager.TaskManagerUI());
        //    userInput.Close();
        //}
        [Fact]
        public async void When_User_Select_Task_List_By_ToDo_Or_Done_Status_Then_It_Should_Return_Task_List_With_Overdue_Status()
        {
            // Arrange
            var status1 = TasksStatus.ToDo;
            var status2 = TasksStatus.Done;
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            // Act
            statusMock.Setup(service => service.ListTasksByStatus(status1)).Returns(tasks.Where(t => t.Status == status1).ToList());
            using var userInput = new StringReader("6\nT\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            statusMock.Verify(user => user.ListTasksByStatus(status1), Times.AtLeastOnce);
            //userInput.Close();
            ResetReader();
        }
        //[Fact]
        //public void When_User_Select_Task_List_By_Invalid_Schedule_Then_It_Should_Throw_Exception()
        //{
        //    // Arrange
        //    var schedule = Schedule.Daily;
        //    var mockService = new Mock<ITaskService>();
        //    var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, inputMock.Object);
        //    List<Tasks> tasks = new List<Tasks>();
        //    var task1 = new Tasks(DateTime.Now, "Test task 1", "meow", Schedule.Monthly);
        //    var task2 = new Tasks(DateTime.Now, "Test task 2", "mrow", Schedule.Daily);
        //    var task3 = new Tasks(DateTime.Now, "Test task 3", "mrrp", Schedule.Daily);
        //    tasks.Add(task1);
        //    tasks.Add(task2);
        //    tasks.Add(task3);
        //    // Act
        //    scheduleMock.Setup(service => service.ListTasksBySchedule(schedule)).Returns(tasks.Where(t => t.schedule == schedule).ToList());
        //    using var userInput = new StringReader("7\nA\n0\nN\n0\n");
        //    Console.SetIn(userInput);
        //    taskManager.TaskManagerUI();
        //    // Assert
        //    scheduleMock.Verify(user => user.ListTasksBySchedule(schedule), Times.Never);
        //    Assert.Throws<NullReferenceException>(() => taskManager.TaskManagerUI());
        //    userInput.Close();
        //}
        [Fact]
        public async void When_User_Select_Task_List_By_Weekly_Schedule_Then_It_Should_Return_Task_List_With_No_Weekly_Schedule()
        {
            // Arrange
            var schedule = Schedule.Weekly;
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            // Act
            scheduleMock.Setup(service => service.ListTasksBySchedule(schedule)).Returns(tasks.Where(t => t.schedule == schedule).ToList());
            using var userInput = new StringReader("7\nW\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            scheduleMock.Verify(user => user.ListTasksBySchedule(schedule), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        [Fact]
        public async void When_User_Select_Task_List_By_Daily_Schedule_Then_It_Should_Return_Task_List_With_Daily_Schedule()
        {
            // Arrange
            var schedule = Schedule.Daily;
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test task 1", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Test task 2", "mrow", Schedule.Daily);
            var task3 = new Tasks(DateTime.Now, "Test task 3", "mrrp", Schedule.Daily);
            tasks.Add(task1);
            tasks.Add(task2);
            tasks.Add(task3);
            // Act
            scheduleMock.Setup(service => service.ListTasksBySchedule(schedule)).Returns(tasks.Where(t => t.schedule == schedule).ToList());
            using var userInput = new StringReader("7\nD\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            scheduleMock.Verify(user => user.ListTasksBySchedule(schedule), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        [Fact]
        public async void When_User_Select_Mark_Task_Complete_Then_It_Should_Mark_It_As_Completed()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var task = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
            taskMock.Setup(service => service.MarkTasksComplete("Test Task"));
            // Act
            using var userInput = new StringReader("8\nTest Task\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.MarkTasksComplete("Test Task"), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        //[Fact]
        //public void When_User_Tried_To_Mark_Task_Complete_Then_It_Should_Throws_Exception()
        //{
        //    // Arrange
        //    var mockService = new Mock<ITaskService>();
        //    var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, inputMock.Object);
        //    mockService.Setup(service => service.MarkTasksComplete("Test Task")).Throws(new NullReferenceException());
        //    // Act
        //    using var userInput = new StringReader("8\n\n0\nN\n0\n");
        //    Console.SetIn(userInput);
        //    taskManager.TaskManagerUI();
        //    // Assert
        //    mockService.Verify(user => user.MarkTasksComplete("Test Task"), Times.Never);
        //    Assert.Throws<NullReferenceException>(() => taskManager.TaskManagerUI());
        //    userInput.Close();
        //}
        [Fact]
        public async void When_User_Select_Find_Task_By_Specific_Title_Then_It_Should_Return_With_That_Title()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, new UserInput());
            var tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test Task 1", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Meow", "meow", Schedule.Monthly);
            var task3 = new Tasks(DateTime.Now, "Test Task 3", "meow", Schedule.Monthly);
            tasks.Add(task1);
            tasks.Add(task2);
            tasks.Add(task3);
            taskMock.Setup(service => service.FindTasksByTitle("Meow")).Returns(task2);
            // Act
            using var userInput = new StringReader("9\nMeow\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.FindTasksByTitle("Meow"), Times.Once);
            ResetReader();
        }
        [Fact]
        public async void When_User_Select_Find_Task_By_Invalid_Title_Then_It_Should_Return_With_Tasks_Not_Found()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            taskMock.Setup(service => service.FindTasksByTitle("Fake Test"));
            // Act
            using var userInput = new StringReader("9\nFake Test\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.FindTasksByTitle("Fake Test"), Times.Once);
            ResetReader();
        }
        [Fact]
        public async void When_User_Select_Update_Task_Then_Task_Should_Update_With_New_Input()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Monthly);
            tasks.Add(task1);
            tasks.Add(task2);
            taskMock.Setup(service => service.UpdateTask("Test Task 2", "Updated Task", "Updated Description", "Updated Note", DateTime.Parse("Jun 6, 2025")));
            // Act
            using var userInput = new StringReader("10\nTest Task 2\nUpdated Task\nUpdated Description\nUpdated Note\nJun 6, 2025\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.UpdateTask("Test Task 2", "Updated Task", "Updated Description", "Updated Note", DateTime.Parse("Jun 6, 2025")), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        [Fact]
        public async void When_User_Select_Update_Task_With_Invalid_Date_Then_Task_Should_Update_With_New_Input()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Monthly);
            tasks.Add(task1);
            tasks.Add(task2);
            taskMock.Setup(service => service.UpdateTask("Test Task 2", "Updated Task", "Updated Description", "Updated Note", null));
            // Act
            using var userInput = new StringReader("10\nTest Task 2\nUpdated Task\nUpdated Description\nUpdated Note\n\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.UpdateTask("Test Task 2", "Updated Task", "Updated Description", "Updated Note", null), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        //[Fact]
        //public void When_User_Tried_To_Update_Task_Then_Task_Should_Throw_Exception()
        //{
        //    // Arrange
        //    var mockService = new Mock<ITaskService>();
        //    var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, inputMock.Object);
        //    var tasks = new List<Tasks>();
        //    var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
        //    var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Monthly);
        //    tasks.Add(task1);
        //    tasks.Add(task2);
        //    mockService.Setup(service => service.UpdateTask("", "Updated Task", "Updated Description", "Updated Note", DateTime.Parse("Jun 6, 2025")));
        //    // Act
        //    using var userInput = new StringReader("10\n\nUpdated Task\nUpdated Description\nUpdated Note\nJun 6, 2025\n0\nN\n0\n");
        //    Console.SetIn(userInput);
        //    taskManager.TaskManagerUI();
        //    // Assert
        //    mockService.Verify(user => user.UpdateTask("Test Task 2", "Updated Task", "Updated Description", "Updated Note", DateTime.Parse("Jun 6, 2025")), Times.Never);
        //    Assert.Throws<NullReferenceException>(() => taskManager.TaskManagerUI());
        //    userInput.Close();
        //}
        [Fact]
        public async void When_User_Select_Change_Priority_Then_Task_Should_Change_To_New_Priority()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Monthly);
            tasks.Add(task1);
            tasks.Add(task2);
            priorityMock.Setup(service => service.ChangePriority("Test Task 2", Priority.High));
            // Act
            using var userInput = new StringReader("11\nTest Task 2\nH\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            priorityMock.Verify(user => user.ChangePriority("Test Task 2", Priority.High), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        //[Fact]
        //public void When_User_Tried_To_Change_Priority_Then_It_Should_Throws_Exception()
        //{
        //    // Arrange
        //    var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, inputMock.Object);
        //    var tasks = new List<Tasks>();
        //    var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
        //    var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Monthly);
        //    tasks.Add(task1);
        //    tasks.Add(task2);
        //    priorityMock.Setup(service => service.ChangePriority("Test Task 2", Priority.High));
        //    // Act
        //    using var userInput = new StringReader("11\nTest Task 2\nA\n0\nN\n0\n");
        //    Console.SetIn(userInput);
        //    taskManager.TaskManagerUI();
        //    // Assert
        //    priorityMock.Verify(user => user.ChangePriority("Test Task 2", Priority.High), Times.Never);
        //    Assert.Throws<NullReferenceException>(() => { });
        //    userInput.Close();
        //}
        [Fact]
        public async void When_User_Select_Change_Status_Then_Task_Should_Change_To_New_Status()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Monthly);
            tasks.Add(task1);
            tasks.Add(task2);
            statusMock.Setup(service => service.ChangeStatus("Test Task 1", TasksStatus.InProgress));
            // Act
            using var userInput = new StringReader("12\nTest Task 2\nI\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            statusMock.Verify(user => user.ChangeStatus("Test Task 2", TasksStatus.InProgress), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        //[Fact]
        //public void When_User_Tried_To_Change_Status_Then_It_Should_Throws_Exception()
        //{
        //    // Arrange
        //    var mockService = new Mock<ITaskService>();
        //    var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, inputMock.Object);
        //    var tasks = new List<Tasks>();
        //    var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
        //    var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Monthly);
        //    tasks.Add(task1);
        //    tasks.Add(task2);
        //    statusMock.Setup(service => service.ChangeStatus("Test Task 1", TasksStatus.InProgress));
        //    // Act
        //    using var userInput = new StringReader("12\nTest Task 2\nP\n0\nN\n0\n");
        //    Console.SetIn(userInput);
        //    taskManager.TaskManagerUI();
        //    // Assert
        //    statusMock.Verify(user => user.ChangeStatus("Test Task 2", TasksStatus.InProgress), Times.Never);
        //    Assert.Throws<NullReferenceException>(() => taskManager.TaskManagerUI());
        //    userInput.Close();
        //}
        [Fact]
        public async void When_User_Select_Change_Category_Then_Task_Should_Change_To_New_Category()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Monthly);
            tasks.Add(task1);
            tasks.Add(task2);
            categoryMock.Setup(service => service.ChangeCategory("Test Task 2", Entries.TASKS, Category.Education));
            // Act
            using var userInput = new StringReader("13\nTest Task 2\nE\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            categoryMock.Verify(user => user.ChangeCategory("Test Task 2", Entries.TASKS, Category.Education), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        //[Fact]
        //public void When_User_Tried_To_Change_Category_Then_It_Should_Throw_Exception()
        //{
        //    // Arrange
        //    var mockService = new Mock<ITaskService>();
        //    var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, inputMock.Object);
        //    var tasks = new List<Tasks>();
        //    var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
        //    var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Monthly);
        //    tasks.Add(task1);
        //    tasks.Add(task2);
        //    categoryMock.Setup(service => service.ChangeCategory("Test Task 2", Entries.TASKS, Category.Education));
        //    // Act
        //    using var userInput = new StringReader("13\nTest Task 2\nL\n0\nN\n0\n");
        //    Console.SetIn(userInput);
        //    taskManager.TaskManagerUI();
        //    // Assert
        //    categoryMock.Verify(user => user.ChangeCategory("Test Task 2", Entries.TASKS, Category.Education), Times.Never);
        //    Assert.Throws<NullReferenceException>(() => taskManager.TaskManagerUI());
        //    userInput.Close();
        //}
        [Fact]
        public async void When_User_Select_Change_Schedule_Then_Task_Should_Change_To_New_Schedule()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Monthly);
            tasks.Add(task1);
            tasks.Add(task2);
            scheduleMock.Setup(service => service.ChangeSchedule("Test Task", Entries.TASKS, Schedule.Yearly));
            // Act
            using var userInput = new StringReader("14\nTest Task\nY\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            scheduleMock.Verify(user => user.ChangeSchedule("Test Task", Entries.TASKS, Schedule.Yearly), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        //[Fact]
        //public void When_User_Select_Tried_To_Schedule_Then_It_Should_Throws_Exception()
        //{
        //    // Arrange
        //    var mockService = new Mock<ITaskService>();
        //    var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, inputMock.Object);
        //    var tasks = new List<Tasks>();
        //    var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
        //    var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Monthly);
        //    tasks.Add(task1);
        //    tasks.Add(task2);
        //    scheduleMock.Setup(service => service.ChangeSchedule("Test Task", Entries.TASKS, Schedule.Yearly));
        //    // Act
        //    using var userInput = new StringReader("14\nTest Task\nA\n0\nN\n0\n");
        //    Console.SetIn(userInput);
        //    taskManager.TaskManagerUI();
        //    // Assert
        //    scheduleMock.Verify(user => user.ChangeSchedule("Test Task", Entries.TASKS, Schedule.Yearly), Times.Never);
        //    Assert.Throws<NullReferenceException>(() => taskManager.TaskManagerUI());
        //    userInput.Close();
        //}
        [Fact]
        public async void When_User_Select_Delete_Task_Then_Task_Should_Be_Deleted()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Weekly);
            var task3 = new Tasks(DateTime.Now, "Test Task 3", "mrrp", Schedule.Daily);
            var task4 = new Tasks(DateTime.Now, "Test Task 4", "mriaw", Schedule.Yearly);
            tasks.Add(task1);
            tasks.Add(task2);
            tasks.Add(task3);
            tasks.Add(task4);
            taskMock.Setup(service => service.DeleteTask("Test Task 3"));
            // Act
            using var userInput = new StringReader("15\nTest Task 3\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.DeleteTask("Test Task 3"), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        //[Fact]
        //public void When_User_Tried_To_Delete_Task_Then_It_Should_Throws_Exception()
        //{
        //    // Arrange
        //    var mockService = new Mock<ITaskService>();
        //    var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, inputMock.Object);
        //    var tasks = new List<Tasks>();
        //    var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
        //    var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Weekly);
        //    var task3 = new Tasks(DateTime.Now, "Test Task 3", "mrrp", Schedule.Daily);
        //    var task4 = new Tasks(DateTime.Now, "Test Task 4", "mriaw", Schedule.Yearly);
        //    tasks.Add(task1);
        //    tasks.Add(task2);
        //    tasks.Add(task3);
        //    tasks.Add(task4);
        //    mockService.Setup(service => service.DeleteTask("Test Task 3"));
        //    // Act
        //    using var userInput = new StringReader("15\nFake Test\n0\nN\n0\n");
        //    Console.SetIn(userInput);
        //    taskManager.TaskManagerUI();
        //    // Assert
        //    mockService.Verify(user => user.DeleteTask("Test Task 3"), Times.Never);
        //    Assert.Throws<NullReferenceException>(() => taskManager.TaskManagerUI());
        //    userInput.Close();
        //}
        [Fact]
        public async void When_User_Select_Load_Task_Then_Tasks_Should_Be_Loaded()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var service =  new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var fileservice = new FileService(new Formatter(), new ConsoleLogger(), new FileLogger(), service);
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var path = Path.Combine("test");
            var tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Weekly);
            tasks.Add(task1);
            tasks.Add(task2);
            fileservice.SaveTasks(path, tasks);
            fileMock.Setup(service => service.LoadTasks("test"));
            // Act
            using var userInput = new StringReader("16\ntest\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            fileMock.Verify(user => user.LoadTasks(It.IsAny<string>()), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        [Fact]
        public async void When_User_Save_Tasks_Then_It_Should_Stored_In_A_Txt_File()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Weekly);
            tasks.Add(task1);
            tasks.Add(task2);
            fileMock.Setup(service => service.SaveTasks(It.IsAny<string>(), It.IsAny<List<Tasks>>()));
            // Act
            using var userInput = new StringReader("0\nY\ntest\nY\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            fileMock.Verify(user => user.SaveTasks(It.IsAny<string>(), It.IsAny<List<Tasks>>()), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        [Fact]
        public async void When_User_Cancelled_Saving_Tasks_Then_It_Should_Stored_In_A_Txt_File()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);

            var tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
            var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Weekly);
            tasks.Add(task1);
            tasks.Add(task2);
            fileMock.Setup(service => service.SaveTasks(It.IsAny<string>(), It.IsAny<List<Tasks>>()));
            // Act
            using var userInput = new StringReader("0\nC\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            fileMock.Verify(user => user.SaveTasks(It.IsAny<string>(), It.IsAny<List<Tasks>>()), Times.Never);
            //userInput.Close();
            ResetReader();
        }
        //[Fact]
        //public void When_User_Tried_To_Save_Tasks_With_Invalid_Strings_Then_It_Should_Throws_Exception()
        //{
        //    // Arrange
        //    var mockService = new Mock<ITaskService>();
        //    var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, inputMock.Object);

        //    var tasks = new List<Tasks>();
        //    var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
        //    var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Weekly);
        //    tasks.Add(task1);
        //    tasks.Add(task2);
        //    fileMock.Setup(service => service.SaveTasks(It.IsAny<string>(), It.IsAny<List<Tasks>>()));
        //    // Act
        //    using var userInput = new StringReader("0\nY\n\nY\n0\n");
        //    Console.SetIn(userInput);
        //    taskManager.TaskManagerUI();
        //    // Assert
        //    fileMock.Verify(user => user.SaveTasks(It.IsAny<string>(), It.IsAny<List<Tasks>>()), Times.Once);
        //    Assert.Throws<NullReferenceException>(() => taskManager.TaskManagerUI());
        //    userInput.Close();
        //}
        //[Fact]
        //public void When_User_Cancelled_To_Overwrite_File_Then_It_Should_Throws_Exception()
        //{
        //    // Arrange
        //    var mockService = new Mock<ITaskService>();
        //    var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, inputMock.Object);
        //    var tasks = new List<Tasks>();
        //    var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
        //    var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Weekly);
        //    tasks.Add(task1);
        //    tasks.Add(task2);
        //    fileMock.Setup(service => service.SaveTasks(It.IsAny<string>(), It.IsAny<List<Tasks>>()));
        //    // Act
        //    using var userInput = new StringReader("0\nY\ntest\nN\n0\nN\n0");
        //    Console.SetIn(userInput);
        //    taskManager.TaskManagerUI();
        //    // Assert
        //    fileMock.Verify(user => user.SaveTasks(It.IsAny<string>(), It.IsAny<List<Tasks>>()), Times.Never);
        //    Assert.Throws<NullReferenceException>(() => taskManager.TaskManagerUI());
        //    userInput.Close();
        //}
        //[Fact]
        //public void When_User_Tried_To_Load_Tasks_With_Empty_File_Name_Then_It_Should_Throws_Exception()
        //{
        //    // Arrange
        //    var mockService = new Mock<ITaskService>();
        //    var service = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
        //    var fileservice = new FileService(new Formatter(), new ConsoleLogger(), new FileLogger(), service);
        //    var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, inputMock.Object);
        //    var path = Path.Combine("test");
        //    var tasks = new List<Tasks>();
        //    var task1 = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly);
        //    var task2 = new Tasks(DateTime.Now, "Test Task 2", "mrow", Schedule.Weekly);
        //    tasks.Add(task1);
        //    tasks.Add(task2);
        //    fileservice.SaveTasks(path, tasks);
        //    fileMock.Setup(service => service.LoadTasks("test"));
        //    // Act
        //    using var userInput = new StringReader("16\n\n0\nN\n0");
        //    Console.SetIn(userInput);
        //    taskManager.TaskManagerUI();
        //    // Assert
        //    fileMock.Verify(user => user.LoadTasks(It.IsAny<string>()), Times.Never);
        //    Assert.Throws<NullReferenceException>(() => taskManager.TaskManagerUI());
        //    userInput.Close();
        //}
        //[Fact]
        //public void When_User_Tried_To_Add_Task_Then_It_Should_Throw_Exception()
        //{
        //    // Arrange
        //    var mockService = new Mock<ITaskService>();
        //    var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, inputMock.Object);
        //    // Act
        //    using var input = new StringReader("1\n\nmeow\nJun 10, 2025\nL\nN\nM\n\n0\nn\n0");
        //    Console.SetIn(input);
        //    taskManager.TaskManagerUI();
        //    // Assert
        //    mockService.Verify(user => user.AddTask(It.Is<Tasks>(task => task.Title == "Test Task" && task.Description == "meow")), Times.Never());
        //    Assert.Throws<NullReferenceException>(() => taskManager.TaskManagerUI());
        //    input.Close();
        //}
        [Fact]
        public async void When_User_Select_List_All_Tasks_With_Empty_Tasks_Then_It_Should_Returns_Nothing()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            // Act
            taskMock.Setup(service => service.ListAllTasks()).Returns(tasks);
            using var userInput = new StringReader("2\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.ListAllTasks(), Times.Once());
            //userInput.Close();
            ResetReader();
        }
        [Fact]
        public async void When_User_Select_List_Incomplete_Tasks_With_No_Incompletes_Then_It_Should_Returns_List_Of_Incomplete_Tasks()
        {
            // Arrange
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            // Act
            taskMock.Setup(service => service.ListIncompleteTasks()).Returns(tasks);
            using var userInput = new StringReader("3\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.ListIncompleteTasks(), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        //[Fact]
        //public void When_User_Select_Task_List_By_Low_Priority_With_Invalid_Priority_Then_It_Should_Throw_Exception()
        //{
        //    // Arrange
        //    var priority = Priority.Low;
        //    var mockService = new Mock<ITaskService>();
        //    var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, inputMock.Object);
        //    List<Tasks> tasks = new List<Tasks>();
        //    var task1 = new Tasks(DateTime.Now, "Test task 1", "meow", Schedule.Monthly, Priority.Low);
        //    var task2 = new Tasks(DateTime.Now, "Test task 2", "mrow", Schedule.Monthly, Priority.Low);
        //    var task3 = new Tasks(DateTime.Now, "Test task 3", "mrrp", Schedule.Monthly, Priority.Medium);
        //    tasks.Add(task1);
        //    tasks.Add(task2);
        //    tasks.Add(task3);
        //    // Act
        //    priorityMock.Setup(service => service.ListTasksByPriority(priority)).Returns(tasks.Where(t => t.Priority == priority).ToList());
        //    using var userInput = new StringReader("4\nALL\n0\nN\n0\n");
        //    Console.SetIn(userInput);
        //    taskManager.TaskManagerUI();
        //    // Assert
        //    priorityMock.Verify(user => user.ListTasksByPriority(priority), Times.Never);
        //    Assert.Throws<NullReferenceException>(() => taskManager.TaskManagerUI());
        //    userInput.Close();
        //}
        [Fact]
        public async void When_User_Select_Task_List_By_High_Priority_Then_It_Should_Return_No_Tasks()
        {
            // Arrange
            var priority = Priority.High;
            var mockService = new Mock<ITaskService>();
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            var task1 = new Tasks(DateTime.Now, "Test task 1", "meow", Schedule.Monthly, Priority.Low);
            var task2 = new Tasks(DateTime.Now, "Test task 2", "mrow", Schedule.Monthly, Priority.Low);
            var task3 = new Tasks(DateTime.Now, "Test task 3", "mrrp", Schedule.Monthly, Priority.Medium);
            tasks.Add(task1);
            tasks.Add(task2);
            tasks.Add(task3);
            // Act
            priorityMock.Setup(service => service.ListTasksByPriority(priority)).Returns(tasks.Where(t => t.Priority == priority).ToList());
            using var userInput = new StringReader("4\nH\n0\nN\n");
            Console.SetIn(userInput);
            taskManager.TaskManagerUI();
            // Assert
            priorityMock.Verify(user => user.ListTasksByPriority(priority), Times.Once);
            //userInput.Close();
            ResetReader();
        }
        [Fact]
        public async void When_Priority_Were_Selected_Then_It_Should_Return_Priority()
        {
            // Arrange
            Priority priority;
            using var Input = new StringReader("M\n");
            Console.SetIn(Input);
            // Act
            priority = userinput.GetPriorityInput("Priority");
            // Assert
            Assert.Equal(Priority.Medium, priority);
            //Input.Close();
            ResetReader();
        }
        [Fact]
        public async void When_Category_Were_Selected_Then_It_Should_Return_Category()
        {
            // Arrange
            Category category1;
            Category category2;
            // Act
            using var userInput1 = new StringReader("H\n");
            Console.SetIn(userInput1);
            category1 = userinput.GetCategoryInput("Category");
            using var userInput2 = new StringReader("F\n");
            Console.SetIn(userInput2);
            category2 = userinput.GetCategoryInput("Category");
            // Assert
            Assert.Equal(Category.Home, category1);
            Assert.Equal(Category.Financial, category2);
            //userInput1.Close();
            //userInput2.Close();
            ResetReader();
        }
        [Fact]
        public async void When_Schedule_Were_Selected_Then_It_Should_Return_Schedule()
        {
            // Arrange
            Schedule schedule;
            // Act
            using var userInput1 = new StringReader("Q\n");
            Console.SetIn(userInput1);
            schedule = userinput.GetScheduleInput("Schedule");
            // Assert
            Assert.Equal(Schedule.Quarterly, schedule);
            //userInput1.Close();
            ResetReader();
        }
        [Fact]
        public async void When_Status_Were_Selected_Then_It_Should_Return_Status()
        {
            // Arrange
            TasksStatus status1;
            TasksStatus status2;
            // Act
            using var userInput1 = new StringReader("D\n");
            Console.SetIn(userInput1);
            status1 = userinput.GetTaskStatusInput("Status");
            using var userInput2 = new StringReader("L\n");
            Console.SetIn(userInput2);
            status2 = userinput.GetTaskStatusInput("Status");
            // Assert
            Assert.Equal(TasksStatus.Done, status1);
            Assert.Equal(TasksStatus.Late, status2);
            //userInput1.Close();
            //userInput2.Close();
            ResetReader();
        }
        public void ResetReader()
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
    }
}
