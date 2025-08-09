using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Test.Core.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Service
{
    public class RoutineServiceTest
    {
        private RoutineService _routineservice = new();
        private RoutineServiceTestData _data = new();
        private List<string> tasklist = new();
        int num = 0;
        private Routines routine1;
        private Routines routine2;
        private Routines routine3;
        public RoutineServiceTest()
        {
            tasklist = _data.SetUpTaskList(tasklist);
            routine1 = new Routines("Test 1", "Test", Category.None, tasklist, Periodicity.Monthly, "Test Note");
            routine2 = new Routines("Test 2", "Test", Category.None, tasklist, Periodicity.Monthly, "Test Note");
            routine3 = new Routines("Test 3", "Test", Category.None, tasklist, Periodicity.Monthly, "Test Note");
        }
        [Fact]
        public void Given_There_Are_No_Routines_In_The_List_When_Adding_A_Routine_Then_It_Should_Be_Added_To_The_List()
        {
            // Arrange
            num = 3;
            // Act
            _routineservice.AddRoutine(routine1);
            _routineservice.AddRoutine(routine2);
            _routineservice.AddRoutine(routine3);
            var routines = _routineservice.GetAllRoutines();
            // Assert
            Assert.Equal(num, routines.Count);
            Assert.Contains(routine1, routines);
            Assert.Contains(routine2, routines);
            Assert.Contains(routine3, routines);
            Assert.Throws<DuplicateNameException>(() => _routineservice.AddRoutine(new Routines("Test 2", "Test", Category.None, tasklist, Periodicity.Monthly, "Test")));
            Assert.Throws<ArgumentNullException>(() => _routineservice.AddRoutine(new Routines("", "Test", Category.None, tasklist, Periodicity.Monthly, "Test")));
            Assert.Throws<ArgumentNullException>(() => _routineservice.AddRoutine(new Routines("Test", "", Category.None, tasklist, Periodicity.Monthly, "Test")));
            Assert.Throws<FormatException>(() => _routineservice.AddRoutine(new Routines("Test", "Test", Category.None, new List<string>(), Periodicity.Monthly, "Test")));
        }
        [Fact]
        public void Given_There_Are_Routines_In_The_List_When_Getting_All_Routines_Then_It_Should_Return_List_Of_Routines()
        {
            // Arrange
            num = 3;
            _data.SetUpRoutines(_routineservice, routine1, routine2, routine3);
            // Act
            var routines = _routineservice.GetAllRoutines();
            // Assert
            Assert.Equal(num, routines.Count);
            Assert.Contains(routine1, routines);
            Assert.Contains(routine2, routines);
            Assert.Contains(routine3, routines);
        }
        [Theory]
        [MemberData(nameof(RoutineServiceTestData.GetStringValue), MemberType =typeof(RoutineServiceTestData))]
        public void Given_There_Are_Routines_In_The_List_When_Searching_For_A_Routine_Then_It_Should_Return_With_That_Routine(string name)
        {
            // Arrange
            _data.SetUpRoutines(_routineservice, routine1, routine2, routine3);
            // Act
            var routine = _routineservice.FindRoutineByName(name);
            // Assert
            Assert.Equal(name, routine.Name);
        }
        [Theory]
        [MemberData(nameof(RoutineServiceTestData.GetValuesForUpdate), MemberType =typeof(RoutineServiceTestData))]
        public void Given_There_Are_Routines_In_The_List_When_Updating_The_Routine_Then_It_Should_Updated_With_New_Values(
            string oldname,
            string newname,
            string newdescription,
            string newnote)
        {
            // Arrange
            num = 3;
            _data.SetUpRoutines(_routineservice, routine1, routine2, routine3);
            // Act
            _routineservice.UpdateRoutine(oldname, newname, newdescription, newnote);
            var routines = _routineservice.GetAllRoutines();
            var routine = _routineservice.FindRoutineByName(newname);
            // Assert
            Assert.Equal(num, routines.Count);
            Assert.Equal(newname, routine.Name);
            Assert.Equal(newdescription, routine.Description);
            Assert.Equal(newnote, routine.Notes);
            Assert.Throws<ArgumentNullException>(() => _routineservice.UpdateRoutine(null, newname, newdescription, newnote));
            Assert.Throws<DuplicateNameException>(() => _routineservice.UpdateRoutine(newname, newname, newdescription, newnote));
            Assert.Throws<ArgumentNullException>(() => _routineservice.UpdateRoutine(newname, null, newdescription, newnote));
            Assert.Throws<ArgumentNullException>(() => _routineservice.UpdateRoutine(newname, oldname, null, newnote));
        }
        [Theory]
        [MemberData(nameof(RoutineServiceTestData.GetValuesForStringList), MemberType =typeof(RoutineServiceTestData))]
        public void Given_There_Are_Routines_In_The_List_When_Updating_The_Task_List_In_Routine_Then_It_Should_Updated_With_New_Values(
            string name,
            List<string> list)
        {
            // Arrange
            num = 3;
            int localnum = 7;
            _data.SetUpRoutines(_routineservice, routine1, routine2, routine3);
            // Act
            _routineservice.ChangeTaskList(name, list);
            var routines = _routineservice.GetAllRoutines();
            var routine = _routineservice.FindRoutineByName(name);
            // Assert
            Assert.Equal(num, routines.Count);
            Assert.Equal(localnum, routine.TaskList.Count);
            Assert.Throws<ArgumentNullException>(() => _routineservice.ChangeTaskList(null, tasklist));
            Assert.Throws<FormatException>(() => _routineservice.ChangeTaskList(name, new List<string>()));
        }
        [Theory]
        [MemberData(nameof(RoutineServiceTestData.GetStringValue), MemberType = typeof(RoutineServiceTestData))]
        public void Given_There_Are_Routines_In_The_List_When_Deleting_A_Routine_Then_It_Should_Be_Removed_From_The_List(string name)
        {
            // Arrange
            num = 2;
            _data.SetUpRoutines(_routineservice, routine1, routine2, routine3);
            var routine = _routineservice.FindRoutineByName(name);
            // Act
            _routineservice.DeleteRoutine(name);
            var routines = _routineservice.GetAllRoutines();
            // Assert
            Assert.Equal(num, routines.Count);
            Assert.DoesNotContain(routine, routines);
            Assert.Throws<ArgumentNullException>(() => _routineservice.DeleteRoutine(null));
        }
    }
}
