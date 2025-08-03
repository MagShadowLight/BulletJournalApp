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
            yield return new object[] { "Test 1", Schedule.Daily };
            yield return new object[] { "Test 1", Schedule.Weekly };
            yield return new object[] { "Test 1", Schedule.Monthly };
            yield return new object[] { "Test 1", Schedule.Quarterly };
            yield return new object[] { "Test 1", Schedule.Yearly };
            yield return new object[] { "Test 2", Schedule.Daily };
            yield return new object[] { "Test 2", Schedule.Weekly };
            yield return new object[] { "Test 2", Schedule.Monthly };
            yield return new object[] { "Test 2", Schedule.Quarterly };
            yield return new object[] { "Test 2", Schedule.Yearly };
            yield return new object[] { "Test 3", Schedule.Daily };
            yield return new object[] { "Test 3", Schedule.Weekly };
            yield return new object[] { "Test 3", Schedule.Monthly };
            yield return new object[] { "Test 3", Schedule.Quarterly };
            yield return new object[] { "Test 3", Schedule.Yearly };
            yield return new object[] { "Test 4", Schedule.Daily };
            yield return new object[] { "Test 4", Schedule.Weekly };
            yield return new object[] { "Test 4", Schedule.Monthly };
            yield return new object[] { "Test 4", Schedule.Quarterly };
            yield return new object[] { "Test 4", Schedule.Yearly };
        }

        public static IEnumerable<object[]> GetScheduleValue()
        {
            yield return new object[] { 1, Schedule.Daily };
            yield return new object[] { 1, Schedule.Weekly };
            yield return new object[] { 1, Schedule.Monthly };
            yield return new object[] { 1, Schedule.Quarterly };
            yield return new object[] { 1, Schedule.Yearly };
        }

        public void SetUpTasks(TaskService taskService)
        {
            var task1 = new Tasks(DateTime.Today, "Test 1", "Test", Schedule.Daily, false);
            var task2 = new Tasks(DateTime.Today, "Test 2", "Test", Schedule.Weekly, false);
            var task3 = new Tasks(DateTime.Today, "Test 3", "Test", Schedule.Monthly, false);
            var task4 = new Tasks(DateTime.Today, "Test 4", "Test", Schedule.Quarterly, false);
            var task5 = new Tasks(DateTime.Today, "Test 5", "Test", Schedule.Yearly, false);
            taskService.AddTask(task1);
            taskService.AddTask(task2);
            taskService.AddTask(task3);
            taskService.AddTask(task4);
            taskService.AddTask(task5);
        }

        public void SetUpItems(ItemService itemservice)
        {
            var item1 = new Items("Test 1", "Test", Schedule.Daily, 1);
            var item2 = new Items("Test 2", "Test", Schedule.Weekly, 1);
            var item3 = new Items("Test 3", "Test", Schedule.Monthly, 1);
            var item4 = new Items("Test 4", "Test", Schedule.Quarterly, 1);
            var item5 = new Items("Test 5", "Test", Schedule.Yearly, 1);
            itemservice.AddItems(item1);
            itemservice.AddItems(item2);
            itemservice.AddItems(item3);
            itemservice.AddItems(item4);
            itemservice.AddItems(item5);
        }
    }
}
