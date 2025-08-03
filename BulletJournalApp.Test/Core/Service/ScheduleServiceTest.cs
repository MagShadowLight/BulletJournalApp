using BulletJournalApp.Core.Interface;
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
    public class ScheduleServiceTest
    {
        private Formatter _formatter = new Formatter();
        private ConsoleLogger _consolelogger = new ConsoleLogger();
        private FileLogger _filelogger = new FileLogger();
        private TaskService _taskService;
        private ItemService _itemService;
        private ScheduleService _scheduleService;
        private Entries entries;
        private int num;

        public ScheduleServiceTest()
        {
            _taskService = new TaskService(_formatter, _consolelogger, _filelogger);
            _itemService = new ItemService(_consolelogger, _filelogger);
            _scheduleService = new ScheduleService(_formatter, _consolelogger, _filelogger, _taskService, _itemService);
        }
        public void SetUpList()
        {
            switch (entries)
            {
                case Entries.TASKS:
                    var data1 = new ScheduleServiceData();
                    data1.SetUpTasks(_taskService);
                    num = _taskService.ListAllTasks().Count;
                    break;
                case Entries.ITEMS:
                    var data2 = new ScheduleServiceData();
                    data2.SetUpItems(_itemService);
                    num = _itemService.GetAllItems().Count;
                    break;

            }
        }

        [Theory]
        [MemberData(nameof(ScheduleServiceData.GetScheduleAndStringValue), MemberType = typeof(ScheduleServiceData))]
        public void Given_There_Are_Tasks_In_The_List_When_Changing_The_Schedule_Then_Task_Should_Be_Updated_With_New_Schedule(string title, Schedule schedule)
        {
            // Arrange
            entries = Entries.TASKS;
            SetUpList();
            // Act
            _scheduleService.ChangeSchedule(title, entries, schedule);
            var tasks = _taskService.ListAllTasks();
            var task = _taskService.FindTasksByTitle(title);
            // Assert
            Assert.Equal(num, tasks.Count);
            Assert.Equal(schedule, task.schedule);
            Assert.Throws<ArgumentNullException>(() => _scheduleService.ChangeSchedule(null, entries, schedule));
        }
        [Theory]
        [MemberData(nameof(ScheduleServiceData.GetScheduleAndStringValue), MemberType =typeof(ScheduleServiceData))]
        public void Given_There_Are_Items_In_The_List_When_Changing_The_Schedule_Then_Item_Should_Be_Updated_With_New_Schedule(string name, Schedule schedule)
        {
            // Arrange
            entries = Entries.ITEMS;
            SetUpList();
            // Act
            _scheduleService.ChangeSchedule(name, entries, schedule);
            var items = _itemService.GetAllItems();
            var item = _itemService.FindItemsByName(name);
            // Assert
            Assert.Equal(num, items.Count);
            Assert.Equal(schedule, item.Schedule);
            Assert.Throws<ArgumentNullException>(() => _scheduleService.ChangeSchedule(null, entries, schedule));
        }
        [Theory]
        [MemberData(nameof(ScheduleServiceData.GetScheduleValue), MemberType = typeof(ScheduleServiceData))]
        public void Given_There_Are_Tasks_In_The_List_When_Listing_Tasks_With_Schedule_Value_Then_It_Should_Return_List_Of_Tasks_With_Specific_Schedule(int num, Schedule schedule)
        {
            // Arrange
            entries = Entries.TASKS;
            SetUpList();
            // Act
            var tasks = _scheduleService.ListTasksBySchedule(schedule);
            // Assert
            Assert.Equal(num, tasks.Count);
        }
        [Theory]
        [MemberData(nameof(ScheduleServiceData.GetScheduleValue), MemberType = typeof(ScheduleServiceData))]
        public void Given_There_Are_Items_In_The_Shopping_List_When_Listing_Items_With_Schedule_Value_Then_It_Should_Return_List_Of_Items_With_Specific_Schedule(int num, Schedule schedule)
        {
            // Arrange
            entries = Entries.ITEMS;
            SetUpList();
            // Act
            var items = _scheduleService.ListItemsBySchedule(schedule);
            // Assert
            Assert.Equal(num, items.Count);
        }
    }
}
