using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Data
{
    public class RoutinesFormatterTestData
    {
        public static List<string> str = new List<string>
        {
            "Meow",
            "Mrow",
            "Mrrp"
        };
        public static IEnumerable<object[]> GetRoutines()
        {
            yield return new object[] { new Routines(
                "Test 1",
                "Test",
                Category.Personal,
                str,
                Periodicity.Monthly,
                "Test",
                1) };
            yield return new object[] { new Routines(
                "Test 2",
                "Test",
                Category.Personal,
                str,
                Periodicity.Monthly,
                "Test",
                2) };
            yield return new object[] { new Routines(
                "Test 3",
                "Test",
                Category.Personal,
                str,
                Periodicity.Monthly,
                "Test",
                3) };
        }
    }
}
