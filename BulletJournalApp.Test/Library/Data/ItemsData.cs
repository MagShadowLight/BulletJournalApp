using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Library.Data
{
    public class ItemsData
    {
        public static IEnumerable<object[]> GetValidItemsForCreation()
        {
            yield return new object[] { "Test", "Test", Schedule.Monthly, 1, 0, Category.None, ItemStatus.NotBought, "", DateTime.Today, DateTime.MinValue, DateTime.Today, DateTime.MinValue };
            yield return new object[] { "Test", "Test", Schedule.Yearly, 1, 0, Category.Education, ItemStatus.NotBought, "", DateTime.Today, DateTime.MinValue, DateTime.Today, DateTime.MinValue };
            yield return new object[] { "Test", "Test", Schedule.Quarterly, 1, 0, Category.Works, ItemStatus.NotBought, "", DateTime.Today, DateTime.MinValue, DateTime.Today, DateTime.MinValue };
            yield return new object[] {"Test", "Test", Schedule.Weekly, 1, 0, Category.Home, ItemStatus.NotBought, "", DateTime.Today, DateTime.MinValue, DateTime.Today, DateTime.MinValue };
            yield return new object[] {"Test", "Test", Schedule.Daily, 1, 0, Category.Personal, ItemStatus.NotBought, "", DateTime.Today, DateTime.MinValue, DateTime.Today, DateTime.MinValue };
            yield return new object[] { "Test", "Test", Schedule.Monthly, 1, 0, Category.Financial, ItemStatus.NotBought, "Meow meow", DateTime.Today, DateTime.MinValue, DateTime.Today, DateTime.MinValue };
            yield return new object[] { "Test", "Test", Schedule.Monthly, 1, 0, Category.Transportation, ItemStatus.NotBought, "", DateTime.Today, DateTime.Today, DateTime.Today, DateTime.Today };
            yield return new object[] { "Test", "Test", Schedule.Monthly, 1, 0, Category.None, ItemStatus.NotBought, "", null, null, DateTime.Today, DateTime.MinValue };
        }
        public static IEnumerable<object[]> GetItemsWithEmptyString()
        {
            yield return new object[] { "", "Test", Schedule.Monthly, 1, 0, Category.None, ItemStatus.NotBought, "", DateTime.Today, DateTime.MinValue };
            yield return new object[] { "Test", "", Schedule.Monthly, 1, 0, Category.None, ItemStatus.NotBought, "", DateTime.Today, DateTime.MinValue };
            yield return new object[] { "", "", Schedule.Monthly, 1, 0, Category.None, ItemStatus.NotBought, "", DateTime.Today, DateTime.MinValue };
        }
        public static IEnumerable<object[]> GetItemsWithInvalidQuantityForCreation()
        {
            yield return new object[] { "Test", "Test", Schedule.Monthly, -2, 0, Category.None, ItemStatus.NotBought, "", DateTime.Today, DateTime.MinValue };
            yield return new object[] { "Test", "Test", Schedule.Monthly, -10.5, 0, Category.None, ItemStatus.NotBought, "", DateTime.Today, DateTime.MinValue };
            yield return new object[] { "Test", "Test", Schedule.Monthly, 0, 0, Category.None, ItemStatus.NotBought, "", DateTime.Today, DateTime.MinValue };
        }
        public static IEnumerable<object[]> GetValidItemsForUpdate()
        {
            yield return new object[] { "Test", "Test", Schedule.Monthly, 1, "Updated Test 1", "Updated Test", "Test 1", 5, Category.Education, Schedule.Yearly, ItemStatus.Bought };
            yield return new object[] { "Test", "Test", Schedule.Monthly, 1, "Updated Test 2", "Updated Test", "Test 2", 2, Category.Works, Schedule.Quarterly, ItemStatus.Ordered };
            yield return new object[] { "Test", "Test", Schedule.Monthly, 1, "Updated Test 2", "Updated Test", "", 7, Category.Home, Schedule.Weekly, ItemStatus.Arrived };
            yield return new object[] { "Test", "Test", Schedule.Monthly, 1, "Updated Test 2", "Updated Test", "Test 3", 15, Category.Personal, Schedule.Daily, ItemStatus.Delayed };
            yield return new object[] { "Test", "Test", Schedule.Monthly, 1, "Updated Test 2", "Updated Test", "Test 4", 9, Category.Financial, Schedule.Monthly, ItemStatus.Cancelled };
            yield return new object[] { "Test", "Test", Schedule.Monthly, 1, "Updated Test 2", "Updated Test", "Test 4", 1, Category.Transportation, Schedule.Monthly, ItemStatus.Unknown };
        }
        public static IEnumerable<object[]> GetItemsWithEmptyStringForUpdating()
        {
            yield return new object[] { "Test", "Test", Schedule.Monthly, 1, "", "Updated Test", "Test 1", 1 };
            yield return new object[] { "Test", "Test", Schedule.Monthly, 1, "Updated Test", "", "Test 1", 1 };
            yield return new object[] { "Test", "Test", Schedule.Monthly, 1, "", "", "Test 1", 1 };
        }
        public static IEnumerable<object[]> GetItemsWithInvalidQuantityForUpdating()
        {
            yield return new object[] { "Test", "Test", Schedule.Monthly, 1, "Updated Test 1", "Updated Test", "Test 1", -2 };
            yield return new object[] { "Test", "Test", Schedule.Monthly, 1, "Updated Test 2", "Updated Test", "Test 1", -10.5 };
            yield return new object[] { "Test", "Test", Schedule.Monthly, 1, "Updated Test 3", "Updated Test", "Test 1", 0 };
        }

        public static IEnumerable<object[]> GetItemsForMarkingBought()
        {
            yield return new object[] { "Test 1", "Test", Schedule.Monthly, 1, ItemStatus.Bought, DateTime.Today };
            yield return new object[] { "Test 2", "Test", Schedule.Monthly, 1, ItemStatus.Bought, DateTime.Today };
            yield return new object[] {"Test 3", "Test", Schedule.Monthly, 1, ItemStatus.Bought, DateTime.Today };
        }
    }
}
