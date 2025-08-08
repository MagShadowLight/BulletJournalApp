using BulletJournalApp.Library.Enum;
using BulletJournalApp.Test.UI.Data;
using BulletJournalApp.Test.Util;
using BulletJournalApp.UI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.UI.Util
{
    [Collection("Sequential")]
    public class UserInputTest
    {
        private UserInput _input = new();
        private string prompt = "Test prompt";
        private StringReader _consoleinput;
        private ConsoleInputOutput _console = new();

        [Fact]
        public void When_User_Input_A_String_Then_String_Should_Be_Return()
        {
            // Arrange
            var outputmessage = "Test String";
            // Act
            _consoleinput = new StringReader(outputmessage);
            Console.SetIn(_consoleinput);
            var message = _input.GetStringInput(prompt);
            // Assert
            Assert.Equal(outputmessage, message);
            _console.ResetReader();
        }
        [Fact]
        public void When_User_Input_A_Date_Then_Date_Should_Be_Return()
        {
            // Arrange
            var outputdate = DateTime.Today;
            var inputdate = DateTime.Today.ToString();
            _consoleinput = new StringReader(inputdate);
            Console.SetIn(_consoleinput);
            // Act
            var output = _input.GetDateInput(prompt);
            // Assert
            Assert.Equal(outputdate, output);
            _console.ResetReader();
            Assert.Throws<FormatException>(() =>
            {
                _consoleinput = new StringReader("Test");
                Console.SetIn(_consoleinput);
                var output = _input.GetDateInput(prompt);
            });
        }
        [Theory]
        [MemberData(nameof(UserInputTestData.GetBooleanOutput), MemberType =typeof(UserInputTestData))]
        public void When_User_Input_A_Boolean_Then_Boolean_Should_Be_Return(string input, bool expectedbool)
        {
            // Arrange
            _consoleinput = new StringReader(input);
            Console.SetIn(_consoleinput);
            // Act
            var output = _input.GetBooleanInput(prompt);
            // Assert
            Assert.Equal(expectedbool, output);
            _console.ResetReader();
            Assert.Throws<FormatException>(() =>
            {
                _consoleinput = new StringReader("Test");
                Console.SetIn(_consoleinput);
                var output = _input.GetBooleanInput(prompt);
            });
        }
        [Theory]
        [MemberData(nameof(UserInputTestData.GetPriorityOutput), MemberType = typeof(UserInputTestData))]
        public void When_User_Input_A_Priority_Then_Priority_Should_Be_Return(string input, Priority expectedPriority)
        {
            // Arrange
            _consoleinput = new StringReader(input);
            Console.SetIn(_consoleinput);
            // Act
            var output = _input.GetPriorityInput(prompt);
            // Assert
            Assert.Equal(expectedPriority, output);
            _console.ResetReader();
            Assert.Throws<FormatException>(() =>
            {
                _consoleinput = new StringReader("Test");
                Console.SetIn(_consoleinput);
                var output = _input.GetPriorityInput(prompt);
            });
        }
        [Theory]
        [MemberData(nameof(UserInputTestData.GetCategoryOutput), MemberType = typeof(UserInputTestData))]
        public void When_User_Input_A_Category_Then_Category_Should_Be_Return(string input, Category expectedCategory)
        {
            // Arrange
            _consoleinput = new StringReader(input);
            Console.SetIn(_consoleinput);
            // Act
            var output = _input.GetCategoryInput(prompt);
            // Assert
            Assert.Equal(expectedCategory, output);
            _console.ResetReader();
            Assert.Throws<FormatException>(() =>
            {
                _consoleinput = new StringReader("Test");
                Console.SetIn(_consoleinput);
                var output = _input.GetCategoryInput(prompt);
            });
        }
        [Theory]
        [MemberData(nameof(UserInputTestData.GetScheduleOutput), MemberType = typeof(UserInputTestData))]
        public void When_User_Input_A_Schedule_Then_Schedule_Should_Be_Return(string input, Schedule expectedSchedule)
        {
            // Arrange
            _consoleinput = new StringReader(input);
            Console.SetIn(_consoleinput);
            // Act
            var output = _input.GetScheduleInput(prompt);
            // Assert
            Assert.Equal(expectedSchedule, output);
            _console.ResetReader();
            Assert.Throws<FormatException>(() =>
            {
                _consoleinput = new StringReader("Test");
                Console.SetIn(_consoleinput);
                var output = _input.GetScheduleInput(prompt);
            });
        }
        [Theory]
        [MemberData(nameof(UserInputTestData.GetTaskStatusOutput), MemberType = typeof(UserInputTestData))]
        public void When_User_Input_A_Task_Status_Then_Task_Status_Should_Be_Return(string input, TasksStatus expectedStatus)
        {
            // Arrange
            _consoleinput = new StringReader(input);
            Console.SetIn(_consoleinput);
            // Act
            var output = _input.GetTaskStatusInput(prompt);
            // Assert
            Assert.Equal(expectedStatus, output);
            _console.ResetReader();
            Assert.Throws<FormatException>(() =>
            {
                _consoleinput = new StringReader("Test");
                Console.SetIn(_consoleinput);
                var output = _input.GetTaskStatusInput(prompt);
            });
        }
        [Theory]
        [MemberData(nameof(UserInputTestData.GetItemStatusOutput), MemberType = typeof(UserInputTestData))]
        public void When_User_Input_A_Item_Status_Then_Item_Status_Should_Be_Return(string input, ItemStatus expectedStatus)
        {
            // Arrange
            _consoleinput = new StringReader(input);
            Console.SetIn(_consoleinput);
            // Act
            var output = _input.GetItemStatusInput(prompt);
            // Assert
            Assert.Equal(expectedStatus, output);
            _console.ResetReader();
        }
        [Fact]
        public void When_User_Input_A_Number_Then_Number_Should_Be_Return()
        {
            // Arrange
            _consoleinput = new StringReader("5");
            var expectedNum = 5;
            Console.SetIn(_consoleinput);
            // Act
            var output = _input.GetIntInput(prompt);
            // Assert
            Assert.Equal(expectedNum, output);
            _console.ResetReader();
            Assert.Throws<FormatException>(() =>
            {
                _consoleinput = new StringReader("Test");
                Console.SetIn(_consoleinput);
                var output = _input.GetIntInput(prompt);
            });
        }
        [Theory]
        [MemberData(nameof(UserInputTestData.GetOptionalDateOutput), MemberType = typeof(UserInputTestData))]
        public void When_User_Input_An_Optional_Date_Then_Date_Should_Be_Return(string input, DateTime expectedDate)
        {
            // Arrange
            _consoleinput = new StringReader(input);
            Console.SetIn(_consoleinput);
            // Act
            var output = _input.GetOptionalDateInput(prompt);
            // Assert
            Assert.Equal(expectedDate, output);
            _console.ResetReader();
        }
        [Theory]
        [MemberData(nameof(UserInputTestData.GetTimeOfDayOutput), MemberType = typeof(UserInputTestData))]
        public void When_User_Input_A_Time_Of_Day_Then_Time_Of_Day_Should_Be_Return(string input, TimeOfDay expectedValue)
        {
            // Arrange
            _consoleinput = new StringReader(input);
            Console.SetIn(_consoleinput);
            // Act
            var output = _input.GetTimeOfDayInput(prompt);
            // Assert
            Assert.Equal(expectedValue, output);
            _console.ResetReader();
        }
        [Fact]
        public void When_User_Input_A_Floating_Point_Num_Then_Floating_Point_Num_Should_Be_Return()
        {
            // Arrange
            _consoleinput = new StringReader("5.21");
            var expectedNum = 5.21;
            Console.SetIn(_consoleinput);
            // Act
            var output = _input.GetDoubleInput(prompt);
            // Assert
            Assert.Equal(expectedNum, output);
            _console.ResetReader();
            Assert.Throws<FormatException>(() =>
            {
                _consoleinput = new StringReader("Test");
                Console.SetIn(_consoleinput);
                var output = _input.GetDoubleInput(prompt);
            });
        }
    }
}
