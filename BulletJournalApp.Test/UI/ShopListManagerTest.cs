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
        private StringReader input;
        private ConsoleInputOutput _console = new();
        private ShopListManagerTestData _data = new();

        [Fact]
        public void When_User_Selected_To_Add_Items_Then_Item_Should_Be_Added()
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            input = new StringReader("1\nTest\nTest\n1\nM\nH\nTest\n0");
            Console.SetIn(input);
            // Act
            ui.UI();
            // Assert
            itemMock.Verify(user => user.AddItems(It.Is<Items>(item => item.Name == "Test" && item.Description == "Test" && item.Schedule == Schedule.Monthly)), Times.Once);
            _console.ResetReader();
        }
        [Fact]
        public void When_User_Selected_To_List_All_Items_Then_It_Should_Return_Lists_Of_Items()
        {
            // Arrange
            var items = new List<Items>();
            items = _data.SetUpItems(items);
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            itemMock.Setup(service => service.GetAllItems()).Returns(items);
            input = new StringReader("2\n0\n");
            Console.SetIn(input);
            // Act
            ui.UI();
            // Assert
            itemMock.Verify(user => user.GetAllItems(), Times.Once());
            _console.ResetReader();
        }
        [Fact]
        public void When_User_Selected_To_List_All_Owned_Items_Then_It_Should_Returns()
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var items = new List<Items>();
            items = _data.SetUpItems(items);
            itemMock.Setup(service => service.GetItemsOwned()).Returns(items);
            input = new StringReader("3\n0\n");
            Console.SetIn(input);
            // Act
            ui.UI();
            // Assert
            itemMock.Verify(user => user.GetItemsOwned(), Times.Once());
            _console.ResetReader();
        }
        [Fact]
        public void When_User_Selected_To_List_All_Unowned_Items_Then_It_Should_Returns()
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var items = new List<Items>();
            items = _data.SetUpItems(items);
            itemMock.Setup(service => service.GetItemsNotOwned()).Returns(items);
            input = new StringReader("4\n0\n");
            Console.SetIn(input);
            // Act
            ui.UI();
            // Assert
            itemMock.Verify(user => user.GetItemsNotOwned(), Times.Once());
            _console.ResetReader();
        }
        [Theory]
        [MemberData(nameof(ShopListManagerTestData.GetScheduleListInput), MemberType =typeof(ShopListManagerTestData))]
        public void When_User_Selected_To_List_All_Items_By_Schedule_Then_It_Should_Returns(Schedule schedule, string userinput)
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var items = new List<Items>();
            items = _data.SetUpItems(items);
            scheduleMock.Setup(service => service.ListItemsBySchedule(schedule)).Returns(items);
            input = new StringReader(userinput);
            Console.SetIn(input);
            // Act
            ui.UI();
            // Assert
            scheduleMock.Verify(user => user.ListItemsBySchedule(schedule), Times.Once());
            _console.ResetReader();
        }
        [Theory]
        [MemberData(nameof(ShopListManagerTestData.GetStatusListInput), MemberType = typeof(ShopListManagerTestData))]
        public void When_User_Selected_To_List_All_Items_By_Status_Then_It_Should_Returns(ItemStatus status, string userinput)
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var items = new List<Items>();
            items = _data.SetUpItems(items);
            statusMock.Setup(service => service.ListItemsByStatus(status)).Returns(items);
            input = new StringReader(userinput);
            Console.SetIn(input);
            // Act
            ui.UI();
            // Assert
            statusMock.Verify(user => user.ListItemsByStatus(status), Times.Once());
            _console.ResetReader();
        }
        [Theory]
        [MemberData(nameof(ShopListManagerTestData.GetCategoryListInput), MemberType = typeof(ShopListManagerTestData))]
        public void When_User_Selected_To_List_All_Items_By_Category_Then_It_Should_Returns(Category category, string userinput)
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var items = new List<Items>();
            items = _data.SetUpItems(items);
            categoryMock.Setup(service => service.ListItemsByCategory(category)).Returns(items);
            input = new StringReader(userinput);
            Console.SetIn(input);
            // Act
            ui.UI();
            // Assert
            categoryMock.Verify(user => user.ListItemsByCategory(category), Times.Once());
            _console.ResetReader();
        }
        [Fact]
        public void When_User_Selected_To_Find_The_Title_Then_It_Should_Return_With_Just_That_Title()
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var items = new List<Items>();
            items = _data.SetUpItems(items);
            var item2 = items[1];
            itemMock.Setup(service => service.FindItemsByName(item2.Name)).Returns(item2);
            input = new StringReader("6\nTest 2\n0");
            Console.SetIn(input);
            // Act
            ui.UI();
            // Assert
            itemMock.Verify(user => user.FindItemsByName(item2.Name), Times.Once());
            _console.ResetReader();
        }
        [Fact]
        public void When_User_Selected_Mark_Item_As_Bought_Then_Item_Should_Be_Marked()
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var items = new List<Items>();
            items = _data.SetUpItems(items);
            var item2 = items[1];
            itemMock.Setup(service => service.MarkItemsAsBought(item2.Name));
            input = new StringReader("7\nTest 2\n0");
            Console.SetIn(input);
            // Act
            ui.UI();
            // Assert
            itemMock.Verify(user => user.MarkItemsAsBought(item2.Name), Times.Once());
            _console.ResetReader();
        }
        [Fact]
        public void When_User_Selected_Update_Item_Then_Items_Should_Update_With_New_Info()
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var items = new List<Items>();
            items = _data.SetUpItems(items);
            itemMock.Setup(service => service.UpdateItems("Test 2", "Updated Test", "new desc", "New Note", 1));
            input = new StringReader("8\nTest 2\nUpdated Test\nnew desc\nNew Note\n1\n0");
            Console.SetIn(input);
            // Act
            ui.UI();
            // Assert
            itemMock.Verify(user => user.UpdateItems("Test 2", "Updated Test", "new desc", "New Note", 1), Times.Once);
            _console.ResetReader();
        }
        [Theory]
        [MemberData(nameof(ShopListManagerTestData.GetScheduleUpdateInput), MemberType = typeof(ShopListManagerTestData))]
        public void When_User_Selected_Change_Item_Schedule_Then_Items_Should_Update_With_New_Schedule(Schedule schedule, string userinput)
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var items = new List<Items>();
            items = _data.SetUpItems(items);
            scheduleMock.Setup(service => service.ChangeSchedule("Test 2", entries, schedule));
            input = new StringReader(userinput);
            Console.SetIn(input);
            // Act
            ui.UI();
            // Assert
            scheduleMock.Verify(user => user.ChangeSchedule("Test 2", entries, schedule), Times.Once);
            _console.ResetReader();
        }
        [Theory]
        [MemberData(nameof(ShopListManagerTestData.GetStatusUpdateInput), MemberType = typeof(ShopListManagerTestData))]
        public void When_User_Selected_Change_Item_Status_Then_Items_Should_Update_With_New_Status(ItemStatus status, string userinput)
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var items = new List<Items>();
            items = _data.SetUpItems(items);
            statusMock.Setup(service => service.ChangeStatus("Test 2", entries, status));
            input = new StringReader(userinput);
            Console.SetIn(input);
            // Act
            ui.UI();
            // Assert
            statusMock.Verify(user => user.ChangeStatus("Test 2", entries, status), Times.Once);
            _console.ResetReader();
        }
        [Theory]
        [MemberData(nameof(ShopListManagerTestData.GetCategoryUpdateInput), MemberType = typeof(ShopListManagerTestData))]
        public void When_User_Selected_Change_Item_Category_Then_Items_Should_Update_With_New_Category(Category category, string userinput)
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var items = new List<Items>();
            items = _data.SetUpItems(items);
            categoryMock.Setup(service => service.ChangeCategory("Test 2", entries, category));
            input = new StringReader(userinput);
            Console.SetIn(input);
            // Act
            ui.UI();
            // Assert
            categoryMock.Verify(user => user.ChangeCategory("Test 2", entries, category), Times.Once);
            _console.ResetReader();
        }
        [Fact]
        public void When_User_Selected_Delete_Item_Then_Items_Should_be_Deleted()
        {
            // Arrange
            var ui = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var items = new List<Items>();
            items = _data.SetUpItems(items);
            itemMock.Setup(service => service.DeleteItems("Test 2"));
            input = new StringReader("10\nTest 2\n0");
            Console.SetIn(input);
            // Act
            ui.UI();
            // Assert
            itemMock.Verify(user => user.DeleteItems("Test 2"), Times.Once);
            _console.ResetReader();
        }
        [Fact]
        public void When_User_Selected_To_Save_Items_List_Then_Items_Should_Be_Saved()
        {
            // Arrange
            var shopListManager = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, inputMock.Object);
            var items = new List<Items>();
            items = _data.SetUpItems(items);
            fileMock.Setup(service => service.SaveFunction(It.IsAny<string>(), Entries.ITEMS, It.IsAny<List<Tasks>>(), It.IsAny<List<Items>>(), It.IsAny<List<Meals>>()));
            input = new StringReader("11\n1\nFakeItem\n0");
            Console.SetIn(input);
            // Act
            shopListManager.UI();
            // Assert
            fileMock.Verify(user => user.SaveFunction(It.IsAny<string>(), Entries.ITEMS, It.IsAny<List<Tasks>>(), It.IsAny<List<Items>>(), It.IsAny<List<Meals>>()), Times.Once);
            _console.ResetReader();
        }
        [Fact]
        public void When_User_Selected_To_Load_Items_Then_Items_Should_Be_Loaded()
        {
            // Assert
            var taskMock = new Mock<ITaskService>();
            var shopListManager = new ShopListManager(itemMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, statusMock.Object, scheduleMock.Object, categoryMock.Object, fileMock.Object, UserInput);
            var meal = new Mock<IMealService>();
            var fileService = new FileService(new Formatter(), new ConsoleLogger(), new FileLogger(), taskMock.Object, new ItemService(consoleLoggerMock.Object, fileLoggerMock.Object), meal.Object);
            var items = new List<Items>();
            items = _data.SetUpItems(items);
            fileService.SaveFunction("Test", Entries.ITEMS, null, items, null);
            fileMock.Setup(service => service.LoadFunction(It.IsAny<string>(), Entries.ITEMS));
            input = new StringReader("11\n2\nTest\n0");
            Console.SetIn(input);
            // Act
            shopListManager.UI();
            // Assert
            fileMock.Verify(user => user.LoadFunction(It.IsAny<string>(), Entries.ITEMS), Times.Once);
            _console.ResetReader();
        }
    }
}
