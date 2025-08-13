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
    public class ItemServiceData
    {
        public static IEnumerable<object[]> GetStringValue()
        {
            yield return new object[] { "Test 1" };
            yield return new object[] { "Test 2" };
            yield return new object[] { "Test 3" };
        }
        public static IEnumerable<object[]> GetValuesForUpdate()
        {
            yield return new object[] { "Test 1", "Updated Test", "Updated Description", "Updated Note", 3 };
            yield return new object[] { "Test 2", "Updated Test", "Updated Description", "Updated Note", 7 };
            yield return new object[] { "Test 3", "Updated Test", "Updated Description", "Updated Note", 5 };
        }
        public void SetUpItems(ItemService itemService, Items item1, Items item2, Items item3)
        {
            itemService.AddItems(item1);
            itemService.AddItems(item2);
            itemService.AddItems(item3);
        }
    }
}
