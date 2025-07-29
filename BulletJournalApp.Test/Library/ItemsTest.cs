using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Test.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Library
{
    public class ItemsTest
    {
        [Theory]
        [MemberData(nameof(ItemsDataFixture.GetValidItemsForCreation), MemberType =typeof(ItemsDataFixture))]
        public void Given_There_Are_Valid_Items_When_Items_Were_Added_Then_Those_Values_Should_Assigned(string name, string desc, Schedule schedule, int quantity, int id, Category category, ItemStatus status, string notes, DateTime? dateadded, DateTime? datebought, DateTime expectedvalue1, DateTime expectedvalue2)
        {
            // Arrange // Act
            var item = new Items(name, desc, schedule, quantity, id, category, status, notes, dateadded, datebought);
            // Assert
            Assert.Equal(name, item.Name);
            Assert.Equal(desc, item.Description);
            Assert.Equal(schedule, item.Schedule);
            Assert.Equal(quantity, item.Quantity);
            Assert.Equal(id, item.Id);
            Assert.Equal(category, item.Category);
            Assert.Equal(status, item.Status);
            Assert.Equal(notes, item.Notes);
            Assert.Equal(expectedvalue1, item.DateAdded);
            Assert.Equal(expectedvalue2, item.DateBought);
        }
        [Theory]
        [MemberData(nameof(ItemsDataFixture.GetItemsWithEmptyString), MemberType = typeof(ItemsDataFixture))]
        public void Given_There_Are_Items_With_Empty_Name_Or_Description_When_Items_Tried_To_Add_Then_It_Should_Throw_Argument_Null_Exception(string name, string desc, Schedule schedule, int quantity, int id, Category category, ItemStatus status, string notes, DateTime? dateadded, DateTime? datebought)
        {
            Assert.Throws<ArgumentNullException>(() => { new Items(name, desc, schedule, quantity, id, category, status, notes, dateadded, datebought); });
        }
        [Theory]
        [MemberData(nameof(ItemsDataFixture.GetItemsWithInvalidQuantityForCreation), MemberType = typeof(ItemsDataFixture))]
        public void Given_There_Are_Items_With_Invalid_Quantity_When_Items_Tried_To_Add_Then_It_Should_Throw_Argument_Out_Of_Range_Exception(string name, string desc, Schedule schedule, int quantity, int id, Category category, ItemStatus status, string notes, DateTime? dateadded, DateTime? datebought) {
            Assert.Throws<ArgumentOutOfRangeException>(() => { new Items(name, desc, schedule, quantity, id, category, status, notes, dateadded, datebought); });
        }
        [Theory]
        [MemberData(nameof(ItemsDataFixture.GetValidItemsForUpdate), MemberType = typeof(ItemsDataFixture))]
        public void Given_There_Are_Items_When_Updating_Items_Then_The_Properties_Should_Reassigned_With_New_Value(string oldname, string olddesc, Schedule oldschedule, int oldquantity, string newname, string newdesc, string newnote, int newquantity, Category newcategory, Schedule newschedule, ItemStatus newstatus)
        {
            // Arrange
            var item = new Items(oldname, olddesc, oldschedule, oldquantity);
            // Act
            item.Update(newname, newdesc, newnote, newquantity);
            item.ChangeCategory(newcategory);
            item.ChangeSchedule(newschedule);
            item.ChangeStatus(newstatus);
            // Assert
            Assert.Equal(newname, item.Name);
            Assert.Equal(newdesc, item.Description);
            Assert.Equal(newnote, item.Notes);
            Assert.Equal(newquantity, item.Quantity);
            Assert.Equal(newcategory, item.Category);
            Assert.Equal(newschedule, item.Schedule);
            Assert.Equal(newstatus, item.Status);
        }
        [Theory]
        [MemberData(nameof(ItemsDataFixture.GetItemsWithEmptyStringForUpdating), MemberType = typeof(ItemsDataFixture))]
        public void Given_There_Are_Invalid_Items_When_Updating_Items_Then_It_Should_Throw_Exception(string oldname, string olddesc, Schedule oldschedule, int oldquantity, string newname, string newdesc, string newnote, int newquantity)
        {
            // Arrange
            var item = new Items(oldname, olddesc, oldschedule, oldquantity);
            // Act // Assert
            Assert.Throws<ArgumentNullException>(() => item.Update(newname, newdesc, newnote, newquantity));
        }
        [Theory]
        [MemberData(nameof(ItemsDataFixture.GetItemsWithInvalidQuantityForUpdating), MemberType = typeof(ItemsDataFixture))]
        public void Given_There_Are_Valid_Items_When_Updating_Items_With_Invalid_Quantity_Then_It_Should_Throw_Exception(string oldname, string olddesc, Schedule oldschedule, int oldquantity, string newname, string newdesc, string newnote, int newquantity)
        {
            // Arrange
            var item = new Items(oldname, olddesc, oldschedule, oldquantity);
            // Act // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => item.Update(newname, newdesc, newnote, newquantity));
        }
        [Theory]
        [MemberData(nameof(ItemsDataFixture.GetItemsForMarkingBought), MemberType = typeof(ItemsDataFixture))]
        public void Given_There_Are_Valid_Items_When_Marking_As_Bought_Then_It_Should_Reassign_Status_To_Bought_And_Set_Bought_Date_As_Today(string name, string desc, Schedule schedule, int quantity, ItemStatus status, DateTime datebought)
        {
            // Arrange
            var item = new Items(name, desc, schedule, quantity);
            // Act
            item.MarkAsBought();
            // Assert
            Assert.Equal(status, item.Status);
            Assert.Equal(datebought, item.DateBought);
        }
    }
}
