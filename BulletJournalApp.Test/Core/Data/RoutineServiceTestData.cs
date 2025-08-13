using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Data
{
    public class RoutineServiceTestData
    {
        public void SetUpRoutines(RoutineService service, Routines routines1, Routines routines2, Routines routines3)
        {
            service.AddRoutine(routines1);
            service.AddRoutine(routines2);
            service.AddRoutine(routines3);
        }
        public List<string> SetUpTaskList(List<string> tasklist)
        {
            tasklist.Add("Test 1");
            tasklist.Add("Test 2");
            tasklist.Add("Test 3");
            tasklist.Add("Test 4");
            tasklist.Add("Test 5");
            return tasklist;
        }

        public static List<string> SetUpTaskList2(List<string> TaskList)
        {
            TaskList.Add("Test 1");
            TaskList.Add("Test 2");
            TaskList.Add("Test 3");
            TaskList.Add("Test 4");
            TaskList.Add("Test 5");
            TaskList.Add("Test 6");
            TaskList.Add("Test 7");
            return TaskList;
        }
        public static IEnumerable<object[]> GetStringValue()
        {
            yield return new object[] { "Test 1" };
            yield return new object[] { "Test 2" };
            yield return new object[] { "Test 3" };
        }
        public static IEnumerable<object[]> GetValuesForUpdate()
        {
            yield return new object[] { "Test 1", "Updated Test", "Updated Test", "Updated note" };
            yield return new object[] { "Test 2", "Updated Test", "Updated Test", "Updated note" };
            yield return new object[] { "Test 3", "Updated Test", "Updated Test", "Updated note" };
        }
        public static IEnumerable<object[]> GetValuesForStringList()
        {
            yield return new object[] { "Test 1", SetUpTaskList2(new List<string>()) };
            yield return new object[] { "Test 2", SetUpTaskList2(new List<string>()) };
            yield return new object[] { "Test 3", SetUpTaskList2(new List<string>()) };
        }
    }
}
