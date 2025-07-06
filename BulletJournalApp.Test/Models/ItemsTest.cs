using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Models
{
    public class ItemsTest
    {
        [Fact]
        public void When_Creating_An_Items_Then_It_Should_Initalize_Id()
        {
            // Arrange
            Items item = new Items("Test Item", "This is a test item", Schedule.Daily, 1, Category.Works, ItemStatus.Bought, "Test note");
            // Act // Assert
            Assert.Equal(1, item.Id);
        }
        [Fact]
        public void When_Creating_An_Items_Then_It_Should_Initalize_Name()
        {
            // Arrange
            Items item = new Items("Test Item", "This is a test item", Schedule.Daily, 1, Category.Works, ItemStatus.Bought, "Test note");
            // Act // Assert
            Assert.Equal("Test Item", item.Name);
        }
        [Fact]
        public void When_Creating_An_Items_Then_It_Should_Initalize_Description()
        {
            // Arrange
            Items item = new Items("Test Item", "This is a test item", Schedule.Daily, 1, Category.Works, ItemStatus.Bought, "Test note");
            // Act // Assert
            Assert.Equal("This is a test item", item.Description);
        }
        [Fact]
        public void When_Creating_An_Items_Then_It_Should_Initalize_Schedule()
        {
            // Arrange
            Items item = new Items("Test Item", "This is a test item", Schedule.Daily, 1, Category.Works, ItemStatus.Bought, "Test note");
            // Act // Assert
            Assert.Equal(Schedule.Daily, item.Schedule);
        }
        [Fact]
        public void When_Creating_An_Items_Then_It_Should_Initalize_Category()
        {
            // Arrange
            Items item = new Items("Test Item", "This is a test item", Schedule.Daily, 1, Category.Works, ItemStatus.Bought, "Test note");
            // Act // Assert
            Assert.Equal(Category.Works, item.Category);
        }
        [Fact]
        public void When_Creating_An_Items_Then_It_Should_Initalize_Status()
        {
            // Arrange
            Items item = new Items("Test Item", "This is a test item", Schedule.Daily, 1, Category.Works, ItemStatus.Bought, "Test note");
            // Act // Assert
            Assert.Equal(ItemStatus.Bought, item.Status);
        }
        [Fact]
        public void When_Creating_An_Items_Then_It_Should_Initalize_Notes()
        {
            // Arrange
            Items item = new Items("Test Item", "This is a test item", Schedule.Daily, 1, Category.Works, ItemStatus.Bought, "Test note");
            // Act // Assert
            Assert.Equal("Test note", item.Notes);
        }
        [Fact]
        public void When_Creating_An_Items_Then_It_Should_Initalize_DateAdded()
        {
            // Arrange
            Items item = new Items("Test Item", "This is a test item", Schedule.Daily, 1, Category.Works, ItemStatus.Bought, "Test note");
            // Act // Assert
            Assert.Equal(DateTime.Today, item.DateAdded);
        }
        [Fact]
        public void When_Creating_An_Items_Then_It_Should_Initalize_DateBought()
        {
            // Arrange

            Items item = new Items("Test Item", "This is a test item", Schedule.Daily, 1, Category.Works, ItemStatus.Bought, "Test note", DateTime.Parse("Jun 10, 2025"), DateTime.Parse("Jun 20, 2025"));
            // Act // Assert
            Assert.Equal(DateTime.Parse("Jun 20, 2025"), item.DateBought);
        }
        [Fact]
        public void When_Creating_An_Items_Then_It_Should_Initalize_DateBought_As_Null()
        {
            // Arrange
            Items item = new Items("Test Item", "This is a test item", Schedule.Daily, 1, Category.Works, ItemStatus.Bought, "Test note", DateTime.Parse("Jun 10, 2025"));
            // Act // Assert
            Assert.Null(item.DateBought);
        }
        [Fact]
        public void When_There_Is_Items_Then_It_Should_Update_With_New_Name()
        {
            // Arrange
            Items item = new Items("Test Item", "This is a test item", Schedule.Daily, 1, Category.Works, ItemStatus.Bought, "Test note");
            // Act
            item.Update("Updated Item", "This is an updated test item", "Updated note");
            // Assert
            Assert.Equal("Updated Item", item.Name);
        }
        [Fact]
        public void When_There_Is_Items_Then_It_Should_Change_Schedule()
        {
            // Arrange
            Items item = new Items("Test Item", "This is a test item", Schedule.Daily, 1, Category.Works, ItemStatus.Bought, "Test note");
            // Act
            item.ChangeSchedule(Schedule.Monthly);
            // Assert
            Assert.Equal(Schedule.Monthly, item.Schedule);
        }
        [Fact]
        public void When_There_Is_Items_Then_It_Should_Change_Category()
        {
            // Arrange
            Items item = new Items("Test Item", "This is a test item", Schedule.Daily, 1, Category.Works, ItemStatus.Bought, "Test note");
            // Act
            item.ChangeCategory(Category.Personal);
            // Assert
            Assert.Equal(Category.Personal, item.Category);
        }
        [Fact]
        public void When_There_Is_Items_Then_It_Should_Change_Status()
        {
            // Arrange
            Items item = new Items("Test Item", "This is a test item", Schedule.Daily, 1, Category.Works, ItemStatus.Bought, "Test note");
            // Act
            item.ChangeStatus(ItemStatus.NotBought);
            // Assert
            Assert.Equal(ItemStatus.NotBought, item.Status);
        }
        [Fact]
        public void When_There_Is_Items_Then_It_Should_Mark_As_Bought()
        {
            // Arrange
            Items item = new Items("Test Item", "This is a test item", Schedule.Daily, 1, Category.Works, ItemStatus.NotBought, "Test note");
            // Act
            item.MarkAsBought();
            // Assert
            Assert.Equal(ItemStatus.Bought, item.Status);
            Assert.NotNull(item.DateBought);
            Assert.Equal(DateTime.Today, item.DateBought.Value);
        }
        [Fact]
        public void When_Updating_Items_With_Blank_Title_Or_Description_Then_It_Should_Throw()
        {
            // Arrange
            List<Items> items = new List<Items>();
            Items item1 = new Items("Meow", "Meow", Schedule.Daily, 0);
            Items item2 = new Items("Mrow", "Mrow", Schedule.Daily, 0);
            // Act
            items.Add(item1);
            items.Add(item2);
            // Assert
            Assert.Throws<ArgumentNullException>(() => item1.Update("", "Mrow", ""));
            Assert.Throws<ArgumentNullException>(() => item2.Update("Mrrp", "", ""));
        }
    }
}
