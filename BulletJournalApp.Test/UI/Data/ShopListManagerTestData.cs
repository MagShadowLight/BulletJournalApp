using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.UI.Data
{
    public class ShopListManagerTestData
    {
        public static IEnumerable<object[]> GetCategoryListInput()
        {
            yield return new object[] { Category.None, "5\nC\nN\n0\n" };
            yield return new object[] { Category.Education, "5\nC\nE\n0\n" };
            yield return new object[] { Category.Works, "5\nC\nW\n0\n" };
            yield return new object[] { Category.Home, "5\nC\nH\n0\n" };
            yield return new object[] { Category.Personal, "5\nC\nP\n0\n" };
            yield return new object[] { Category.Financial, "5\nC\nF\n0\n" };
            yield return new object[] { Category.Transportation, "5\nC\nT\n0\n" };
        }
        public static IEnumerable<object[]> GetCategoryUpdateInput()
        {
            yield return new object[] { Category.None, "9\nC\nTest 2\nN\n0" };
            yield return new object[] { Category.Education, "9\nC\nTest 2\nE\n0" };
            yield return new object[] { Category.Works, "9\nC\nTest 2\nW\n0" };
            yield return new object[] { Category.Home, "9\nC\nTest 2\nH\n0" };
            yield return new object[] { Category.Personal, "9\nC\nTest 2\nP\n0" };
            yield return new object[] { Category.Financial, "9\nC\nTest 2\nF\n0" };
            yield return new object[] { Category.Transportation, "9\nC\nTest 2\nT\n0" };
        }
        public static IEnumerable<object[]> GetStatusListInput()
        {
            yield return new object[] { ItemStatus.NotBought, "5\nSt\nN\n0\n" };
            yield return new object[] { ItemStatus.Bought, "5\nSt\nB\n0\n" };
            yield return new object[] { ItemStatus.Ordered, "5\nSt\nO\n0\n" };
            yield return new object[] { ItemStatus.Arrived, "5\nSt\nA\n0\n" };
            yield return new object[] { ItemStatus.Delayed, "5\nSt\nD\n0\n" };
            yield return new object[] { ItemStatus.Cancelled, "5\nSt\nC\n0\n" };
            yield return new object[] { ItemStatus.Unknown, "5\nSt\nU\n0\n" };
        }
        public static IEnumerable<object[]> GetStatusUpdateInput()
        {
            yield return new object[] { ItemStatus.NotBought, "9\nSt\nTest 2\nN\n0" };
            yield return new object[] { ItemStatus.Bought, "9\nSt\nTest 2\nB\n0" };
            yield return new object[] { ItemStatus.Ordered, "9\nSt\nTest 2\nO\n0" };
            yield return new object[] { ItemStatus.Arrived, "9\nSt\nTest 2\nA\n0" };
            yield return new object[] { ItemStatus.Delayed, "9\nSt\nTest 2\nD\n0" };
            yield return new object[] { ItemStatus.Cancelled, "9\nSt\nTest 2\nC\n0" };
            yield return new object[] { ItemStatus.Unknown, "9\nSt\nTest 2\nU\n0" };
        }
        public static IEnumerable<object[]> GetScheduleListInput()
        {
            yield return new object[] { Schedule.Yearly, "5\nSc\nY\n0\n" };
            yield return new object[] { Schedule.Quarterly, "5\nSc\nQ\n0\n" };
            yield return new object[] { Schedule.Monthly, "5\nSc\nM\n0\n" };
            yield return new object[] { Schedule.Weekly, "5\nSc\nW\n0\n" };
            yield return new object[] { Schedule.Daily, "5\nSc\nD\n0\n" };
        }
        public static IEnumerable<object[]> GetScheduleUpdateInput()
        {
            yield return new object[] { Schedule.Yearly, "9\nSc\nTest 2\nY\n0" };
            yield return new object[] { Schedule.Quarterly, "9\nSc\nTest 2\nQ\n0" };
            yield return new object[] { Schedule.Monthly, "9\nSc\nTest 2\nM\n0" };
            yield return new object[] { Schedule.Weekly, "9\nSc\nTest 2\nW\n0" };
            yield return new object[] { Schedule.Daily, "9\nSc\nTest 2\nD\n0" };
        }

        public List<Items> SetUpItems(List<Items> items)
        {
            var item1 = new Items("Test 1", "Test", Schedule.Yearly, 1, 1, Category.None, ItemStatus.NotBought, "", DateTime.Today, DateTime.MinValue);
            var item2 = new Items("Test 2", "Test", Schedule.Quarterly, 1, 2, Category.Education, ItemStatus.Bought, "", DateTime.Today, DateTime.Today.AddDays(1));
            var item3 = new Items("Test 3", "Test", Schedule.Monthly, 1, 3, Category.Works, ItemStatus.Ordered, "", DateTime.Today, DateTime.MinValue);
            var item4 = new Items("Test 4", "Test", Schedule.Weekly, 1, 4, Category.Home, ItemStatus.Arrived, "", DateTime.Today, DateTime.MinValue);
            var item5 = new Items("Test 5", "Test", Schedule.Daily, 1, 5, Category.Personal, ItemStatus.Delayed, "", DateTime.Today, DateTime.MinValue);
            var item6 = new Items("Test 6", "Test", Schedule.Yearly, 1, 6, Category.Financial, ItemStatus.Cancelled, "", DateTime.Today, DateTime.MinValue);
            var item7 = new Items("Test 7", "Test", Schedule.Quarterly, 1, 7, Category.Transportation, ItemStatus.Unknown, "", DateTime.Today, DateTime.MinValue);
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            items.Add(item4);
            items.Add(item5);
            items.Add(item6);
            items.Add(item7);
            return items;
        }
    }
}
