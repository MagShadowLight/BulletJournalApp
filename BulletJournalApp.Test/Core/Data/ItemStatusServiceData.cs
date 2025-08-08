using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Data
{
    public class ItemStatusServiceData
    {
        public static IEnumerable<object[]> GetStatusAndStringValue()
        {
            yield return new object[] { "Test 1", ItemStatus.NotBought };
            yield return new object[] { "Test 1", ItemStatus.Bought };
            yield return new object[] { "Test 1", ItemStatus.Ordered };
            yield return new object[] { "Test 1", ItemStatus.Arrived };
            yield return new object[] { "Test 1", ItemStatus.Delayed };
            yield return new object[] { "Test 1", ItemStatus.Cancelled };
            yield return new object[] { "Test 1", ItemStatus.Unknown };
            yield return new object[] { "Test 2", ItemStatus.NotBought };
            yield return new object[] { "Test 2", ItemStatus.Bought };
            yield return new object[] { "Test 2", ItemStatus.Ordered };
            yield return new object[] { "Test 2", ItemStatus.Arrived };
            yield return new object[] { "Test 2", ItemStatus.Delayed };
            yield return new object[] { "Test 2", ItemStatus.Cancelled };
            yield return new object[] { "Test 2", ItemStatus.Unknown };
            yield return new object[] { "Test 3", ItemStatus.NotBought };
            yield return new object[] { "Test 3", ItemStatus.Bought };
            yield return new object[] { "Test 3", ItemStatus.Ordered };
            yield return new object[] { "Test 3", ItemStatus.Arrived };
            yield return new object[] { "Test 3", ItemStatus.Delayed };
            yield return new object[] { "Test 3", ItemStatus.Cancelled };
            yield return new object[] { "Test 3", ItemStatus.Unknown };
            yield return new object[] { "Test 4", ItemStatus.NotBought };
            yield return new object[] { "Test 4", ItemStatus.Bought };
            yield return new object[] { "Test 4", ItemStatus.Ordered };
            yield return new object[] { "Test 4", ItemStatus.Arrived };
            yield return new object[] { "Test 4", ItemStatus.Delayed };
            yield return new object[] { "Test 4", ItemStatus.Cancelled };
            yield return new object[] { "Test 4", ItemStatus.Unknown };
        }

        public static IEnumerable<object[]> GetStatusValue()
        {
            yield return new object[] { 1, ItemStatus.NotBought };
            yield return new object[] { 1, ItemStatus.Bought };
            yield return new object[] { 1, ItemStatus.Ordered };
            yield return new object[] { 1, ItemStatus.Arrived };
            yield return new object[] { 1, ItemStatus.Delayed };
            yield return new object[] { 1, ItemStatus.Cancelled };
            yield return new object[] { 1, ItemStatus.Unknown };
        }

        public void SetUpItems(ItemService itemService)
        {
            Items item1 = new Items("Test 1", "Test", Periodicity.Monthly, 1, 0, Category.None, ItemStatus.NotBought);
            Items item2 = new Items("Test 2", "Test", Periodicity.Monthly, 1, 0, Category.None, ItemStatus.Bought);
            Items item3 = new Items("Test 3", "Test", Periodicity.Monthly, 1, 0, Category.None, ItemStatus.Ordered);
            Items item4 = new Items("Test 4", "Test", Periodicity.Monthly, 1, 0, Category.None, ItemStatus.Arrived);
            Items item5 = new Items("Test 5", "Test", Periodicity.Monthly, 1, 0, Category.None, ItemStatus.Delayed);
            Items item6 = new Items("Test 6", "Test", Periodicity.Monthly, 1, 0, Category.None, ItemStatus.Cancelled);
            Items item7 = new Items("Test 7", "Test", Periodicity.Monthly, 1, 0, Category.None, ItemStatus.Unknown);
            itemService.AddItems(item1);
            itemService.AddItems(item2);
            itemService.AddItems(item3);
            itemService.AddItems(item4);
            itemService.AddItems(item5);
            itemService.AddItems(item6);
            itemService.AddItems(item7);
        }
    }
}
