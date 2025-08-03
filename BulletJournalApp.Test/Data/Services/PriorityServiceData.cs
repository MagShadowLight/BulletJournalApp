using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Data.Services
{
    public class PriorityServiceData
    {
        public static IEnumerable<object[]> GetStringAndPriorityValue()
        {
            yield return new object[] { "Test 1", Priority.Low };
            yield return new object[] { "Test 1", Priority.Medium };
            yield return new object[] { "Test 1", Priority.High };
            yield return new object[] { "Test 2", Priority.Low };
            yield return new object[] { "Test 2", Priority.Medium };
            yield return new object[] { "Test 2", Priority.High };
            yield return new object[] { "Test 3", Priority.Low };
            yield return new object[] { "Test 3", Priority.Medium };
            yield return new object[] { "Test 3", Priority.High };
            yield return new object[] { "Test 4", Priority.Low };
            yield return new object[] { "Test 4", Priority.Medium };
            yield return new object[] { "Test 4", Priority.High };
            yield return new object[] { "Test 5", Priority.Low };
            yield return new object[] { "Test 5", Priority.Medium };
            yield return new object[] { "Test 5", Priority.High };
        }

        public static IEnumerable<object[]> GetPriorityValue()
        {
            yield return new object[] { Priority.High, 1 };
            yield return new object[] { Priority.Medium, 3 };
            yield return new object[] { Priority.Low, 1 };
        }
    }
}
