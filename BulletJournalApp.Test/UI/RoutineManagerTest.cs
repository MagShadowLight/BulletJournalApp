using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Test.UI.Data;
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
    public class RoutineManagerTest
    {
        private Entries entries = Entries.ROUTINES;
        private Mock<IConsoleLogger> consoleLoggerMock = new();
        private Mock<IFileLogger> fileLoggerMock = new();
        private Mock<IFormatter> formatterMock = new();
        private Mock<IFileService> fileMock = new();
        private Mock<UserInput> inputMock = new();
        private UserInput userInput = new UserInput();
        private Mock<IRoutineService> routineServiceMock;
        private Mock<ICategoryService> categoryServiceMock = new();
        private Mock<IPeriodicityService> periodicityServiceMock = new();
        private ConsoleInputOutput stream = new();
        private List<Routines> routines = new();
        private StringReader _input;
        private RoutineManagerTestData _data = new();
        private Routines _routines1;
        private Routines _routines2;
        private Routines _routines3;
        private List<string> strlist = new();

        public RoutineManagerTest()
        {
            _data.SetUpStringList(strlist);
            _routines1 = new Routines("Test 1", "Test", Category.Personal, strlist, Periodicity.Monthly, "Test note", 0);
            _routines2 = new Routines("Test 2", "Test", Category.None, strlist, Periodicity.Weekly, "Test note", 1);
            _routines3 = new Routines("Test 3", "Test", Category.Financial, strlist, Periodicity.Yearly, "Test note", 2);
        }


        [Fact]
        public void Given_There_Are_No_Routines_In_The_List_When_User_Selected_To_Add_Routine_Then_It_Should_Be_Added()
        {
            // Arrange
            routineServiceMock = new();
            var ui = new RoutineManager(routineServiceMock.Object, categoryServiceMock.Object, periodicityServiceMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            _input = new StringReader("1\nTest 1\nTest\nP\nTest note\nM\n1\nTest Task 1\n1\nTest Task 2\n1\nTest Task 3\n0\n0");
            Console.SetIn(_input);
            // Act
            ui.RoutineUI();
            // Assert
            routineServiceMock.Verify(user => user.AddRoutine(It.Is<Routines>(
                routine => routine.Name == "Test 1"
                && routine.Description == "Test"
                && routine.Category == Category.Personal
                && routine.Notes == "Test note"
                && routine.Periodicity == Periodicity.Monthly
                && routine.TaskList.Count == 3)),
                Times.Once);
        }
        [Theory]
        [MemberData(nameof(RoutineManagerTestData.GetLogsVerification), MemberType =typeof(RoutineManagerTestData))]
        public void Given_Routine_Manager_Were_Opened_When_User_Are_Selecting_Variant_Of_Options_Then_Logger_Should_Be_Logged_That_User_Is_Doing_Something(int id, string input, string logVerify)
        {
            // Arrange
            routineServiceMock = new();
            var ui = new RoutineManager(routineServiceMock.Object, categoryServiceMock.Object, periodicityServiceMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            _input = new StringReader(input);
            Console.SetIn(_input);
            // Act
            ui.RoutineUI();
            // Assert
            fileLoggerMock.Verify(log => log.Log(logVerify), Times.AtLeastOnce);
        }
        [Theory]
        [MemberData(nameof(RoutineManagerTestData.GetErrorVerificationForAdding), MemberType = typeof(RoutineManagerTestData))]
        public void Given_There_Are_No_Routines_In_The_List_When_User_Selected_To_Add_Routine_Then_It_Should_Display_That_It_Was_Failed_To_Add(int id, string input, string errorverify)
        {
            // Arrange
            routineServiceMock = new();
            var ui = new RoutineManager(routineServiceMock.Object, categoryServiceMock.Object, periodicityServiceMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            _input = new StringReader(input);
            Console.SetIn(_input);
            // Act
            ui.RoutineUI();
            // Assert
            fileLoggerMock.Verify(log => log.Error(errorverify), Times.Once);
            consoleLoggerMock.Verify(log => log.Error(errorverify), Times.Once);
        }
        [Fact]
        public void Given_There_Are_Routines_In_The_List_When_User_Selected_To_List_All_Routines_Then_It_Should_Get_All_Routines_And_Display_It_To_User()
        {
            // Arrange
            routineServiceMock = new();
            _data.GetAllRoutines(routines, _routines1, _routines2, _routines3);
            var ui = new RoutineManager(routineServiceMock.Object, categoryServiceMock.Object, periodicityServiceMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            routineServiceMock.Setup(s => s.GetAllRoutines()).Returns(routines);
            _input = new StringReader("2\n0");
            Console.SetIn(_input);
            // Act
            ui.RoutineUI();
            // Assert
            routineServiceMock.Verify(user => user.GetAllRoutines(), Times.Once);
        }
        [Theory]
        [MemberData(nameof(RoutineManagerTestData.GetValueForError), MemberType =typeof(RoutineManagerTestData))]
        public void Given_Routine_Manager_Were_Opened_When_User_Selected_Invalid_Values_Or_Choices_Then_It_Should_Display_Error(int id, string input, string message)
        {
            // Arrange
            routineServiceMock = new();
            var ui = new RoutineManager(routineServiceMock.Object, categoryServiceMock.Object, periodicityServiceMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            _input = new StringReader(input);
            Console.SetIn(_input);
            // Act
            ui.RoutineUI();
            // Assert
            consoleLoggerMock.Verify(user => user.Error(message), Times.Once);
            fileLoggerMock.Verify(user => user.Error(message), Times.Once);
        }
        [Fact]
        public void Given_There_Are_Routines_In_The_List_When_User_Selected_To_List_All_Routines_Then_UI_Should_Display_The_List()
        {
            // Arrange
            routineServiceMock = new();
            _data.GetAllRoutines(routines, _routines1, _routines2, _routines3);
            var ui = new RoutineManager(routineServiceMock.Object, categoryServiceMock.Object, periodicityServiceMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            routineServiceMock.Setup(service => service.GetAllRoutines()).Returns(routines);
            _input = new StringReader("2\n0");
            Console.SetIn(_input);
            // Act
            ui.RoutineUI();
            // Assert
            routineServiceMock.Verify(user => user.GetAllRoutines(), Times.Once);
        }
        [Fact]
        public void Given_There_Are_Routines_In_The_List_When_User_Selected_To_List_Routines_By_Category_Then_UI_Should_Display_The_List()
        {
            // Arrange
            var category = _routines1.Category;
            routineServiceMock = new();
            routines.Add(_routines1);
            var ui = new RoutineManager(routineServiceMock.Object, categoryServiceMock.Object, periodicityServiceMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            categoryServiceMock.Setup(service => service.ListRoutinesByCategory(category)).Returns(routines);
            _input = new StringReader("3\nC\nP\n0");
            Console.SetIn(_input);
            // Act
            ui.RoutineUI();
            // Assert
            categoryServiceMock.Verify(user => user.ListRoutinesByCategory(category), Times.Once);
        }
        [Fact]
        public void Given_There_Are_Routines_In_The_List_When_User_Selected_To_List_Routines_By_Periodicity_Then_UI_Should_Display_The_List()
        {
            // Arrange
            var periodicity = _routines1.Periodicity;
            routineServiceMock = new();
            routines.Add(_routines1);
            var ui = new RoutineManager(routineServiceMock.Object, categoryServiceMock.Object, periodicityServiceMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            periodicityServiceMock.Setup(service => service.ListRoutinesByPeriodicity(periodicity)).Returns(routines);
            _input = new StringReader("3\nS\nM\n0");
            Console.SetIn(_input);
            // Act
            ui.RoutineUI();
            // Assert
            periodicityServiceMock.Verify(user => user.ListRoutinesByPeriodicity(periodicity), Times.Once);
        }
        [Fact]
        public void Given_There_Are_Routines_In_The_List_When_User_Selected_To_Search_Routines_Then_UI_Should_Display_That_Routine()
        {
            // Arrange
            routineServiceMock = new();
            var ui = new RoutineManager(routineServiceMock.Object, categoryServiceMock.Object, periodicityServiceMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            routineServiceMock.Setup(service => service.FindRoutineByName(_routines1.Name)).Returns(_routines1);
            _input = new StringReader("4\nTest 1\n0");
            Console.SetIn(_input);
            // Act
            ui.RoutineUI();
            // Assert
            routineServiceMock.Verify(user => user.FindRoutineByName(_routines1.Name), Times.Once);
        }
        [Fact]
        public void Given_There_Are_Routines_In_The_List_When_User_Selected_To_Update_Routine_With_New_Name_Description_And_Note_Then_It_Should_Be_Updated_With_New_Values()
        {
            // Arrange
            routineServiceMock = new();
            var ui = new RoutineManager(routineServiceMock.Object, categoryServiceMock.Object, periodicityServiceMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            routineServiceMock.Setup(service => service.UpdateRoutine(_routines2.Name, "Updated Test", "Update", "Meow"));
            _input = new StringReader("5\n1\nTest 2\nUpdated Test\nUpdate\nMeow\n0");
            Console.SetIn(_input);
            // Act
            ui.RoutineUI();
            // Assert
            routineServiceMock.Verify(user => user.UpdateRoutine(_routines2.Name, "Updated Test", "Update", "Meow"), Times.Once);
        }
        [Fact]
        public void Given_There_Are_Routines_In_The_List_When_User_Selected_To_Update_Routine_With_New_Category_Then_It_Should_Be_Updated_With_New_Values()
        {
            // Arrange
            var category = Category.Home;
            routineServiceMock = new();
            var ui = new RoutineManager(routineServiceMock.Object, categoryServiceMock.Object, periodicityServiceMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            categoryServiceMock.Setup(service => service.ChangeCategory(_routines2.Name, entries, category));
            _input = new StringReader("5\n2\nTest 2\nH\n0");
            Console.SetIn(_input);
            // Act
            ui.RoutineUI();
            // Assert
            categoryServiceMock.Verify(user => user.ChangeCategory(_routines2.Name, entries, category), Times.Once);
        }
        [Fact]
        public void Given_There_Are_Routines_In_The_List_When_User_Selected_To_Update_Routine_With_New_Periodicity_Then_It_Should_Be_Updated_With_New_Values()
        {
            // Arrange
            var periodicity = Periodicity.Quarterly;
            routineServiceMock = new();
            var ui = new RoutineManager(routineServiceMock.Object, categoryServiceMock.Object, periodicityServiceMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            periodicityServiceMock.Setup(service => service.ChangeSchedule(_routines2.Name, entries, periodicity));
            _input = new StringReader("5\n3\nTest 2\nQ\n0");
            Console.SetIn(_input);
            // Act
            ui.RoutineUI();
            // Assert
            periodicityServiceMock.Verify(user => user.ChangeSchedule(_routines2.Name, entries, periodicity), Times.Once);
        }
        [Fact]
        public void Given_There_Are_Routines_In_The_List_When_User_Selected_To_Update_Routine_With_New_Task_List_Then_It_Should_Be_Updated_With_New_Values()
        {
            // Arrange
            var templist = new List<string> { "Meow mrow mrrp" };
            routineServiceMock = new();
            var ui = new RoutineManager(routineServiceMock.Object, categoryServiceMock.Object, periodicityServiceMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            routineServiceMock.Setup(service => service.ChangeTaskList(_routines2.Name, templist));
            _input = new StringReader("5\n4\nTest 2\n1\nMeow mrow mrrp\n0\n0");
            Console.SetIn(_input);
            // Act
            ui.RoutineUI();
            // Assert
            routineServiceMock.Verify(user => user.ChangeTaskList(_routines2.Name, templist), Times.Once);
        }
        [Fact]
        public void Given_There_Are_Routines_In_The_List_When_User_Selected_To_Delete_Routines_Then_It_Should_Be_Removed_From_The_List()
        {
            // Arrange
            routineServiceMock = new();
            var ui = new RoutineManager(routineServiceMock.Object, categoryServiceMock.Object, periodicityServiceMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            routineServiceMock.Setup(service => service.DeleteRoutine(_routines2.Name));
            _input = new StringReader("6\nTest 2\n0");
            Console.SetIn(_input);
            // Act
            ui.RoutineUI();
            // Assert
            routineServiceMock.Verify(user => user.DeleteRoutine(_routines2.Name), Times.Once);
        }
    }
}
