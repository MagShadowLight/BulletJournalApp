using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Data
{
    public class ItemFormatterData
    {
        public static IEnumerable<object[]> GetRegularItems()
        {
            yield return new object[] { new Items(
                 "Test 1",
                 "Test",
                 Schedule.Monthly,
                 1,
                 1,
                 Category.None,
                 ItemStatus.NotBought,
                 "Test",
                 DateTime.Today,
                 DateTime.MinValue
                )
            };
            yield return new object[] { new Items(
                 "Test 2",
                 "Test",
                 Schedule.Monthly,
                 1,
                 2,
                 Category.None,
                 ItemStatus.NotBought,
                 "Test",
                 DateTime.Today,
                 DateTime.MinValue
                )
            };
            yield return new object[] { new Items(
                 "Test 3",
                 "Test",
                 Schedule.Monthly,
                 1,
                 3,
                 Category.None,
                 ItemStatus.NotBought,
                 "Test",
                 DateTime.Today,
                 DateTime.MinValue
                )
            };
        }
    }
}
