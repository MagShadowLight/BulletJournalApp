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
    public class ItemServiceTest
    {
        [Fact]
        public void When_Items_Were_Added_Then_It_Should_Succeed()
        {
            // Arrange
            List<Items> items;
            var service = new ItemService(new ConsoleLogger(), new FileLogger());
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test 2", "Test", Schedule.Monthly, 1);
            var item3 = new Items("Test 3", "Test", Schedule.Monthly, 1);
            // Act
            service.AddItems(item1);
            service.AddItems(item2);
            service.AddItems(item3);
            items = service.GetAllItems();
            // Assert
            Assert.Equal(3, items.Count);
            Assert.Contains(item1, items);
            Assert.Contains(item2, items);
            Assert.Contains(item3, items);
            Assert.Throws<ArgumentNullException>(() => service.AddItems(new Items("", "Test", Schedule.Monthly, 1)));
            Assert.Throws<Exception>(() => service.AddItems(new Items("Test", "Test", Schedule.Monthly, 1)));
        }
        [Fact]
        public void When_Items_Were_Marked_As_Owned_Then_It_Should_Return_Only_Owned_Items()
        {
            // Arrange
            List<Items> AllItems;
            List<Items> OwnedItems;
            var service = new ItemService(new ConsoleLogger(), new FileLogger());
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            service.AddItems(item1);
            service.AddItems(item2);
            service.AddItems(item3);
            // Act
            service.MarkItemsAsBought("Test2");
            AllItems = service.GetAllItems();
            OwnedItems = service.GetItemsOwned();
            // Assert
            Assert.Equal(3, AllItems.Count);
            Assert.Equal(1, OwnedItems.Count);
            Assert.Contains(item2, OwnedItems);
            Assert.DoesNotContain(item1, OwnedItems);
            Assert.DoesNotContain(item3, OwnedItems);
            Assert.Throws<Exception>(() => service.MarkItemsAsBought("Fake Item"));
        }
        [Fact]
        public void When_Items_Were_Marked_As_Owned_Then_It_Should_Return_Items_That_Were_Not_Owned()
        {
            // Arrange
            List<Items> AllItems;
            List<Items> UnOwnedItems;
            var service = new ItemService(new ConsoleLogger(), new FileLogger());
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            service.AddItems(item1);
            service.AddItems(item2);
            service.AddItems(item3);
            // Act
            service.MarkItemsAsBought("Test2");
            AllItems = service.GetAllItems();
            UnOwnedItems = service.GetItemsNotOwned();
            // Assert
            Assert.Equal(3, AllItems.Count);
            Assert.Equal(2, UnOwnedItems.Count);
            Assert.DoesNotContain(item2, UnOwnedItems);
            Assert.Contains(item1, UnOwnedItems);
            Assert.Contains(item3, UnOwnedItems);
            Assert.Throws<Exception>(() => service.MarkItemsAsBought("Fake Item"));
        }
        [Fact]
        public void When_Items_Were_Updated_Then_It_Should_Succeed()
        {
            // Assert
            List<Items> items;
            var service = new ItemService(new ConsoleLogger(), new FileLogger());
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            service.AddItems(item1);
            service.AddItems(item2);
            service.AddItems(item3);
            // Act
            service.UpdateItems("Test3", "Updated Test", "Updated Description", "New Note");
            items = service.GetAllItems();
            // Assert
            Assert.Equal(3, items.Count);
            Assert.Contains("Updated Test", item3.Name);
            Assert.Contains("Updated Description", item3.Description);
            Assert.Contains("New Note", item3.Notes);
            Assert.Throws<Exception>(() => service.UpdateItems("Fake Item", "Invalid Item", "Test", ""));
            Assert.Throws<Exception>(() => service.UpdateItems("Test2", "Test", "Test", ""));
            Assert.Throws<ArgumentNullException>(() => service.UpdateItems("Test2", "", "Test", ""));
        }
        [Fact]
        public void When_Items_Were_Deleted_Then_It_Should_Succeed()
        {
            // Assert
            List<Items> items;
            var service = new ItemService(new ConsoleLogger(), new FileLogger());
            var item1 = new Items("Test", "Test", Schedule.Monthly, 1);
            var item2 = new Items("Test2", "Test", Schedule.Monthly, 1);
            var item3 = new Items("Test3", "Test", Schedule.Monthly, 1);
            service.AddItems(item1);
            service.AddItems(item2);
            service.AddItems(item3);
            // Act
            service.DeleteItems("Test2");
            items = service.GetAllItems();
            // Assert
            Assert.Equal(2, items.Count);
            Assert.Contains(item1, items);
            Assert.Contains(item3, items);
            Assert.DoesNotContain(item2, items);
            Assert.Throws<Exception>(() => service.DeleteItems("Fake Item"));
        }
    }
}
