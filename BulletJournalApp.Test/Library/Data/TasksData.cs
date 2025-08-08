using BulletJournalApp.Library.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Library.Data
{
    public class TasksData
    {
        public static IEnumerable<object[]> GetValidTestDataForCreation()
        {
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.ToDo, 0, false };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Yearly, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.ToDo, 0, false };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Quarterly, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.ToDo, 0, false };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Weekly, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.ToDo, 0, false };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Daily, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.ToDo, 0, false };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.Low, Category.None, "", TasksStatus.ToDo, 0, false };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.High, Category.None, "", TasksStatus.ToDo, 0, false };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.Education, "", TasksStatus.ToDo, 0, false };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.Works, "", TasksStatus.ToDo, 0, false };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.Home, "", TasksStatus.ToDo, 0, false };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.Personal, "", TasksStatus.ToDo, 0, false };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.Financial, "", TasksStatus.ToDo, 0, false };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.Transportation, "", TasksStatus.ToDo, 0, false };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, true, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.ToDo, 0, false };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, true, 7, DateTime.Parse("August 19, 2025"), Priority.Medium, Category.None, "", TasksStatus.ToDo, 0, false };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "Meow meow meow", TasksStatus.ToDo, 0, false };
        }

        public static IEnumerable<object[]> GetInvalidTestDataForCreation()
        {
            yield return new object[] { DateTime.Parse("July 29, 2025"), "", "Test", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.ToDo, 0, false };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "", Periodicity.Monthly, false, 7, DateTime.MinValue, Priority.Medium, Category.None, "", TasksStatus.ToDo, 0, false };
        }
        
        public static IEnumerable<object[]> GetValidTestDataForUpdate()
        {
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, false, DateTime.Parse("August 1, 2025"), "Updated test 1", "UpdatedDescription 1", false, Priority.Low, Category.Financial, TasksStatus.InProgress, Periodicity.Daily, "", 7, DateTime.MinValue };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, false, DateTime.Parse("August 7, 2025"), "Updated test 2", "UpdatedDescription 2", false, Priority.High, Category.Works, TasksStatus.Done, Periodicity.Quarterly, "", 7, DateTime.MinValue };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, false, DateTime.Parse("August 7, 2025"), "Updated test 3", "UpdatedDescription 3", true, Priority.High, Category.Personal, TasksStatus.Overdue, Periodicity.Weekly, "", 7, DateTime.MinValue };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, false, DateTime.Parse("August 7, 2025"), "Updated test 4", "UpdatedDescription 4", true, Priority.Medium, Category.Home, TasksStatus.Late, Periodicity.Yearly, "", 7, DateTime.Parse("August 21, 2025") };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, false, DateTime.Parse("August 7, 2025"), "Updated test 5", "UpdatedDescription 5", false, Priority.Medium, Category.Education, TasksStatus.Done, Periodicity.Monthly, "Meow meow meow", 7, DateTime.MinValue };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test", "Test", Periodicity.Monthly, false, DateTime.Parse("August 7, 2025"), "Updated test 6", "UpdatedDescription 6", true, Priority.Medium, Category.Education, TasksStatus.Done, Periodicity.Monthly, "", 30, DateTime.MinValue };
        }
        public static IEnumerable<object[]> GetTestDataForMarkingAndRepeating()
        {
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test 1", "Test", Periodicity.Monthly, false, 7, DateTime.MinValue, DateTime.Parse("August 5, 2025"), DateTime.Parse("September 2, 2025") };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test 2", "Test", Periodicity.Monthly, true, 7, DateTime.MinValue, DateTime.Parse("August 5, 2025"), DateTime.Parse("September 2, 2025") };
            yield return new object[] { DateTime.Parse("July 29, 2025"), "Test 2", "Test", Periodicity.Monthly, true, 28, DateTime.MinValue, DateTime.Parse("August 26, 2025"), DateTime.Parse("December 16, 2025") };
            yield return new object[] {DateTime.Parse("July 29, 2025"), "Test 3", "Test", Periodicity.Monthly, true, 7, DateTime.Parse("August 26, 2025"), DateTime.Parse("August 5, 2025"), DateTime.Parse("August 26, 2025") };
        }
        public static IEnumerable<object[]> GetTestDataForOverdue()
        {
            yield return new object[] { DateTime.Today.Subtract(TimeSpan.FromDays(1)), DateTime.Today.AddDays(1), DateTime.MinValue, "Test 1", "Test", Periodicity.Monthly, false};
            yield return new object[] { DateTime.Today.Subtract(TimeSpan.FromDays(5)), DateTime.Today.AddDays(5), DateTime.MinValue, "Test 2", "Test", Periodicity.Monthly, false};
            yield return new object[] { DateTime.Today.Subtract(TimeSpan.FromDays(30)), DateTime.Today.AddDays(30), DateTime.MinValue, "Test 2", "Test", Periodicity.Monthly, false};
        }
    }
}
