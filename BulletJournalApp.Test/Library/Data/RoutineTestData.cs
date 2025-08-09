using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Library.Data
{
    public class RoutineTestData
    {
        public static List<string> TaskList1 = SetTaskList1(new List<string>());
        public static List<string> TaskList2 = SetTaskList2(new List<string>());
        private static List<string> SetTaskList1(List<string> taskList)
        {
            taskList.Add("Test 1");
            taskList.Add("Test 2");
            taskList.Add("Test 3");
            taskList.Add("Test 4");
            taskList.Add("Test 5");
            return taskList;
        }
        private static List<string> SetTaskList2(List<string> taskList)
        {
            taskList.Add("Test 1");
            taskList.Add("Test 2");
            taskList.Add("Test 3");
            return taskList;
        }
        public static IEnumerable<object[]> GetValidRoutine()
        {
            yield return new object[] { "Test 1", "Test", Category.None, TaskList1, Periodicity.Yearly, "Test Note", "Updated Test", "Updated Test", Category.Education, TaskList2, Periodicity.Quarterly, "Updated Note" };
            yield return new object[] { "Test 2", "Test", Category.Education, TaskList1, Periodicity.Quarterly, "Test Note", "Updated Test", "Updated Test", Category.Works, TaskList2, Periodicity.Monthly, "Updated Note" };
            yield return new object[] { "Test 3", "Test", Category.Works, TaskList1, Periodicity.Monthly, "Test Note", "Updated Test", "Updated Test", Category.Home, TaskList2, Periodicity.Weekly, "Updated Note" };
            yield return new object[] { "Test 4", "Test", Category.Home, TaskList1, Periodicity.Weekly, "Test Note", "Updated Test", "Updated Test", Category.Personal, TaskList2, Periodicity.Daily, "Updated Note" };
            yield return new object[] { "Test 5", "Test", Category.Personal, TaskList1, Periodicity.Daily, "Test Note", "Updated Test", "Updated Test", Category.Financial, TaskList2, Periodicity.Yearly, "Updated Note" };
            yield return new object[] { "Test 6", "Test", Category.Financial, TaskList1, Periodicity.Yearly, "Test Note", "Updated Test", "Updated Test", Category.Transportation, TaskList2, Periodicity.Quarterly, "Updated Note" };
            yield return new object[] { "Test 7", "Test", Category.Transportation, TaskList1, Periodicity.Quarterly, "Test Note", "Updated Test", "Updated Test", Category.None, TaskList2, Periodicity.Monthly, "Updated Note" };
        }
        public static IEnumerable<object[]> GetRoutinessWithEmptyString()
        {
            yield return new object[] { "", "Test", Category.None, TaskList1, Periodicity.Monthly, "Test Note", "Test 1", "Test" };
            yield return new object[] { "Test", "", Category.None, TaskList1, Periodicity.Monthly, "Test Note", "Test 1", "Test" };
            yield return new object[] { "", "", Category.None, TaskList1, Periodicity.Monthly, "Test Note", "Test 1", "Test" };
        }
        public static IEnumerable<object[]> GetRoutinesWithEmptyList()
        {
            yield return new object[] { "Test 1", "Test", Category.None, new List<string>(), Periodicity.Monthly, "Test Note", TaskList1 };
            yield return new object[] { "Test 2", "Test", Category.None, new List<string>(), Periodicity.Monthly, "Test Note", TaskList1 };
            yield return new object[] { "Test 3", "Test", Category.None, new List<string>(), Periodicity.Monthly, "Test Note", TaskList1 };
        }
    }
}
