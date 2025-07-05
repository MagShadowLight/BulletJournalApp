using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
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
    public class ShopListManagerTest
    {
        private Mock<IConsoleLogger> consoleLoggerMock = new();
        private Mock<IFileLogger> fileLoggerMock = new();
        private Mock<IFormatter> formatterMock = new();
        private Mock<IPriorityService> priorityMock = new();
        private Mock<ICategoryService> categoryMock = new();
        private Mock<IScheduleService> scheduleMock = new();
        private Mock<IItemStatusService> statusMock = new();
        private Mock<IFileService> fileMock = new();
        private Mock<IItemService> itemMock = new();
        private Mock<UserInput> inputMock = new();
        private UserInput UserInput = new UserInput();

        [Fact]
        public async void When_User_Selected_To_Add_Items_Then_Item_Should_Be_Added()
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            // Act
            using var input = new StringReader("1\nTest\nTest\nM\nH\nTest\n0");
            Console.SetIn(input);
            ui.UI();
            // Assert
            itemMock.Verify(user => user.AddItems(It.Is<Items>(item => item.Name == "Test" && item.Description == "Test" && item.Schedule == Schedule.Monthly)), Times.Once);
            ResetReader();
        }
        [Fact]
        public async void When_User_Selected_To_List_All_Items_Then_It_Should_Return_Lists_Of_Items()
        {
            // Arrange
            var mockService = new ItemService(new ConsoleLogger(), new FileLogger());
            var item1 = new Items("Test", "Test", Schedule.Monthly);
            var item2 = new Items("Test2", "Test", Schedule.Monthly);
            var item3 = new Items("Test3", "Test", Schedule.Monthly);
            mockService.AddItems(item1);
            mockService.AddItems(item2);
            mockService.AddItems(item3);
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);

        }
        public void ResetReader()
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
    }
}
