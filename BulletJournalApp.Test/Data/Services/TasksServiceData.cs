using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Data.Services
{
    public class TasksServiceData
    {
        public static IEnumerable<object[]> GetValueForUpdate()
        {
            yield return new object[] { "Updated Task 1", "Updated Description", "Updated Note", false, DateTime.Today.AddDays(2) };
            yield return new object[] { "Updated Task 2", "Updated Description", "Updated Note", true, DateTime.Today.AddDays(2) };
            yield return new object[] { "Updated Task 3", "Updated Description", "Updated Note", true, DateTime.Today.AddDays(2) };
            yield return new object[] { "Updated Task 4", "Updated Description", "Updated Note", false, DateTime.Today.AddDays(2) };
        }

        public static IEnumerable<object[]> GetString() {
            yield return new object[] { "Test 1" };
            yield return new object[] { "Test 2" };
            yield return new object[] { "Test 3" };
        }
    }
}
