using BulletJournalApp.Core.Interface;
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

        public void SetUpTasks(TaskService taskService)
        {
            var task1 = new Tasks(DateTime.Today, "Test 1", "Test", Schedule.Monthly, false);
            var task2 = new Tasks(DateTime.Today, "Test 2", "Test", Schedule.Monthly, false);
            var task3 = new Tasks(DateTime.Today, "Test 3", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.High);
            var task4 = new Tasks(DateTime.Today, "Test 4", "Test", Schedule.Monthly, false);
            var task5 = new Tasks(DateTime.Today, "Test 5", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Low);
            taskService.AddTask(task1);
            taskService.AddTask(task2);
            taskService.AddTask(task3);
            taskService.AddTask(task4);
            taskService.AddTask(task5);
        }
    }
}
