using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Services;
using BulletJournalApp.Test.Util;
using BulletJournalApp.UI;
using BulletJournalApp.UI.Util;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.UI
{
    [Collection("Sequential")]
    public class ConsoleUITest
    {
        private Mock<IConsoleLogger> consoleLoggerMock = new();
        private Mock<IFileLogger> fileLoggerMock = new();
        private Mock<IFormatter> formatterMock = new();
        private Mock<IPriorityService> priorityMock = new();
        private Mock<ICategoryService> categoryMock = new();
        private Mock<IPeriodicityService> scheduleMock = new();
        private Mock<ITasksStatusService> taskstatusmock = new();
        private Mock<IItemStatusService> itemStatusMock = new();
        private Mock<IFileService> fileMock = new();
        private Mock<ITaskService> taskMock = new();
        private Mock<IItemService> itemMock = new();
        private Mock<IMealService> mealMock = new();
        private Mock<IRoutineService> routineMock = new();
        private Mock<IIngredientService> ingredientMock = new();
        private Mock<IUserInput> _inputMock = new();
        private Mock<ITimeOfDayService> timeOfDayMock = new();
        private TaskManager _taskManagerMock;
        private ShopListManager _shopListManagerMock;
        private MealPlanManager _mealPlanManagerMock;
        private RoutineManager _routineManagerMock;
        private StringReader _input;
        private ConsoleInputOutput _console = new();

        public ConsoleUITest()
        {
            _taskManagerMock = new(taskMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, taskstatusmock.Object, fileMock.Object, scheduleMock.Object, priorityMock.Object, categoryMock.Object, _inputMock.Object);
            _shopListManagerMock = new(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, itemStatusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, _inputMock.Object);
            _mealPlanManagerMock = new(mealMock.Object, ingredientMock.Object, timeOfDayMock.Object, _inputMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            _routineManagerMock = new(routineMock.Object, categoryMock.Object, scheduleMock.Object, _inputMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
        }

        [Fact]
        public void When_User_Select_Tasks_Manager_Then_Task_Manager_UI_Should_Open()
        {
            // Arrange
            var ui = new ConsoleUI(_taskManagerMock, fileLoggerMock.Object, consoleLoggerMock.Object, _shopListManagerMock, _mealPlanManagerMock, _routineManagerMock);
            _input = new StringReader("1\n0\nN\n0");
            Console.SetIn(_input);
            // Act
            ui.Run();
            // Assert
            fileLoggerMock.Verify(logger => logger.Log("Opening Task Manager"));
            fileLoggerMock.Verify(logger => logger.Log("Task manager closed"));
            _console.ResetReader();
        }
        [Fact]
        public void When_User_Select_Shopping_List_Manager_Then_Shopping_List_Manager_UI_Should_Open()
        {
            // Arrange
            var ui = new ConsoleUI(_taskManagerMock, fileLoggerMock.Object, consoleLoggerMock.Object, _shopListManagerMock, _mealPlanManagerMock, _routineManagerMock);
            _input = new StringReader("2\n0\n0");
            Console.SetIn(_input);
            // Act
            ui.Run();
            // Assert
            fileLoggerMock.Verify(logger => logger.Log("Opening Shopping List Manager"));
            fileLoggerMock.Verify(logger => logger.Log("Shopping List Manager Closed"));
            _console.ResetReader();
        }
        [Fact]
        public void When_User_Select_Meal_Plan_Manager_Then_Meal_Plan_Manager_UI_Should_Open()
        {
            // Arrange
            var ui = new ConsoleUI(_taskManagerMock, fileLoggerMock.Object, consoleLoggerMock.Object, _shopListManagerMock, _mealPlanManagerMock, _routineManagerMock);
            _input = new StringReader("3\n0\n0");
            Console.SetIn(_input);
            // Act
            ui.Run();
            // Assert
            fileLoggerMock.Verify(logger => logger.Log("Opening Meal Plan Manager"));
            fileLoggerMock.Verify(logger => logger.Log("Meal Plan Manager Closed"));
            _console.ResetReader();
        }
        [Fact]
        public void When_User_Select_Invalid_Choices_Then_It_Should_Throw_An_Error()
        {
            // Arrange
            var ui = new ConsoleUI(_taskManagerMock, fileLoggerMock.Object, consoleLoggerMock.Object, _shopListManagerMock, _mealPlanManagerMock, _routineManagerMock);
            _input = new StringReader("Test\n0");
            Console.SetIn(_input);
            // Act
            ui.Run();
            // Assert
            fileLoggerMock.Verify(logger => logger.Error("Invalid Choice. Please Try Again"));
            _console.ResetReader();
        }
    }
}
