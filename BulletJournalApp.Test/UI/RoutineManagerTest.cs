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
    public class RoutineManagerTest
    {
        private Entries entries = Entries.ROUTINES;
        private Mock<IConsoleLogger> consoleLoggerMock = new();
        private Mock<IFileLogger> fileLoggerMock = new();
        private Mock<IFormatter> formatterMock = new();
        private Mock<IFileService> fileMock = new();
        private Mock<UserInput> inputMock = new();
        private UserInput userInput = new UserInput();
        private Mock<IRoutineService> routineServiceMock = new();
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
            _routines2 = new Routines("Test 1", "Test", Category.Personal, strlist, Periodicity.Monthly, "Test note", 1);
            _routines3 = new Routines("Test 1", "Test", Category.Personal, strlist, Periodicity.Monthly, "Test note", 2);
        }


        [Fact]
        public void Given_There_Are_No_Routines_In_The_List_When_User_Selected_To_Add_Routine_Then_It_Should_Be_Added()
        {
            // Arrange
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
        [MemberData(nameof(RoutineManagerTestData.GetLogsVerificationForAddingRoutineWithSuccess), MemberType =typeof(RoutineManagerTestData))]
        public void Given_There_Are_No_Routines_In_The_List_When_User_Selected_To_Add_Routine_Then_Logger_Should_Be_Logged_That_User_Is_Adding(string logVerify)
        {
            // Arrange
            var ui = new RoutineManager(routineServiceMock.Object, categoryServiceMock.Object, periodicityServiceMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            _input = new StringReader("1\nTest 1\nTest\nP\nTest note\nM\n1\nTest Task 1\n1\nTest Task 2\n1\nTest Task 3\n0\n0");
            Console.SetIn(_input);
            // Act
            ui.RoutineUI();
            // Assert
            fileLoggerMock.Verify(log => log.Log(logVerify), Times.AtLeastOnce);
        }
        [Theory]
        [MemberData(nameof(RoutineManagerTestData.GetErrorVerificationForAdding), MemberType = typeof(RoutineManagerTestData))]
        public void Given_There_Are_No_Routines_In_The_List_When_User_Selected_To_Add_Routine_Then_It_Should_Display_That_It_Was_Failed_To_Add(string input, string errorverify)
        {
            // Arrange
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

    }
}
