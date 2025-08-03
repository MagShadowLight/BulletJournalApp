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
    public class CategoryServiceTest
    {
        private Formatter _formatter = new Formatter();
        private ConsoleLogger _consolelogger = new ConsoleLogger();
        private FileLogger _filelogger = new FileLogger();
        private TaskService _taskservice;
        private ItemService _itemservice;
        private CategoryService _categoryservice;
        private Entries entries;
        private int num;

        public CategoryServiceTest()
        {
            _taskservice = new TaskService(_formatter, _consolelogger, _filelogger);
            _itemservice = new ItemService(_consolelogger, _filelogger);
            _categoryservice = new CategoryService(_consolelogger, _filelogger, _formatter, _taskservice, _itemservice);
            
        }
        public void SetUpList()
        {
            switch (entries)
            {
                case Entries.TASKS:
                    var data1 = new CategoryServiceData();
                    data1.SetUpTasks(_taskservice);
                    num = _taskservice.ListAllTasks().Count;
                    break;
                case Entries.ITEMS:
                    var data2 = new CategoryServiceData();
                    data2.SetUpItems(_itemservice);
                    num = _itemservice.GetAllItems().Count;
                    break;

            }
        }

        [Theory]
        [MemberData(nameof(CategoryServiceData.GetCategoryAndStringValue), MemberType = typeof(CategoryServiceData))]
        public void Given_There_Are_Tasks_In_The_List_When_Changing_The_Category_Then_Task_Should_Be_Updated_With_New_Category(string title, Category category)
        {
            // Arrange
            entries = Entries.TASKS;
            SetUpList();
            // Act
            _categoryservice.ChangeCategory(title, entries, category);
            var tasks = _taskservice.ListAllTasks();
            var task = _taskservice.FindTasksByTitle(title);
            // Assert
            Assert.Equal(num, tasks.Count);
            Assert.Equal(category, task.Category);
            Assert.Throws<ArgumentNullException>(() => _categoryservice.ChangeCategory(null, entries, category));
        }
        [Theory]
        [MemberData(nameof(CategoryServiceData.GetCategoryAndStringValue), MemberType = typeof(CategoryServiceData))]
        public void Given_There_Are_Items_In_The_Shopping_List_When_Changing_The_Category_Then_Item_Should_Be_Updated_With_New_Category(string name, Category category)
        {
            // Arrange
            entries = Entries.ITEMS;
            SetUpList();
            // Act
            _categoryservice.ChangeCategory(name, entries, category);
            var items = _itemservice.GetAllItems();
            var item = _itemservice.FindItemsByName(name);
            // Assert
            Assert.Equal(num, items.Count);
            Assert.Equal(category, item.Category);
            Assert.Throws<ArgumentNullException>(() => _categoryservice.ChangeCategory(null, entries, category));
        }
        [Theory]
        [MemberData(nameof(CategoryServiceData.GetCategoryValue), MemberType =typeof(CategoryServiceData))]
        public void Given_There_Are_Tasks_In_The_List_When_Listing_Tasks_With_Category_Value_Then_It_Should_Return_List_Of_Tasks_With_Only_Specific_Category_Value(int num, Category category)
        {
            // Arrange
            entries = Entries.TASKS;
            SetUpList();
            // Act
            var tasks = _categoryservice.ListTasksByCategory(category);
            // Assert
            Assert.Equal(num, tasks.Count);
        }
        [Theory]
        [MemberData(nameof(CategoryServiceData.GetCategoryValue), MemberType = typeof(CategoryServiceData))]
        public void Given_There_Are_Items_In_The_List_When_Listing_Items_With_Category_Value_Then_It_Should_Return_List_Of_Items_With_Only_Specific_Category_Value(int num, Category category)
        {
            // Arrange
            entries = Entries.ITEMS;
            SetUpList();
            // Act
            var items = _categoryservice.ListItemsByCategory(category);
            // Assert
            Assert.Equal(num, items.Count);
        }
    }
}
