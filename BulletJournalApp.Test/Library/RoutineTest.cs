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
    public class RoutineTest
    {
        [Theory]
        [MemberData(nameof(RoutineTestData.GetValidRoutine), MemberType =typeof(RoutineTestData))]
        public void Given_There_Are_Valid_Values_When_Routine_Was_Added_Then_It_Should_Assigned_With_Those_Value(
            string name,
            string description, 
            Category category, 
            List<string> tasklist, 
            Periodicity periodicity, 
            string note, 
            string newname,
            string newdescription,
            Category newcategory,
            List<string> newtasklist,
            Periodicity newperiodicity,
            string newnote)
        {
            // Arrange // Act
            var routine = new Routines(name, description, category, tasklist, periodicity, note);
            // Assert
            Assert.Equal(name, routine.Name);
            Assert.Equal(description, routine.Description);
            Assert.Equal(category, routine.Category);
            Assert.Equal(tasklist, routine.TaskList);
            Assert.Equal(periodicity, routine.Periodicity);
            Assert.Equal(note, routine.Notes);
        }
        [Theory]
        [MemberData(nameof(RoutineTestData.GetRoutinessWithEmptyString), MemberType =typeof(RoutineTestData))]
        public void Given_There_Are_Values_With_Empty_Name_Or_Description_When_Routines_Was_Tried_To_Add_Then_It_Should_Throw_Exception(
            string name,
            string description,
            Category category,
            List<string> tasklist,
            Periodicity periodicity,
            string note,
            string fakename,
            string fakedescription)
        {
            Assert.Throws<ArgumentNullException>(() => new Routines(name, description, category, tasklist, periodicity, note));
        }
        [Theory]
        [MemberData(nameof(RoutineTestData.GetRoutinesWithEmptyList), MemberType =typeof(RoutineTestData))]
        public void Given_There_Are_Values_With_Empty_Task_List_When_Routines_Was_Tried_To_Add_Then_It_Should_Throw_Exception(
            string name,
            string description,
            Category category,
            List<string> tasklist,
            Periodicity periodicity,
            string note,
            List<string> faketasklist)
        {
            Assert.Throws<FormatException>(() => new Routines(name, description, category, tasklist, periodicity, note));
        }
        [Theory]
        [MemberData(nameof(RoutineTestData.GetValidRoutine), MemberType = typeof(RoutineTestData))]
        public void Given_There_Are_Valid_Values_When_Routine_Was_Updated_Then_It_Should_Assigned_With_New_Value(
            string name,
            string description,
            Category category,
            List<string> tasklist,
            Periodicity periodicity,
            string note,
            string newname,
            string newdescription,
            Category newcategory,
            List<string> newtasklist,
            Periodicity newperiodicity,
            string newnote)
        {
            // Arrange 
            var routine = new Routines(name, description, category, tasklist, periodicity, note);
            // Act
            routine.UpdateRoutine(newname, newdescription, newnote);
            routine.ChangeCategory(newcategory);
            routine.ChangePeriodicity(newperiodicity);
            routine.ChangeTaskList(newtasklist);
            // Assert
            Assert.Equal(newname, routine.Name);
            Assert.Equal(newdescription, routine.Description);
            Assert.Equal(newcategory, routine.Category);
            Assert.Equal(newtasklist, routine.TaskList);
            Assert.Equal(newperiodicity, routine.Periodicity);
            Assert.Equal(newnote, routine.Notes);
        }
        [Theory]
        [MemberData(nameof(RoutineTestData.GetRoutinessWithEmptyString), MemberType = typeof(RoutineTestData))]
        public void Given_There_Are_Values_With_Empty_Name_Or_Description_When_Routines_Was_Tried_To_Update_Then_It_Should_Throw_Exception(
            string name,
            string description,
            Category category,
            List<string> tasklist,
            Periodicity periodicity,
            string note,
            string fakename,
            string fakedescription)
        {
            // Arrange
            var routine = new Routines(fakename, fakedescription, category, tasklist, periodicity, note);
            // Act // Assert
            Assert.Throws<ArgumentNullException>(() => routine.UpdateRoutine(name, description, note));
        }
        [Theory]
        [MemberData(nameof(RoutineTestData.GetRoutinesWithEmptyList), MemberType = typeof(RoutineTestData))]
        public void Given_There_Are_Values_With_Empty_Task_List_When_Routines_Was_Tried_To_Update_Then_It_Should_Throw_Exception(
            string name,
            string description,
            Category category,
            List<string> tasklist,
            Periodicity periodicity,
            string note,
            List<string> faketasklist)
        {
            // Arrange
            var routine = new Routines(name, description, category, faketasklist, periodicity, note);
            // Act // Assert
            Assert.Throws<FormatException>(() => routine.ChangeTaskList(tasklist));
        }
    }
}
