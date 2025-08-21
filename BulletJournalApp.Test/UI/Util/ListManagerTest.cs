using BulletJournalApp.Core.Interface;
using BulletJournalApp.Test.UI.Data;
using BulletJournalApp.UI.Util;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.UI.Util
{
    [Collection("Sequential")]
    public class ListManagerTest
    {
        private Mock<IFileLogger> fileLoggerMock = new();
        private Mock<IConsoleLogger> consoleLoggerMock = new();
        private StringReader _input;
        [Fact]
        public void Given_String_List_Manager_Is_Opened_When_User_Entered_Invalid_Value_Then_It_Should_Display_Error()
        {
            // Arrange
            var listmanager = new ListManager(fileLoggerMock.Object, consoleLoggerMock.Object);
            _input = new StringReader("Meow\n0");
            Console.SetIn(_input);
            // Act
            listmanager.StringListManager();
            // Assert
            fileLoggerMock.Verify(user => user.Error("Invalid choice. Try again."), Times.Once);
            consoleLoggerMock.Verify(user => user.Error("Invalid choice. Try again."), Times.Once);
        }
        [Fact]
        public void Given_String_List_Manager_Is_Opened_When_User_Entered_Empty_String_When_Adding_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var listmanager = new ListManager(fileLoggerMock.Object, consoleLoggerMock.Object);
            _input = new StringReader("1\n\n0");
            Console.SetIn(_input);
            // Act
            listmanager.StringListManager();
            // Assert
            fileLoggerMock.Verify(user => user.Error("Value cannot be null. (Parameter 'str must not be null or empty string')"), Times.Once);
            consoleLoggerMock.Verify(user => user.Error("Value cannot be null. (Parameter 'str must not be null or empty string')"), Times.Once);
        }
        [Theory]
        [MemberData(nameof(ListManagerTestData.GetInputForEdit), MemberType =typeof(ListManagerTestData))]
        public void Given_String_List_Manager_Is_Opened_When_User_Entered_Empty_String_When_Editing_Then_It_Should_Throw_Exception(string input, string message)
        {
            // Arrange
            var listmanager = new ListManager(fileLoggerMock.Object, consoleLoggerMock.Object);
            _input = new StringReader(input);
            Console.SetIn(_input);
            // Act
            listmanager.StringListManager();
            // Assert
            fileLoggerMock.Verify(user => user.Error(message), Times.Once);
            consoleLoggerMock.Verify(user => user.Error(message), Times.Once);
        }
        [Fact]
        public void Given_String_List_Manager_Is_Opened_When_User_Entered_Empty_String_When_Deleting_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var listmanager = new ListManager(fileLoggerMock.Object, consoleLoggerMock.Object);
            _input = new StringReader("3\n\n0");
            Console.SetIn(_input);
            // Act
            listmanager.StringListManager();
            // Assert
            fileLoggerMock.Verify(user => user.Error("Value cannot be null. (Parameter 'str must not be null or empty string')"), Times.Once);
            consoleLoggerMock.Verify(user => user.Error("Value cannot be null. (Parameter 'str must not be null or empty string')"), Times.Once);
        }
    }
}
