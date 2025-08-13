using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.UI.Data
{
    public class UserInputTestData
    {
        public static IEnumerable<object[]> GetBooleanOutput()
        {
            yield return new object[] { "Y", true };
            yield return new object[] { "N", false };
        }
        public static IEnumerable<object[]> GetPriorityOutput()
        {
            yield return new object[] { "L", Priority.Low };
            yield return new object[] { "M", Priority.Medium };
            yield return new object[] { "H", Priority.High };
        }
        public static IEnumerable<object[]> GetCategoryOutput()
        {
            yield return new object[] { "N", Category.None };
            yield return new object[] { "E", Category.Education };
            yield return new object[] { "W", Category.Works };
            yield return new object[] { "H", Category.Home };
            yield return new object[] { "P", Category.Personal };
            yield return new object[] { "F", Category.Financial };
            yield return new object[] { "T", Category.Transportation };
        }
        public static IEnumerable<object[]> GetScheduleOutput()
        {
            yield return new object[] { "Y", Periodicity.Yearly };
            yield return new object[] { "Q", Periodicity.Quarterly };
            yield return new object[] { "M", Periodicity.Monthly };
            yield return new object[] { "W", Periodicity.Weekly };
            yield return new object[] { "D", Periodicity.Daily };
        }
        public static IEnumerable<object[]> GetTaskStatusOutput()
        {
            yield return new object[] { "T", TasksStatus.ToDo };
            yield return new object[] { "I", TasksStatus.InProgress };
            yield return new object[] { "D", TasksStatus.Done };
            yield return new object[] { "O", TasksStatus.Overdue };
            yield return new object[] { "L", TasksStatus.Late };
        }
        public static IEnumerable<object[]> GetItemStatusOutput()
        {
            yield return new object[] { "N", ItemStatus.NotBought };
            yield return new object[] { "B", ItemStatus.Bought };
            yield return new object[] { "O", ItemStatus.Ordered };
            yield return new object[] { "A", ItemStatus.Arrived };
            yield return new object[] { "D", ItemStatus.Delayed };
            yield return new object[] { "C", ItemStatus.Cancelled };
            yield return new object[] { "Test", ItemStatus.Unknown };
        }
        public static IEnumerable<object[]> GetOptionalDateOutput()
        {
            yield return new object[] { DateTime.Today.ToString(), DateTime.Today };
            yield return new object[] { "Test", DateTime.MinValue };
        }
        public static IEnumerable<object[]> GetTimeOfDayOutput()
        {
            yield return new object[] { "B", TimeOfDay.Breakfast };
            yield return new object[] { "L", TimeOfDay.Lunch };
            yield return new object[] { "DI", TimeOfDay.Dinner };
            yield return new object[] { "S", TimeOfDay.Snacks };
            yield return new object[] { "DE", TimeOfDay.Dessert };
            yield return new object[] { "Test", TimeOfDay.None };
        }
        
    }
}
