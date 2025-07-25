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
        private Entries entries = Entries.ITEMS;
        private Mock<IConsoleLogger> consoleLoggerMock = new();
        private Mock<IFileLogger> fileLoggerMock = new();
        private Mock<IFormatter> formatterMock = new();
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
            using var input = new StringReader("1\nTest\nTest\n1\nM\nH\nTest\n0");
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
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            var items = new List<Items>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            // Act
            itemMock.Setup(service => service.GetAllItems()).Returns(items);
            using var input = new StringReader("2\n0\n");
            Console.SetIn(input);
            ui.UI();
            // Assert
            itemMock.Verify(user => user.GetAllItems(), Times.Once());
            ResetReader();
        }
        [Fact]
        public void When_User_Selected_To_List_All_Owned_Items_Then_It_Should_Returns()
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            item2.MarkAsBought();
            var items = new List<Items>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            // Act
            itemMock.Setup(service => service.GetItemsOwned()).Returns(items);
            using var input = new StringReader("3\n0\n");
            Console.SetIn(input);
            ui.UI();
            // Assert
            itemMock.Verify(user => user.GetItemsOwned(), Times.Once());
            ResetReader();
        }
        [Fact]
        public void When_User_Selected_To_List_All_Unowned_Items_Then_It_Should_Returns()
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            item2.MarkAsBought();
            var items = new List<Items>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            // Act
            itemMock.Setup(service => service.GetItemsNotOwned()).Returns(items);
            using var input = new StringReader("4\n0\n");
            Console.SetIn(input);
            ui.UI();
            // Assert
            itemMock.Verify(user => user.GetItemsNotOwned(), Times.Once());
            ResetReader();
        }
        [Fact]
        public void When_User_Selected_To_List_All_Items_By_Schedule_Then_It_Should_Returns()
        {
            // Arrange
            var schedule = Schedule.Daily;
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Daily, 1);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            var items = new List<Items>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            // Act
            scheduleMock.Setup(service => service.ListItemsBySchedule(schedule)).Returns(items);
            using var input = new StringReader("5\nSc\nD\n0\n");
            Console.SetIn(input);
            ui.UI();
            // Assert
            scheduleMock.Verify(user => user.ListItemsBySchedule(schedule), Times.Once());
            ResetReader();
        }
        [Fact]
        public void When_User_Selected_To_List_All_Items_By_Status_Then_It_Should_Returns()
        {
            // Arrange
            var status = ItemStatus.Delayed;
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1, 0, Category.None, ItemStatus.Delayed);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            var items = new List<Items>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            // Act
            statusMock.Setup(service => service.ListItemsByStatus(status)).Returns(items);
            using var input = new StringReader("5\nSt\nD\n0\n");
            Console.SetIn(input);
            ui.UI();
            // Assert
            statusMock.Verify(user => user.ListItemsByStatus(status), Times.Once());
            ResetReader();
        }
        [Fact]
        public void When_User_Selected_To_List_All_Items_By_Category_Then_It_Should_Returns()
        {
            // Arrange
            var category = Category.Financial;
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1, 0, Category.Financial);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            var items = new List<Items>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            // Act
            categoryMock.Setup(service => service.ListItemsByCategory(category)).Returns(items);
            using var input = new StringReader("5\nC\nF\n0\n");
            Console.SetIn(input);
            ui.UI();
            // Assert
            categoryMock.Verify(user => user.ListItemsByCategory(category), Times.Once());
            ResetReader();
        }
        [Fact]
        public void When_User_Selected_To_Find_The_Title_Then_It_Should_Return_With_Just_That_Title()
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1, 0, Category.Financial);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            var items = new List<Items>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            // Act
            itemMock.Setup(service => service.FindItemsByName(item2.Name)).Returns(item2);
            using var input = new StringReader("6\nTest2\n0");
            Console.SetIn(input);
            ui.UI();
            // Assert
            itemMock.Verify(user => user.FindItemsByName(item2.Name), Times.Once());
            ResetReader();
        }
        [Fact]
        public void When_User_Selected_Mark_Item_As_Bought_Then_Item_Should_Be_Marked()
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1, 0, Category.Financial);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            var items = new List<Items>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            // Act
            itemMock.Setup(service => service.MarkItemsAsBought(item2.Name));
            using var input = new StringReader("7\nTest2\n0");
            Console.SetIn(input);
            ui.UI();
            // Assert
            itemMock.Verify(user => user.MarkItemsAsBought(item2.Name), Times.Once());
            ResetReader();
        }
        [Fact]
        public void When_User_Selected_Update_Item_Then_Items_Should_Update_With_New_Info()
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1, 0, Category.Financial);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            var items = new List<Items>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            // Act
            itemMock.Setup(service => service.UpdateItems("Test2", "Updated Test", "new desc", "New Note", 1));
            using var input = new StringReader("8\nTest2\nUpdated Test\nnew desc\nNew Note\n1\n0");
            Console.SetIn(input);
            ui.UI();
            // Assert
            itemMock.Verify(user => user.UpdateItems("Test2", "Updated Test", "new desc", "New Note", 1), Times.Once);
            ResetReader();
        }
        [Fact]
        public void When_User_Selected_Change_Item_Schedule_Then_Items_Should_Update_With_New_Schedule()
        {
            // Arrange
            var schedule = Schedule.Weekly;
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            var items = new List<Items>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            // Act
            scheduleMock.Setup(service => service.ChangeSchedule("Test2", entries, schedule));
            using var input = new StringReader("9\nSc\nTest2\nW\n0");
            Console.SetIn(input);
            ui.UI();
            // Assert
            scheduleMock.Verify(user => user.ChangeSchedule("Test2", entries, schedule), Times.Once);
            ResetReader();
        }
        [Fact]
        public void When_User_Selected_Change_Item_Status_Then_Items_Should_Update_With_New_Status()
        {
            // Arrange
            var status = ItemStatus.Delayed;
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            var items = new List<Items>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            // Act
            statusMock.Setup(service => service.ChangeStatus("Test2", entries, status));
            using var input = new StringReader("9\nSt\nTest2\nD\n0");
            Console.SetIn(input);
            ui.UI();
            // Assert
            statusMock.Verify(user => user.ChangeStatus("Test2", entries, status), Times.Once);
            ResetReader();
        }
        [Fact]
        public void When_User_Selected_Change_Item_Category_Then_Items_Should_Update_With_New_Category()
        {
            // Arrange
            var category = Category.Financial;
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            var items = new List<Items>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            // Act
            categoryMock.Setup(service => service.ChangeCategory("Test2", entries, category));
            using var input = new StringReader("9\nC\nTest2\nF\n0");
            Console.SetIn(input);
            ui.UI();
            // Assert
            categoryMock.Verify(user => user.ChangeCategory("Test2", entries, category), Times.Once);
            ResetReader();
        }
        [Fact]
        public void When_User_Selected_Delete_Item_Then_Items_Should_be_Deleted()
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            var items = new List<Items>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            // Act
            itemMock.Setup(service => service.DeleteItems("Test2"));
            using var input = new StringReader("10\nTest2\n0");
            Console.SetIn(input);
            ui.UI();
            // Assert
            itemMock.Verify(user => user.DeleteItems("Test2"), Times.Once);
            ResetReader();
        }
        [Fact]
        public void When_User_Selected_To_Save_Items_List_Then_Items_Should_Be_Saved()
        {
            // Arrange
            var shopListManager = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            var items = new List<Items>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            fileMock.Setup(service => service.SaveFunction(It.IsAny<string>(), Entries.ITEMS, It.IsAny<List<Tasks>>(), It.IsAny<List<Items>>(), It.IsAny<List<Meals>>()));
            // Act
            using var input = new StringReader("11\n1\nFakeItem\n0");
            Console.SetIn(input);
            shopListManager.UI();
            // Assert
            fileMock.Verify(user => user.SaveFunction(It.IsAny<string>(), Entries.ITEMS, It.IsAny<List<Tasks>>(), It.IsAny<List<Items>>(), It.IsAny<List<Meals>>()), Times.Once);
            ResetReader();
        }
        [Fact]
        public void When_User_Selected_To_Load_Items_Then_Items_Should_Be_Loaded()
        {
            // Assert
            var taskMock = new Mock<ITaskService>();
            var shopListManager = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, UserInput);
            var meal = new Mock<IMealService>();
            var fileService = new FileService(new Formatter(), new ConsoleLogger(), new FileLogger(), taskMock.Object, new ItemService(consoleLoggerMock.Object, fileLoggerMock.Object), meal.Object);
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            var items = new List<Items>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            fileService.SaveFunction("Test", Entries.ITEMS, null, items, null);
            fileMock.Setup(service => service.LoadFunction(It.IsAny<string>(), Entries.ITEMS));
            // Act
            using var input = new StringReader("11\n2\nTest\n0");
            Console.SetIn(input);
            shopListManager.UI();
            // Assert
            fileMock.Verify(user => user.LoadFunction(It.IsAny<string>(), Entries.ITEMS), Times.Once);
        }
        public void ResetReader()
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
    }
}
