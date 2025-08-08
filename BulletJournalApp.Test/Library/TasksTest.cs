using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Test.Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Library
{
    public class TasksTest
    {

        [Theory]
        [MemberData(nameof(TasksData.GetValidTestDataForCreation), MemberType =typeof(TasksData))]
        public void Given_There_Is_Valid_Values_In_Tasks_When_Tasks_Was_Added_Then_It_Should_Created_With_Values(DateTime duedate, string title, string description, Periodicity schedule, bool isrepeating, int repeatdays, DateTime endrepeatdate, Priority priority, Category category, string notes, TasksStatus status, int id, bool iscomplete)
        {
            // Arrange // Act
            var task = new Tasks(duedate, title, description, schedule, isrepeating, repeatdays, endrepeatdate, priority, category, notes, status, id, iscomplete);
            // Assert
            Assert.Equal(duedate, task.DueDate);
            Assert.Equal(title, task.Title);
            Assert.Equal(description, task.Description);
            Assert.Equal(schedule, task.schedule);
            Assert.Equal(isrepeating, task.IsRepeatable);
            Assert.Equal(repeatdays, task.RepeatDays);
            Assert.Equal(endrepeatdate, task.EndRepeatDate);
            Assert.Equal(priority, task.Priority);
            Assert.Equal(category, task.Category);
            Assert.Equal(notes, task.Notes);
            Assert.Equal(status, task.Status);
            Assert.Equal(id, task.Id);
            Assert.Equal(iscomplete, task.IsCompleted);
        }
        [Theory]
        [MemberData(nameof(TasksData.GetInvalidTestDataForCreation), MemberType = typeof(TasksData))]
        public void Given_There_Is_Invalid_Values_In_Tasks_When_Tasks_Tried_To_Create_Then_It_Should_Throw_Exception(DateTime duedate, string title, string description, Periodicity schedule, bool isrepeating, int repeatdays, DateTime endrepeatdate, Priority priority, Category category, string notes, TasksStatus status, int id, bool iscomplete)
        {
            // Assert
            Assert.Throws<ArgumentException>(() => { new Tasks(duedate, title, description, schedule, isrepeating, repeatdays, endrepeatdate, priority, category, notes, status, id, iscomplete); } );
        }
        [Theory]
        [MemberData(nameof(TasksData.GetValidTestDataForUpdate), MemberType =typeof(TasksData))]
        public void Given_There_Is_An_Existing_Tasks_When_Updating_Tasks_Then_It_Should_Update_With_New_Valuew(DateTime oldduedate, string oldtitle, string olddesc, Periodicity oldschedule, bool oldisrepeat, DateTime newduedate, string newtitle, string newdesc, bool newisrepeat, Priority newpriority, Category newcategory, TasksStatus newstatus, Periodicity newschedule, string newnote, int newrepeatdays, DateTime newendrepeatdate)
        {
            // Arrange
            var task = new Tasks(oldduedate, oldtitle, olddesc, oldschedule, oldisrepeat);
            // Act
            task.Update(newduedate, newtitle, newdesc, newisrepeat, newnote, newrepeatdays, newendrepeatdate);
            task.ChangePriority(newpriority);
            task.ChangeCategory(newcategory);
            task.ChangeStatus(newstatus);
            task.ChangeSchedule(newschedule);
            // Assert
            Assert.Equal(newduedate, task.DueDate);
            Assert.Equal(newtitle, task.Title);
            Assert.Equal(newdesc, task.Description);
            Assert.Equal(newisrepeat, task.IsRepeatable);
            Assert.Equal(newnote, task.Notes);
            Assert.Equal(newrepeatdays, task.RepeatDays);
            Assert.Equal(newendrepeatdate, task.EndRepeatDate);
            Assert.Equal(newpriority, task.Priority);
            Assert.Equal(newcategory, task.Category);
            Assert.Equal(newstatus, task.Status);
            Assert.Equal(newschedule, task.schedule);
        }
        [Theory]
        [MemberData(nameof(TasksData.GetTestDataForMarkingAndRepeating), MemberType =typeof(TasksData))]
        public void Given_There_Are_Tasks_When_Marking_As_Complete_Then_Is_Complete_Should_Set_To_True(DateTime duedate, string title, string desc, Periodicity schedule, bool isrepeat, int repeatdays, DateTime endrepeatdate, DateTime expectedvalue1, DateTime expectedvalue2)
        {
            // Arrange
            var task = new Tasks(duedate, title, desc, schedule, isrepeat, repeatdays, endrepeatdate);
            // Act
            task.MarkComplete();
            // Assert
            Assert.True(task.IsCompleted);
        }
        [Theory]
        [MemberData(nameof(TasksData.GetTestDataForMarkingAndRepeating), MemberType =typeof(TasksData))]
        public void Given_There_Are_Tasks_When_Repeating_The_Tasks_Then_DueDate_And_IsCompleted_Should_Be_Reassigned(DateTime duedate, string title, string desc, Periodicity schedule, bool isrepeat, int repeatdays, DateTime endrepeatdate, DateTime expectedvalue1, DateTime expectedvalue2)
        {
            // Arrange
            var task = new Tasks(duedate, title, desc, schedule, isrepeat, repeatdays, endrepeatdate);
            // Act
            task.RepeatTask();
            // Assert
            Assert.Equal(expectedvalue1, task.DueDate);
        }
        [Theory]
        [MemberData(nameof(TasksData.GetTestDataForMarkingAndRepeating), MemberType =typeof(TasksData))]
        public void Given_There_Are_Tasks_With_End_Repeat_Date_When_Repeating_The_Tasks_Over_The_End_Repeat_Date_Then_DueDate_And_IsCompleted_Should_Not_Be_Reassigned(DateTime duedate, string title, string desc, Periodicity schedule, bool isrepeat, int repeatdays, DateTime endrepeatdate, DateTime expectedvalue1, DateTime expectedvalue2)
        {
            // Arrange
            var task = new Tasks(duedate, title, desc, schedule, isrepeat, repeatdays, endrepeatdate);
            // Act
            task.RepeatTask();
            task.RepeatTask();
            task.RepeatTask();
            task.RepeatTask();
            task.RepeatTask();
            // Assert
            Assert.Equal(expectedvalue2, task.DueDate);
        }
        [Theory]
        [MemberData(nameof(TasksData.GetTestDataForOverdue), MemberType=typeof(TasksData))]
        public void Given_There_Are_Tasks_With_Past_Due_Date_When_Is_Overdue_Method_Run_Then_It_Should_Return_True(DateTime duedate1, DateTime duedate2, DateTime duedate3, string title, string desc, Periodicity schedule, bool isrepeat)
        {
            // Arrange
            var task = new Tasks(duedate1, title, desc, schedule, isrepeat);
            // Act
            bool overdue = task.IsOverdue();
            // Assert
            Assert.True(overdue);
        }
        [Theory]
        [MemberData(nameof(TasksData.GetTestDataForOverdue), MemberType=typeof(TasksData))]
        public void Given_There_Are_Tasks_With_Future_Due_Date_When_Is_Overdue_Method_Run_Then_It_Should_Return_True(DateTime duedate1, DateTime duedate2, DateTime duedate3, string title, string desc, Periodicity schedule, bool isrepeat)
        {
            // Arrange
            var task = new Tasks(duedate2, title, desc, schedule, isrepeat);
            // Act
            bool overdue = task.IsOverdue();
            // Assert
            Assert.False(overdue);
        }
        [Theory]
        [MemberData(nameof(TasksData.GetTestDataForOverdue), MemberType=typeof(TasksData))]
        public void Given_There_Are_Tasks_With_No_Due_Date_When_Is_Overdue_Method_Run_Then_It_Should_Return_True(DateTime duedate1, DateTime duedate2, DateTime duedate3, string title, string desc, Periodicity schedule, bool isrepeat)
        {
            // Arrange
            var task = new Tasks(duedate3, title, desc, schedule, isrepeat);
            // Act
            bool overdue = task.IsOverdue();
            // Assert
            Assert.False(overdue);
        }
        [Theory]
        [MemberData(nameof(TasksData.GetTestDataForOverdue), MemberType=typeof(TasksData))]
        public void Given_There_Are_Completed_Tasks_When_Is_Overdue_Method_Run_Then_It_Should_Return_True(DateTime duedate1, DateTime duedate2, DateTime duedate3, string title, string desc, Periodicity schedule, bool isrepeat)
        {
            // Arrange
            var task = new Tasks(duedate3, title, desc, schedule, isrepeat);
            // Act
            task.MarkComplete();
            bool overdue = task.IsOverdue();
            // Assert
            Assert.False(overdue);
        }
    }
}
