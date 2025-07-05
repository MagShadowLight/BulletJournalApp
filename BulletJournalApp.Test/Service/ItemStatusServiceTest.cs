using BulletJournalApp.Core.Services;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Service
{
    public class ItemStatusServiceTest
    {
        private ItemService _itemService = new ItemService(new ConsoleLogger(), new FileLogger());
        public Items item1 = new Items("Test", "Test", Schedule.Monthly);
        public Items item2 = new Items("Test2", "Test", Schedule.Monthly);
        public Items item3 = new Items("Test3", "Test", Schedule.Monthly);
        [Fact]
        public void When_User_Change_Status_Then_Items_Should_Update_With_New_Status()
        {
            // Arrange
            List<Items> items;
            var service = new ItemStatusService(_itemService);
            _itemService.AddItems(item1);
            _itemService.AddItems(item2);
            _itemService.AddItems(item3);
            // Act
            service.ChangeStatus("Test2", Entries.ITEMS, ItemStatus.Cancelled);
            items = _itemService.GetAllItems();
            // Assert
            Assert.Equal(3, items.Count);
            Assert.Equal(ItemStatus.Cancelled, item2.Status);
            Assert.Throws<Exception>(() => service.ChangeStatus("Fake Item", Entries.ITEMS, ItemStatus.Arrived));
        }
        [Fact]
        public void When_Status_Were_Selected_Then_Items_Should_Return_With_Only_Just_This_Status()
        {
            // Arrange
            List<Items> AllItems;
            List<Items> ArrivedItems;
            var service = new ItemStatusService(_itemService);
            _itemService.AddItems(item1);
            _itemService.AddItems(item2);
            _itemService.AddItems(item3);
            service.ChangeStatus("Test3", Entries.ITEMS, ItemStatus.Arrived);
            // Act
            AllItems = _itemService.GetAllItems();
            ArrivedItems = service.ListItemsByStatus(ItemStatus.Arrived);
            // Assert
            Assert.Equal(3, AllItems.Count);
            Assert.Single(ArrivedItems);
            Assert.Contains(item3, ArrivedItems);
            Assert.DoesNotContain(item1, ArrivedItems);
            Assert.DoesNotContain(item2, ArrivedItems);
        }
    }
}
