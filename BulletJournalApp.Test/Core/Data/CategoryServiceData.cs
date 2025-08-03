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
    public class CategoryServiceData
    {
        public static IEnumerable<object[]> GetCategoryAndStringValue()
        {
            yield return new object[] { "Test 1", Category.None };
            yield return new object[] { "Test 1", Category.Education };
            yield return new object[] { "Test 1", Category.Works };
            yield return new object[] { "Test 1", Category.Home };
            yield return new object[] { "Test 1", Category.Personal };
            yield return new object[] { "Test 1", Category.Financial };
            yield return new object[] { "Test 1", Category.Transportation };
            yield return new object[] { "Test 2", Category.None };
            yield return new object[] { "Test 2", Category.Education };
            yield return new object[] { "Test 2", Category.Works };
            yield return new object[] { "Test 2", Category.Home };
            yield return new object[] { "Test 2", Category.Personal };
            yield return new object[] { "Test 2", Category.Financial };
            yield return new object[] { "Test 2", Category.Transportation };
            yield return new object[] { "Test 3", Category.None };
            yield return new object[] { "Test 3", Category.Education };
            yield return new object[] { "Test 3", Category.Works };
            yield return new object[] { "Test 3", Category.Home };
            yield return new object[] { "Test 3", Category.Personal };
            yield return new object[] { "Test 3", Category.Financial };
            yield return new object[] { "Test 3", Category.Transportation };
        }

        public static IEnumerable<object[]> GetCategoryValue()
        {
            yield return new object[] { 1, Category.None };
            yield return new object[] { 1, Category.Education };
            yield return new object[] { 1, Category.Works };
            yield return new object[] { 1, Category.Home };
            yield return new object[] { 1, Category.Personal };
            yield return new object[] { 1, Category.Financial };
            yield return new object[] { 1, Category.Transportation };
        }

        public void SetUpTasks(TaskService taskService)
        {
            var task1 = new Tasks(DateTime.Today, "Test 1", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.None);
            var task2 = new Tasks(DateTime.Today, "Test 2", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.Education);
            var task3 = new Tasks(DateTime.Today, "Test 3", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.Works);
            var task4 = new Tasks(DateTime.Today, "Test 4", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.Home);
            var task5 = new Tasks(DateTime.Today, "Test 5", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.Personal);
            var task6 = new Tasks(DateTime.Today, "Test 6", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.Financial);
            var task7 = new Tasks(DateTime.Today, "Test 7", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.Transportation);
            taskService.AddTask(task1);
            taskService.AddTask(task2);
            taskService.AddTask(task3);
            taskService.AddTask(task4);
            taskService.AddTask(task5);
            taskService.AddTask(task6);
            taskService.AddTask(task7);
        }

        public void SetUpItems(ItemService itemservice)
        {
            var item1 = new Items("Test 1", "Test", Schedule.Monthly, 1, 1, Category.None);
            var item2 = new Items("Test 2", "Test", Schedule.Monthly, 1, 2, Category.Education);
            var item3 = new Items("Test 3", "Test", Schedule.Monthly, 1, 3, Category.Works);
            var item4 = new Items("Test 4", "Test", Schedule.Monthly, 1, 4, Category.Home);
            var item5 = new Items("Test 5", "Test", Schedule.Monthly, 1, 5, Category.Personal);
            var item6 = new Items("Test 6", "Test", Schedule.Monthly, 1, 6, Category.Financial);
            var item7 = new Items("Test 7", "Test", Schedule.Monthly, 1, 7, Category.Transportation);
            itemservice.AddItems(item1);
            itemservice.AddItems(item2);
            itemservice.AddItems(item3);
            itemservice.AddItems(item4);
            itemservice.AddItems(item5);
            itemservice.AddItems(item6);
            itemservice.AddItems(item7);
        }
    }
}
