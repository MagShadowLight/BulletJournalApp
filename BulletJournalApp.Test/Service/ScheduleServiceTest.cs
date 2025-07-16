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
    public class ScheduleServiceTest
    {
        private TaskService _taskService = new TaskService(new Formatter(), new ConsoleLogger(), new FileLogger());
        private ItemService _itemService = new ItemService(new ConsoleLogger(), new FileLogger());
        [Fact]
        public void When_Task_Change_Schedule_Then_It_Should_Succeed()
        {
            // Arrange
            var service = new ScheduleService(new Formatter(), new ConsoleLogger(), new FileLogger(), _taskService, _itemService);
            var task1 = new Tasks(DateTime.Now, "Test 1", "Test", Schedule.Monthly, false);
            _taskService.AddTask(task1);
            // Act
            service.ChangeSchedule("Test 1", Entries.TASKS, Schedule.Daily);
            var task = service.ListTasksBySchedule(Schedule.Daily);
            // Assert
            Assert.Contains(task1, task);
            Assert.Throws<Exception>(() => service.ChangeSchedule("", Entries.TASKS, Schedule.Daily));
        }
        [Fact]  
        public void When_User_Change_Schedule_Then_Item_Should_Update_With_New_Schedule()
        {
            // Arrange
            List<Items> items;
            var service2 = new ScheduleService(new Formatter(), new ConsoleLogger(), new FileLogger(), _taskService, _itemService);
            var item1 = new Items("Test", "Test", Schedule.Monthly);
            var item2 = new Items("Test2", "Test", Schedule.Monthly);
            var item3 = new Items("Test3", "Test", Schedule.Monthly);
            _itemService.AddItems(item1);
            _itemService.AddItems(item2);
            _itemService.AddItems(item3);
            // Act
            service2.ChangeSchedule("Test2", Entries.ITEMS, Schedule.Daily);
            items = _itemService.GetAllItems();
            // Assert
            Assert.Equal(3, items.Count);
            Assert.Equal(Schedule.Daily, item2.Schedule);
            Assert.Throws<Exception>(() => service2.ChangeSchedule("Fake Test", Entries.ITEMS, Schedule.Monthly));
        }
        [Fact]
        public void When_Schedule_Were_Selected_Then_Items_Should_Return_With_Schedule()
        {
            // Assert
            List<Items> AllItems;
            List<Items> MonthlyItems;
            Schedule schedule = Schedule.Monthly;
            var service2 = new ScheduleService(new Formatter(), new ConsoleLogger(), new FileLogger(), _taskService, _itemService);
            var item1 = new Items("Test", "Test", Schedule.Monthly);
            var item2 = new Items("Test2", "Test", Schedule.Monthly);
            var item3 = new Items("Test3", "Test", Schedule.Monthly);
            _itemService.AddItems(item1);
            _itemService.AddItems(item2);
            _itemService.AddItems(item3);
            service2.ChangeSchedule("Test3", Entries.ITEMS, Schedule.Weekly);
            // Act
            AllItems = _itemService.GetAllItems();
            MonthlyItems = service2.ListItemsBySchedule(Schedule.Monthly);
            // Assert
            Assert.Equal(3, AllItems.Count);
            Assert.Equal(2, MonthlyItems.Count);
            Assert.Contains(item1, MonthlyItems);
            Assert.Contains(item2, MonthlyItems);
            Assert.DoesNotContain(item3, MonthlyItems);
        }
    }
}
