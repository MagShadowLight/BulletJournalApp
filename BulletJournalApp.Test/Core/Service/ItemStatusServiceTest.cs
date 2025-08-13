using BulletJournalApp.Core.Services;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletJournalApp.Test.Core.Data;

namespace BulletJournalApp.Test.Core.Service
{
    public class ItemStatusServiceTest
    {
        private ConsoleLogger _consolelogger = new ConsoleLogger();
        private FileLogger _filelogger = new FileLogger();
        private ItemService _itemService;
        private ItemStatusService _itemStatusService;

        public ItemStatusServiceTest()
        {
            _itemService = new ItemService(_consolelogger, _filelogger);
            _itemStatusService = new ItemStatusService(_itemService);
            var data = new ItemStatusServiceData();
            data.SetUpItems(_itemService);
        }

        [Theory]
        [MemberData(nameof(ItemStatusServiceData.GetStatusAndStringValue), MemberType =typeof(ItemStatusServiceData))]
        public void Given_There_Are_Items_In_The_Shopping_List_When_Changing_The_Status_Then_Item_Should_Be_Updated_With_New_Status(string name, ItemStatus status)
        {
            // Arrange
            int num = 7;
            // Act
            _itemStatusService.ChangeStatus(name, Entries.ITEMS, status);
            var items = _itemService.GetAllItems();
            var item = _itemService.FindItemsByName(name);
            // Assert
            Assert.Equal(num, items.Count);
            Assert.Equal(status, item.Status);
            Assert.Throws<ArgumentNullException>(() => _itemStatusService.ChangeStatus(null, Entries.ITEMS, status));
        }
        [Theory]
        [MemberData(nameof(ItemStatusServiceData.GetStatusValue), MemberType =typeof(ItemStatusServiceData))]
        public void Given_There_Are_Items_In_The_Shopping_List_When_Listing_The_Items_With_Specific_Status_Then_It_Should_Return_A_List_Of_Items_With_Status_Value(int num, ItemStatus status)
        {
            // Assert // Act
            var items = _itemStatusService.ListItemsByStatus(status);
            // Assert
            Assert.Equal(num, items.Count);
        }
    }
}
