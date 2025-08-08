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
using BulletJournalApp.Test.UI.Data;

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
        private Mock<IItemService> itemMock = new();
        private ItemService itemService = new ItemService(new ConsoleLogger(), new FileLogger());
        private StringReader input;
        private TaskManagerTestData _data = new();




        [Fact]
        public void When_Tasks_Were_Added_Then_It_Should_Succeed()
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            input = new StringReader("1\nTest Task\nmeow\nJun 10, 2025\nL\nN\nM\n\nN\n0\nN");
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.AddTask(It.Is<Tasks>(task => task.Title == "Test Task" && task.Description == "meow")), Times.Once());
            ResetReader();
        }
        [Fact]
        public void When_Tasks_Were_Added_With_Repeating_Date_Then_It_Should_Succeed()
        {
            // Arrange]
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            input = new StringReader("1\nTest Task\nmeow\nJun 10, 2025\nL\nN\nM\n\nY\n7\nJul 31, 2025\n0\nN");
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.AddTask(It.Is<Tasks>(task => task.Title == "Test Task" && task.Description == "meow")), Times.Once());
            ResetReader();
        }
        [Fact]
        public void When_User_Select_List_All_Tasks_Then_It_Should_Returns_List_Of_Tasks()
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            taskMock.Setup(service => service.ListAllTasks()).Returns(tasks);
            input = new StringReader("2\n0\nN\n");
            // Act
            Console.SetIn(input);
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.ListAllTasks(), Times.Once());
            ResetReader();
        }
        [Fact]
        public void When_User_Select_List_Incomplete_Tasks_Then_It_Should_Returns_List_Of_Incomplete_Tasks()
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            taskMock.Setup(service => service.ListIncompleteTasks()).Returns(tasks);
            input = new StringReader("3\n0\nN\n");
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.ListIncompleteTasks(), Times.Once);
            ResetReader();
        }
        [Theory]
        [MemberData(nameof(TaskManagerTestData.GetPriorityListInput), MemberType =typeof(TaskManagerTestData))]
        public void When_User_Select_Task_List_By_Specific_Priority_Then_It_Should_Return_Tasks_With_Specific_Priority(Priority priority, string userInput)
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            priorityMock.Setup(service => service.ListTasksByPriority(priority)).Returns(tasks.Where(t => t.Priority == priority).ToList());
            input = new StringReader(userInput);
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            priorityMock.Verify(user => user.ListTasksByPriority(priority), Times.Once);
            ResetReader();
        }
        [Theory]
        [MemberData(nameof(TaskManagerTestData.GetCategoryListInput), MemberType = typeof(TaskManagerTestData))]
        public void When_User_Select_Task_List_By_Specific_Category_Then_It_Should_Return_Task_List_With_Specific_Category(Category category, string userInput)
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            categoryMock.Setup(service => service.ListTasksByCategory(category)).Returns(tasks.Where(t => t.Category == category).ToList());
            input = new StringReader(userInput);
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            categoryMock.Verify(user => user.ListTasksByCategory(category), Times.Once);
            ResetReader();
        }
        [Theory]
        [MemberData(nameof(TaskManagerTestData.GetStatusListInput), MemberType = typeof(TaskManagerTestData))]
        public void When_User_Select_Task_List_By_Specific_Status_Then_It_Should_Return_Task_List_With_Specific_Status(TasksStatus status, string userInput)
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            statusMock.Setup(service => service.ListTasksByStatus(status)).Returns(tasks.Where(t => t.Status == status).ToList());
            input = new StringReader(userInput);
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            statusMock.Verify(user => user.ListTasksByStatus(status), Times.Once);
            ResetReader();
        }
        [Theory]
        [MemberData(nameof(TaskManagerTestData.GetScheduleListInput), MemberType = typeof(TaskManagerTestData))]
        public void When_User_Select_Task_List_By_Weekly_Schedule_Then_It_Should_Return_Task_List_With_Only_Weekly_Schedule(Schedule schedule, string userInput)
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            scheduleMock.Setup(service => service.ListTasksBySchedule(schedule)).Returns(tasks.Where(t => t.schedule == schedule).ToList());
            input = new StringReader(userInput);
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            scheduleMock.Verify(user => user.ListTasksBySchedule(schedule), Times.Once);
            ResetReader();
        }
        [Fact]
        public void When_User_Select_Mark_Task_Complete_Then_It_Should_Mark_It_As_Completed()
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var task = new Tasks(DateTime.Now, "Test Task", "meow", Schedule.Monthly, false);
            taskMock.Setup(service => service.MarkTasksComplete("Test Task"));
            input = new StringReader("8\nTest Task\n0\nN\n");
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.MarkTasksComplete("Test Task"), Times.Once);
            ResetReader();
        }
        [Fact]
        public void When_User_Select_Find_Task_By_Specific_Title_Then_It_Should_Return_With_That_Title()
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, new UserInput());
            var tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            Tasks task3 = tasks.FirstOrDefault(t => t.Title == "Test 3");
            taskMock.Setup(service => service.FindTasksByTitle("Test 3")).Returns(task3);
            input = new StringReader("9\nTest 3\n0\nN\n");
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.FindTasksByTitle("Test 3"), Times.Once);
            ResetReader();
        }
        [Fact]
        public void When_User_Select_Find_Task_By_Invalid_Title_Then_It_Should_Return_With_Tasks_Not_Found()
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            taskMock.Setup(service => service.FindTasksByTitle("Fake Test"));
            // Act
            input = new StringReader("9\nFake Test\n0\nN\n");
            Console.SetIn(input);
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.FindTasksByTitle("Fake Test"), Times.Once);
            ResetReader();
        }
        [Fact]
        public void When_User_Select_Update_Task_Then_Task_Should_Update_With_New_Input()
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            taskMock.Setup(service => service.UpdateTask("Test 2", "Updated Task", "Updated Description", "Updated Note", false, DateTime.Parse("Jun 6, 2025"), default, default));
            input = new StringReader("10\nTest 2\nUpdated Task\nUpdated Description\nUpdated Note\nJun 6, 2025\n\n0\nN\n");
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.UpdateTask("Test 2", "Updated Task", "Updated Description", "Updated Note", false, DateTime.Parse("Jun 6, 2025"), 7, default), Times.Once);
            ResetReader();
        }
        [Fact]
        public void When_User_Select_Update_Task_With_Invalid_Date_Then_Task_Should_Update_With_New_Input()
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            taskMock.Setup(service => service.UpdateTask("Test 2", "Updated Task", "Updated Description", "Updated Note", false, DateTime.MinValue, default, default));
            input = new StringReader("10\nTest 2\nUpdated Task\nUpdated Description\nUpdated Note\n\n\n0\nN\n");
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.UpdateTask("Test 2", "Updated Task", "Updated Description", "Updated Note", false, DateTime.MinValue, 7, default), Times.Once);
            ResetReader();
        }
        [Theory]
        [MemberData(nameof(TaskManagerTestData.GetPriorityUpdateInput), MemberType = typeof(TaskManagerTestData))]
        public void When_User_Select_Change_Priority_Then_Task_Should_Change_To_New_Priority(Priority priority, string userInput)
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            priorityMock.Setup(service => service.ChangePriority("Test 2", priority));
            input = new StringReader(userInput);
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            priorityMock.Verify(user => user.ChangePriority("Test 2", priority), Times.Once);
            ResetReader();
        }
        [Theory]
        [MemberData(nameof(TaskManagerTestData.GetStatusUpdateInput), MemberType = typeof(TaskManagerTestData))]
        public void When_User_Select_Change_Status_Then_Task_Should_Change_To_New_Status(TasksStatus status, string userInput)
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            statusMock.Setup(service => service.ChangeStatus("Test 1", status));
            input = new StringReader(userInput);
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            statusMock.Verify(user => user.ChangeStatus("Test 1", status), Times.Once);
            ResetReader();
        }
        [Theory]
        [MemberData(nameof(TaskManagerTestData.GetCategoryUpdateInput), MemberType = typeof(TaskManagerTestData))]
        public void When_User_Select_Change_Category_Then_Task_Should_Change_To_New_Category(Category category, string userInput)
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            categoryMock.Setup(service => service.ChangeCategory("Test 2", Entries.TASKS, category));
            input = new StringReader(userInput);
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            categoryMock.Verify(user => user.ChangeCategory("Test 2", Entries.TASKS, category), Times.Once);
            ResetReader();
        }
        [Theory]
        [MemberData(nameof(TaskManagerTestData.GetScheduleUpdateInput), MemberType = typeof(TaskManagerTestData))]
        public void When_User_Select_Change_Schedule_Then_Task_Should_Change_To_New_Schedule(Schedule schedule, string userInput)
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            scheduleMock.Setup(service => service.ChangeSchedule("Test 1", Entries.TASKS, schedule));
            input = new StringReader(userInput);
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            scheduleMock.Verify(user => user.ChangeSchedule("Test 1", Entries.TASKS, schedule), Times.Once);
            ResetReader();
        }
        [Fact]
        public void When_User_Select_Delete_Task_Then_Task_Should_Be_Deleted()
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            taskMock.Setup(service => service.DeleteTask("Test 3"));
            input = new StringReader("15\nTest 3\n0\nN\n");
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.DeleteTask("Test 3"), Times.Once);
            ResetReader();
        }
        [Fact]
        public void When_User_Select_Load_Task_Then_Tasks_Should_Be_Loaded()
        {
            // Arrange
            var mockServiceFake = new Mock<IMealService>();
            var service =  new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
            var fileservice = new FileService(formatterMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, service, itemService, mockServiceFake.Object);
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var path = Path.Combine("test");
            var tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            fileservice.SaveFunction(path, Entries.TASKS, tasks, null, null);
            fileMock.Setup(service => service.LoadFunction("test", Entries.TASKS));
            input = new StringReader("16\ntest\n0\nN\n");
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            fileMock.Verify(user => user.LoadFunction(It.IsAny<string>(), Entries.TASKS), Times.Once);
            ResetReader();
        }
        [Fact]
        public void When_User_Save_Tasks_Then_It_Should_Stored_In_A_Txt_File()
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            fileMock.Setup(service => service.SaveFunction(It.IsAny<string>(), Entries.TASKS, It.IsAny<List<Tasks>>(), It.IsAny<List<Items>>(), It.IsAny<List<Meals>>()));
            input = new StringReader("0\nY\ntest\nY\n");
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            fileMock.Verify(user => user.SaveFunction(It.IsAny<string>(), Entries.TASKS, It.IsAny<List<Tasks>>(), It.IsAny<List<Items>>(), It.IsAny<List<Meals>>()), Times.Once);
            ResetReader();
        }
        [Fact]
        public void When_User_Cancelled_Saving_Tasks_Then_It_Should_Stored_In_A_Txt_File()
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            var tasks = new List<Tasks>();
            tasks = _data.SetUpTasks(tasks);
            fileMock.Setup(service => service.SaveFunction(It.IsAny<string>(), Entries.TASKS, It.IsAny<List<Tasks>>(), It.IsAny<List<Items>>(), It.IsAny<List<Meals>>()));
            input = new StringReader("0\nC\n0\nN\n");
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            fileMock.Verify(user => user.SaveFunction(It.IsAny<string>(), Entries.TASKS, It.IsAny<List<Tasks>>(), It.IsAny<List<Items>>(), It.IsAny<List<Meals>>()), Times.Never);
            ResetReader();
        }
        [Fact]
        public void When_User_Select_List_All_Tasks_With_Empty_Tasks_Then_It_Should_Returns_Nothing()
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            taskMock.Setup(service => service.ListAllTasks()).Returns(tasks);
            input = new StringReader("2\n0\nN\n");
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.ListAllTasks(), Times.Once());
            ResetReader();
        }
        [Fact]
        public void When_User_Select_List_Incomplete_Tasks_With_No_Incompletes_Then_It_Should_Returns_List_Of_Incomplete_Tasks()
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            taskMock.Setup(service => service.ListIncompleteTasks()).Returns(tasks);
            input = new StringReader("3\n0\nN\n");
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            taskMock.Verify(user => user.ListIncompleteTasks(), Times.Once);
            ResetReader();
        }
        [Theory]
        [MemberData(nameof(TaskManagerTestData.GetPriorityListInput), MemberType = typeof(TaskManagerTestData))]
        public async void When_User_Select_Task_List_By_Specific_Priority_Then_It_Should_Return_No_Tasks(Priority priority, string userInput)
        {
            // Arrange
            var taskManager = new TaskManager(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, userinput);
            List<Tasks> tasks = new List<Tasks>();
            priorityMock.Setup(service => service.ListTasksByPriority(priority)).Returns(tasks.Where(t => t.Priority == priority).ToList());
            input = new StringReader(userInput);
            Console.SetIn(input);
            // Act
            taskManager.TaskManagerUI();
            // Assert
            priorityMock.Verify(user => user.ListTasksByPriority(priority), Times.Once);
            ResetReader();
        }
        public void ResetReader()
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }


        /*
        [Fact]
        public async void When_Priority_Were_Selected_Then_It_Should_Return_Priority()
        {
            // Arrange
            Priority priority;
            input = new StringReader("M\n");
            Console.SetIn(input);
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
        }*/

    }
}
