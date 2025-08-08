using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.UI.Data
{
    public class TaskManagerTestData
    {
        public static IEnumerable<object[]> GetScheduleUpdateInput()
        {
            yield return new object[] { Periodicity.Yearly, "14\nTest 1\nY\n0\nN\n" };
            yield return new object[] { Periodicity.Quarterly, "14\nTest 1\nQ\n0\nN\n" };
            yield return new object[] { Periodicity.Monthly, "14\nTest 1\nM\n0\nN\n" };
            yield return new object[] { Periodicity.Weekly, "14\nTest 1\nW\n0\nN\n" };
            yield return new object[] { Periodicity.Daily, "14\nTest 1\nD\n0\nN\n" };
        }
        public static IEnumerable<object[]> GetCategoryUpdateInput()
        {
            yield return new object[] { Category.None, "13\nTest 2\nN\n0\nN\n" };
            yield return new object[] { Category.Education, "13\nTest 2\nE\n0\nN\n" };
            yield return new object[] { Category.Works, "13\nTest 2\nW\n0\nN\n" };
            yield return new object[] { Category.Home, "13\nTest 2\nH\n0\nN\n" };
            yield return new object[] { Category.Personal, "13\nTest 2\nP\n0\nN\n" };
            yield return new object[] { Category.Financial, "13\nTest 2\nF\n0\nN\n" };
            yield return new object[] { Category.Transportation, "13\nTest 2\nT\n0\nN\n" };
        }
        public static IEnumerable<object[]> GetPriorityUpdateInput()
        {
            yield return new object[] { Priority.Low, "11\nTest 2\nL\n0\nN\n" };
            yield return new object[] { Priority.Medium, "11\nTest 2\nM\n0\nN\n" };
            yield return new object[] { Priority.High, "11\nTest 2\nH\n0\nN\n" };
        }
        public static IEnumerable<object[]> GetStatusUpdateInput()
        {
            yield return new object[] { TasksStatus.ToDo, "12\nTest 1\nT\n0\nN\n" };
            yield return new object[] { TasksStatus.InProgress, "12\nTest 1\nI\n0\nN\n" };
            yield return new object[] { TasksStatus.Done, "12\nTest 1\nD\n0\nN\n" };
            yield return new object[] { TasksStatus.Overdue, "12\nTest 1\nO\n0\nN\n" };
            yield return new object[] { TasksStatus.Late, "12\nTest 1\nL\n0\nN\n" };
        }
        public static IEnumerable<object[]> GetScheduleListInput()
        {
            yield return new object[] { Periodicity.Yearly, "7\nY\n0\nN\n" };
            yield return new object[] { Periodicity.Quarterly, "7\nQ\n0\nN\n" };
            yield return new object[] { Periodicity.Monthly, "7\nM\n0\nN\n" };
            yield return new object[] { Periodicity.Weekly, "7\nW\n0\nN\n" };
            yield return new object[] { Periodicity.Daily, "7\nD\n0\nN\n" };
        }
        public static IEnumerable<object[]> GetStatusListInput()
        {
            yield return new object[] { TasksStatus.ToDo, "6\nT\n0\nN\n" };
            yield return new object[] { TasksStatus.InProgress, "6\nI\n0\nN\n" };
            yield return new object[] { TasksStatus.Done, "6\nD\n0\nN\n" };
            yield return new object[] { TasksStatus.Overdue, "6\nO\n0\nN\n" };
            yield return new object[] { TasksStatus.Late, "6\nL\n0\nN\n" };
        }
        public static IEnumerable<object[]> GetPriorityListInput()
        {
            yield return new object[] { Priority.Low, "4\nL\n0\nN\n" };
            yield return new object[] { Priority.Medium, "4\nM\n0\nN\n" };
            yield return new object[] { Priority.High, "4\nH\n0\nN\n" };
        }
        public static IEnumerable<object[]> GetCategoryListInput()
        {
            yield return new object[] { Category.None, "5\nN\n0\nN\n" };
            yield return new object[] { Category.Education, "5\nE\n0\nN\n" };
            yield return new object[] { Category.Works, "5\nW\n0\nN\n" };
            yield return new object[] { Category.Home, "5\nH\n0\nN\n" };
            yield return new object[] { Category.Personal, "5\nP\n0\nN\n" };
            yield return new object[] { Category.Financial, "5\nF\n0\nN\n" };
            yield return new object[] { Category.Transportation, "5\nT\n0\nN\n" };
        }


        public List<Tasks> SetUpTasks(List<Tasks> tasks)
        {
            var task1 = new Tasks(DateTime.Now, "Test 1", "meow", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.ToDo, 0, false);
            var task2 = new Tasks(DateTime.Now, "Test 2", "mrow", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.Low, Category.Education, "", TasksStatus.InProgress, 0, true);
            var task3 = new Tasks(DateTime.Now, "Test 3", "mrrp", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.High, Category.Works, "", TasksStatus.Done, 0, false);
            var task4 = new Tasks(DateTime.Now, "Test 4", "mrrp", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.Home, "", TasksStatus.Overdue, 0, false);
            var task5 = new Tasks(DateTime.Now, "Test 5", "mrrp", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.Low, Category.Personal, "", TasksStatus.Late, 0, true);
            var task6 = new Tasks(DateTime.Now, "Test 6", "mrrp", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.High, Category.Financial, "", TasksStatus.ToDo, 0, true);
            var task7 = new Tasks(DateTime.Now, "Test 7", "mrrp", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.Transportation, "", TasksStatus.InProgress, 0, false);
            tasks.Add(task1);
            tasks.Add(task2);
            tasks.Add(task3);
            tasks.Add(task4);
            tasks.Add(task5);
            tasks.Add(task6);
            tasks.Add(task7);
            return tasks;
        }
    }
}
