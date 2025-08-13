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
    public class PeriodicityServiceTest
    {
        private Formatter _formatter = new Formatter();
        private ConsoleLogger _consolelogger = new ConsoleLogger();
        private FileLogger _filelogger = new FileLogger();
        private TaskService _taskService;
        private ItemService _itemService;
        private PeriodicityService _scheduleService;
        private Entries entries;
        private int num;
        private RoutineService _routineService;
        private PeriodicityServiceData _data;

        public PeriodicityServiceTest()
        {
            _taskService = new TaskService(_formatter, _consolelogger, _filelogger);
            _itemService = new ItemService(_consolelogger, _filelogger);
            _routineService = new();
            _scheduleService = new PeriodicityService(_formatter, _consolelogger, _filelogger, _taskService, _itemService, _routineService);
        }
        public void SetUpList()
        {
            switch (entries)
            {
                case Entries.TASKS:
                    _data = new PeriodicityServiceData();
                    _data.SetUpTasks(_taskService);
                    num = _taskService.ListAllTasks().Count;
                    break;
                case Entries.ITEMS:
                    _data = new PeriodicityServiceData();
                    _data.SetUpItems(_itemService);
                    num = _itemService.GetAllItems().Count;
                    break;
                case Entries.ROUTINES:
                    _data = new PeriodicityServiceData();
                    _data.SetUpRoutines(_routineService);
                    num = _routineService.GetAllRoutines().Count;
                    break;
            }
        }

        [Theory]
        [MemberData(nameof(PeriodicityServiceData.GetScheduleAndStringValue), MemberType = typeof(PeriodicityServiceData))]
        public void Given_There_Are_Tasks_In_The_List_When_Changing_The_Schedule_Then_Task_Should_Be_Updated_With_New_Schedule(string title, Periodicity schedule)
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
        [MemberData(nameof(PeriodicityServiceData.GetScheduleAndStringValue), MemberType =typeof(PeriodicityServiceData))]
        public void Given_There_Are_Items_In_The_List_When_Changing_The_Schedule_Then_Item_Should_Be_Updated_With_New_Schedule(string name, Periodicity schedule)
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
        [MemberData(nameof(PeriodicityServiceData.GetScheduleAndStringValue), MemberType =typeof(PeriodicityServiceData))]
        public void Given_There_Are_Routines_In_The_List_When_Changing_The_Schedule_Then_Routine_Should_Be_Updated_With_New_Schedule(string name, Periodicity schedule)
        {
            // Arrange
            entries = Entries.ROUTINES;
            SetUpList();
            // Act
            _scheduleService.ChangeSchedule(name, entries, schedule);
            var routines = _routineService.GetAllRoutines();
            var routine = _routineService.FindRoutineByName(name);
            // Assert
            Assert.Equal(num, routines.Count);
            Assert.Equal(schedule, routine.Periodicity);
            Assert.Throws<ArgumentNullException>(() => _scheduleService.ChangeSchedule(null, entries, schedule));
        }
        [Theory]
        [MemberData(nameof(PeriodicityServiceData.GetScheduleValue), MemberType = typeof(PeriodicityServiceData))]
        public void Given_There_Are_Tasks_In_The_List_When_Listing_Tasks_With_Schedule_Value_Then_It_Should_Return_List_Of_Tasks_With_Specific_Schedule(int num, Periodicity schedule)
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
        [MemberData(nameof(PeriodicityServiceData.GetScheduleValue), MemberType = typeof(PeriodicityServiceData))]
        public void Given_There_Are_Items_In_The_Shopping_List_When_Listing_Items_With_Schedule_Value_Then_It_Should_Return_List_Of_Items_With_Specific_Schedule(int num, Periodicity schedule)
        {
            // Arrange
            entries = Entries.ITEMS;
            SetUpList();
            // Act
            var items = _scheduleService.ListItemsBySchedule(schedule);
            // Assert
            Assert.Equal(num, items.Count);
        }
        [Theory]
        [MemberData(nameof(PeriodicityServiceData.GetScheduleValue), MemberType = typeof(PeriodicityServiceData))]
        public void Given_There_Are_Routines_In_The__List_When_Listing_Routines_With_Schedule_Value_Then_It_Should_Return_List_Of_Routines_With_Specific_Schedule(int num, Periodicity schedule)
        {
            // Arrange
            entries = Entries.ROUTINES;
            SetUpList();
            // Act
            var routines = _scheduleService.ListRoutinesByPeriodicity(schedule);
            // Assert
            Assert.Equal(num, routines.Count);
        }
    }
}
