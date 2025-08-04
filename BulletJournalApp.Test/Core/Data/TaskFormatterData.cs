using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Data
{
    public class TaskFormatterData
    {
        public static IEnumerable<object[]> GetNormalTasks()
        {
            yield return new object[] { new Tasks(
                DateTime.Today.AddDays(1),
                "Test 1",
                "Test",
                Schedule.Monthly,
                false,
                7,
                DateTime.MinValue,
                Priority.Medium,
                Category.None,
                "Test",
                TasksStatus.ToDo,
                0,
                false
                )
            };
            yield return new object[] { new Tasks(
                DateTime.Today.AddDays(7),
                "Test 2",
                "Test",
                Schedule.Daily,
                false,
                7,
                DateTime.MinValue,
                Priority.Low,
                Category.Personal,
                "Test 2",
                TasksStatus.Late,
                1,
                false
                ) 
            };
            yield return new object[] { new Tasks(
                DateTime.Today.AddDays(3),
                "Test 3",
                "Test",
                Schedule.Monthly,
                false,
                7,
                DateTime.MinValue,
                Priority.High,
                Category.Transportation,
                "Test 3",
                TasksStatus.Done,
                2,
                false
                )
            };
        }
        public static IEnumerable<object[]> GetCompleteTask()
        {
            yield return new object[] { new Tasks(
                DateTime.Today.AddDays(1),
                "Test 1",
                "Test",
                Schedule.Monthly,
                false,
                7,
                DateTime.MinValue,
                Priority.Medium,
                Category.None,
                "Test",
                TasksStatus.ToDo,
                0,
                true
                )
            };
            yield return new object[] { new Tasks(
                DateTime.Today.AddDays(1),
                "Test 2",
                "Test",
                Schedule.Monthly,
                false,
                7,
                DateTime.MinValue,
                Priority.Medium,
                Category.None,
                "Test",
                TasksStatus.ToDo,
                0,
                true
                )
            };
            yield return new object[] { new Tasks(
                DateTime.Today.AddDays(1),
                "Test 3",
                "Test",
                Schedule.Monthly,
                false,
                7,
                DateTime.MinValue,
                Priority.Medium,
                Category.None,
                "Test",
                TasksStatus.ToDo,
                0,
                true
                )
            };
        }
        public static IEnumerable<object[]> GetRepeatingTask()
        {
            yield return new object[] { new Tasks(
                DateTime.Today.AddDays(1),
                "Test 1",
                "Test",
                Schedule.Monthly,
                true,
                7,
                DateTime.MinValue,
                Priority.Medium,
                Category.None,
                "Test",
                TasksStatus.ToDo,
                0,
                false
                )
            };
            yield return new object[] { new Tasks(
                DateTime.Today.AddDays(1),
                "Test 2",
                "Test",
                Schedule.Monthly,
                true,
                7,
                DateTime.MinValue,
                Priority.Medium,
                Category.None,
                "Test",
                TasksStatus.ToDo,
                0,
                false
                )
            };
            yield return new object[] { new Tasks(
                DateTime.Today.AddDays(1),
                "Test 3",
                "Test",
                Schedule.Monthly,
                true,
                7,
                DateTime.MinValue,
                Priority.Medium,
                Category.None,
                "Test",
                TasksStatus.ToDo,
                0,
                false
                )
            };
        }
        public static IEnumerable<object[]> GetRepeatingTaskWithEndDate()
        {
            yield return new object[] { new Tasks(
                DateTime.Today.AddDays(1),
                "Test 1",
                "Test",
                Schedule.Monthly,
                true,
                7,
                DateTime.Today.AddDays(3),
                Priority.Medium,
                Category.None,
                "Test",
                TasksStatus.ToDo,
                0,
                false
                )
            };
            yield return new object[] { new Tasks(
                DateTime.Today.AddDays(1),
                "Test 2",
                "Test",
                Schedule.Monthly,
                true,
                7,
                DateTime.Today.AddDays(3),
                Priority.Medium,
                Category.None,
                "Test",
                TasksStatus.ToDo,
                0,
                false
                )
            };
            yield return new object[] { new Tasks(
                DateTime.Today.AddDays(1),
                "Test 3",
                "Test",
                Schedule.Monthly,
                true,
                7,
                DateTime.Today.AddDays(3),
                Priority.Medium,
                Category.None,
                "Test",
                TasksStatus.ToDo,
                0,
                false
                )
            };
        }
    }
}
