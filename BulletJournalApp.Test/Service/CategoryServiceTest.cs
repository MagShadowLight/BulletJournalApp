using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Service
{
    public class CategoryServiceTest
    {
        private TaskService _taskService = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
        private ItemService _itemService = new ItemService(new ConsoleLogger(), new FileLogger());
        [Fact]
        public void When_User_Try_To_Change_Category_With_Empty_Tasks_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var service2 = new CategoryService(new ConsoleLogger(), new FileLogger(), new Formatter(), _taskService, _itemService);
            var task = new Tasks(DateTime.Now, "Test", "Test", Schedule.Monthly, false);
            _taskService.AddTask(task);
            // Act // Assert
            Assert.Throws<Exception>(() => service2.ChangeCategory("", Entries.TASKS, Category.Home));
        }
        [Fact]
        public void When_User_Change_Category_Then_Items_Category_Should_Update()
        {
            // Arrange
            List<Items> items;
            var service2 = new CategoryService(new ConsoleLogger(), new FileLogger(), new Formatter(), _taskService, _itemService);
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            _itemService.AddItems(item1);
            _itemService.AddItems(item2);
            _itemService.AddItems(item3);
            // Act
            service2.ChangeCategory("Test2", Entries.ITEMS, Category.Home);
            items = _itemService.GetAllItems();
            // Assert
            Assert.Equal(3, items.Count);
            Assert.Equal(Category.Home, item2.Category);
            Assert.Contains(item1, items);
            Assert.Contains(item2, items);
            Assert.Contains(item3, items);
            Assert.Throws<Exception>(() => service2.ChangeCategory("Fake Test", Entries.ITEMS, Category.Home));
        }
        [Fact]
        public void When_Category_Were_Selected_Then_Items_Should_Return_With_Category()
        {
            // Arrange
            List<Items> AllItems;
            List<Items> HomeItems;
            Category category = Category.Home;
            var service2 = new CategoryService(new ConsoleLogger(), new FileLogger(), new Formatter(), _taskService, _itemService);
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            _itemService.AddItems(item1);
            _itemService.AddItems(item2);
            _itemService.AddItems(item3);
            service2.ChangeCategory("Test2", Entries.ITEMS, Category.Home);
            // Act
            AllItems = _itemService.GetAllItems();
            HomeItems = service2.ListItemsByCategory(category);
            // Assert
            Assert.Equal(3, AllItems.Count);
            Assert.Single(HomeItems);
            Assert.Contains(item2, HomeItems);
            Assert.DoesNotContain(item1, HomeItems);
            Assert.DoesNotContain(item3, HomeItems);
        }
    }
}
