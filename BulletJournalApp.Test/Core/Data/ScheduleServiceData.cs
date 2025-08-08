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
    public class ScheduleServiceData
    {
        public static IEnumerable<object[]> GetScheduleAndStringValue()
        {
            yield return new object[] { "Test 1", Periodicity.Daily };
            yield return new object[] { "Test 1", Periodicity.Weekly };
            yield return new object[] { "Test 1", Periodicity.Monthly };
            yield return new object[] { "Test 1", Periodicity.Quarterly };
            yield return new object[] { "Test 1", Periodicity.Yearly };
            yield return new object[] { "Test 2", Periodicity.Daily };
            yield return new object[] { "Test 2", Periodicity.Weekly };
            yield return new object[] { "Test 2", Periodicity.Monthly };
            yield return new object[] { "Test 2", Periodicity.Quarterly };
            yield return new object[] { "Test 2", Periodicity.Yearly };
            yield return new object[] { "Test 3", Periodicity.Daily };
            yield return new object[] { "Test 3", Periodicity.Weekly };
            yield return new object[] { "Test 3", Periodicity.Monthly };
            yield return new object[] { "Test 3", Periodicity.Quarterly };
            yield return new object[] { "Test 3", Periodicity.Yearly };
            yield return new object[] { "Test 4", Periodicity.Daily };
            yield return new object[] { "Test 4", Periodicity.Weekly };
            yield return new object[] { "Test 4", Periodicity.Monthly };
            yield return new object[] { "Test 4", Periodicity.Quarterly };
            yield return new object[] { "Test 4", Periodicity.Yearly };
        }

        public static IEnumerable<object[]> GetScheduleValue()
        {
            yield return new object[] { 1, Periodicity.Daily };
            yield return new object[] { 1, Periodicity.Weekly };
            yield return new object[] { 1, Periodicity.Monthly };
            yield return new object[] { 1, Periodicity.Quarterly };
            yield return new object[] { 1, Periodicity.Yearly };
        }

        public void SetUpTasks(TaskService taskService)
        {
            var task1 = new Tasks(DateTime.Today, "Test 1", "Test", Periodicity.Daily, false);
            var task2 = new Tasks(DateTime.Today, "Test 2", "Test", Periodicity.Weekly, false);
            var task3 = new Tasks(DateTime.Today, "Test 3", "Test", Periodicity.Monthly, false);
            var task4 = new Tasks(DateTime.Today, "Test 4", "Test", Periodicity.Quarterly, false);
            var task5 = new Tasks(DateTime.Today, "Test 5", "Test", Periodicity.Yearly, false);
            taskService.AddTask(task1);
            taskService.AddTask(task2);
            taskService.AddTask(task3);
            taskService.AddTask(task4);
            taskService.AddTask(task5);
        }

        public void SetUpItems(ItemService itemservice)
        {
            var item1 = new Items("Test 1", "Test", Periodicity.Daily, 1);
            var item2 = new Items("Test 2", "Test", Periodicity.Weekly, 1);
            var item3 = new Items("Test 3", "Test", Periodicity.Monthly, 1);
            var item4 = new Items("Test 4", "Test", Periodicity.Quarterly, 1);
            var item5 = new Items("Test 5", "Test", Periodicity.Yearly, 1);
            itemservice.AddItems(item1);
            itemservice.AddItems(item2);
            itemservice.AddItems(item3);
            itemservice.AddItems(item4);
            itemservice.AddItems(item5);
        }
    }
}
