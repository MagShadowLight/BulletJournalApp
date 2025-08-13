using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Test.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Service
{
    public class ItemServiceTest
    {
        private ConsoleLogger _consolelogger = new ConsoleLogger();
        private FileLogger _filelogger = new FileLogger();
        private ItemService _itemService;
        private ItemServiceData _data;
        Items item1 = new Items("Test 1", "Test", Periodicity.Monthly, 1);
        Items item2 = new Items("Test 2", "Test", Periodicity.Monthly, 1);
        Items item3 = new Items("Test 3", "Test", Periodicity.Monthly, 1);

        public ItemServiceTest()
        {
            _itemService = new ItemService(_consolelogger, _filelogger);
            _data = new ItemServiceData();
        }
        [Fact]
        public void Given_There_Are_No_Items_In_The_Shopping_List_When_Adding_An_Item_Then_It_Should_Be_Added_To_The_List()
        {
            // Arrange
            int num = 3;
            // Act
            _itemService.AddItems(item1);
            _itemService.AddItems(item2);
            _itemService.AddItems(item3);
            var items = _itemService.GetAllItems();
            // Assert
            Assert.Equal(num, items.Count);
            Assert.Contains(item1, items);
            Assert.Contains(item2, items);
            Assert.Contains(item3, items);
            Assert.Throws<FormatException>(() => _itemService.AddItems(new Items("Test 1", "Test", Periodicity.Monthly, 1)));
            Assert.Throws<ArgumentNullException>(() => _itemService.AddItems(new Items("", "Test", Periodicity.Monthly, 1)));
            Assert.Throws<ArgumentOutOfRangeException>(() => _itemService.AddItems(new Items("Test 4", "Test", Periodicity.Monthly, -5)));
        }
        [Fact]
        public void Given_There_Are_Items_In_The_Shopping_List_When_Listing_All_Items_Then_It_Should_Return_All_Items()
        {
            // Arrange
            int num = 3;
            _data.SetUpItems(_itemService, item1, item2, item3);
            // Act
            var items = _itemService.GetAllItems();
            // Assert
            Assert.Equal(num, items.Count);
            Assert.Contains(item1, items);
            Assert.Contains(item2, items);
            Assert.Contains(item3, items);
        }
        [Fact]
        public void Given_There_Are_Items_In_The_Shopping_List_When_Listing_Items_Not_Owned_Then_It_Should_Return_Items_Not_Owned()
        {
            // Arrange
            int num = 2;
            _data.SetUpItems(_itemService, item1, item2, item3);
            _itemService.MarkItemsAsBought(item2.Name);
            // Act
            var unownedItems = _itemService.GetItemsNotOwned();
            // Assert
            Assert.Equal(num, unownedItems.Count);
            Assert.Contains(item1, unownedItems);
            Assert.DoesNotContain(item2, unownedItems);
            Assert.Contains(item3, unownedItems);
        }
        [Fact]
        public void Given_There_Are_Items_In_The_Shopping_List_When_Listing_Items_Owned_Then_It_Should_Return_Items_Owned()
        {
            // Arrange
            int num = 1;
            _data.SetUpItems(_itemService, item1, item2, item3);
            _itemService.MarkItemsAsBought(item2.Name);
            // Act
            var ownedItems = _itemService.GetItemsOwned();
            // Assert
            Assert.Equal(num, ownedItems.Count);
            Assert.Contains(item2, ownedItems);
            Assert.DoesNotContain(item1, ownedItems);
            Assert.DoesNotContain(item3, ownedItems);
        }
        [Fact]
        public void Given_There_Are_Items_In_The_Shopping_List_When_Searching_For_The_Item_With_A_Name_Then_It_Should_Return_That_Item()
        {
            // Arrange
            _data.SetUpItems(_itemService, item1, item2, item3);
            // Act
            var item = _itemService.FindItemsByName(item2.Name);
            // Assert
            Assert.Equal(item2, item);
            Assert.NotEqual(item1, item);
            Assert.NotEqual(item3, item);
        }
        [Theory]
        [MemberData(nameof(ItemServiceData.GetStringValue), MemberType =typeof(ItemServiceData))]
        public void Given_There_Are_Items_In_The_Shopping_List_When_Marking_Items_As_Bought_Then_Item_Should_Be_Marked_As_Bought(string name)
        {
            // Arrange
            int num = 3;
            _data.SetUpItems(_itemService, item1, item2, item3);
            // Act
            _itemService.MarkItemsAsBought(name);
            var items = _itemService.GetAllItems();
            var item = _itemService.FindItemsByName(name);
            // Assert
            Assert.Equal(num, items.Count);
            Assert.Equal(ItemStatus.Bought, item.Status);
            Assert.Equal(DateTime.Today, item.DateBought);
            Assert.Throws<ArgumentNullException>(() => _itemService.MarkItemsAsBought(null));
        }
        [Theory]
        [MemberData(nameof(ItemServiceData.GetValuesForUpdate), MemberType =typeof(ItemServiceData))]
        public void Given_There_Are_Items_In_The_Shopping_List_When_Updating_An_Item_Then_Item_Should_Be_Updated_With_New_Values(string oldname, string newname, string newdescription, string newnote, int quantity)
        {
            // Arrange
            int num = 3;
            _data.SetUpItems(_itemService, item1, item2, item3);
            // Act
            _itemService.UpdateItems(oldname, newname, newdescription, newnote, quantity);
            var items = _itemService.GetAllItems();
            var item = _itemService.FindItemsByName(newname);
            // Assert
            Assert.Equal(num, items.Count);
            Assert.Equal(newname, item.Name);
            Assert.Equal(newdescription, item.Description);
            Assert.Equal(quantity, item.Quantity);
            Assert.Equal(newnote, item.Notes);
            Assert.Throws<ArgumentNullException>(() => _itemService.UpdateItems(null, newname, newdescription, newnote, quantity));
            Assert.Throws<FormatException>(() => _itemService.UpdateItems(newname, newname, newdescription, newnote, quantity));
        }
        [Theory]
        [MemberData(nameof(ItemServiceData.GetStringValue), MemberType = typeof(ItemServiceData))]
        public void Given_There_Are_Items_In_The_Shopping_List_When_Deleting_An_Item_Then_Item_Should_Be_Deleted(string name)
        {
            // Assert
            int num = 2;
            _data.SetUpItems(_itemService, item1, item2, item3);
            var item = _itemService.FindItemsByName(name);
            // Act
            _itemService.DeleteItems(name);
            var items = _itemService.GetAllItems();
            // Assert
            Assert.Equal(num, items.Count);
            Assert.DoesNotContain(item, items);
            Assert.Throws<ArgumentNullException>(() => _itemService.DeleteItems(null));
        }








        /*

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
            service.UpdateItems("Test3", "Updated Test", "Updated Description", "New Note", 1);
            items = service.GetAllItems();
            // Assert
            Assert.Equal(3, items.Count);
            Assert.Contains("Updated Test", item3.Name);
            Assert.Contains("Updated Description", item3.Description);
            Assert.Contains("New Note", item3.Notes);
            Assert.Throws<Exception>(() => service.UpdateItems("Fake Item", "Invalid Item", "Test", "", 1));
            Assert.Throws<Exception>(() => service.UpdateItems("Test2", "Test", "Test", "", 1));
            Assert.Throws<ArgumentNullException>(() => service.UpdateItems("Test2", "", "Test", "", 1));
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
        }*/
    }
}
