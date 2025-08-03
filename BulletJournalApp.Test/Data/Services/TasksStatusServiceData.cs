using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Data.Services
{
    public class TasksStatusServiceData
    {

        public static IEnumerable<object[]> GetStatusAndStringValue()
        {
            yield return new object[] { "Test 1", TasksStatus.ToDo };
            yield return new object[] { "Test 1", TasksStatus.InProgress };
            yield return new object[] { "Test 1", TasksStatus.Done };
            yield return new object[] { "Test 1", TasksStatus.Overdue };
            yield return new object[] { "Test 1", TasksStatus.Late };
            yield return new object[] { "Test 2", TasksStatus.ToDo };
            yield return new object[] { "Test 2", TasksStatus.InProgress };
            yield return new object[] { "Test 2", TasksStatus.Done };
            yield return new object[] { "Test 2", TasksStatus.Overdue };
            yield return new object[] { "Test 2", TasksStatus.Late };
            yield return new object[] { "Test 3", TasksStatus.ToDo };
            yield return new object[] { "Test 3", TasksStatus.InProgress };
            yield return new object[] { "Test 3", TasksStatus.Done };
            yield return new object[] { "Test 3", TasksStatus.Overdue };
            yield return new object[] { "Test 3", TasksStatus.Late };
            yield return new object[] { "Test 4", TasksStatus.ToDo };
            yield return new object[] { "Test 4", TasksStatus.InProgress };
            yield return new object[] { "Test 4", TasksStatus.Done };
            yield return new object[] { "Test 4", TasksStatus.Overdue };
            yield return new object[] { "Test 4", TasksStatus.Late };
        }

        public static IEnumerable<object[]> GetStatusValue()
        {
            yield return new object[] { 1, TasksStatus.ToDo };
            yield return new object[] { 1, TasksStatus.InProgress };
            yield return new object[] { 1, TasksStatus.Done };
            yield return new object[] { 1, TasksStatus.Overdue };
            yield return new object[] { 1, TasksStatus.Late };
        }


        public void SetUpTasks(TaskService taskService)
        {
            var task1 = new Tasks(DateTime.Today, "Test 1", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.ToDo);
            var task2 = new Tasks(DateTime.Today, "Test 2", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.InProgress);
            var task3 = new Tasks(DateTime.Today, "Test 3", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.Done);
            var task4 = new Tasks(DateTime.Today, "Test 4", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.Overdue);
            var task5 = new Tasks(DateTime.Today, "Test 5", "Test", Schedule.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.Late);
            taskService.AddTask(task1);
            taskService.AddTask(task2);
            taskService.AddTask(task3);
            taskService.AddTask(task4);
            taskService.AddTask(task5);
        }
    }
}
