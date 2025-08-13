using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.UI.Data
{
    public class RoutineManagerTestData
    {
        private List<string> list = new();


        public static IEnumerable<object[]> GetLogsVerificationForAddingRoutineWithSuccess()
        {
            yield return new object[] { "Adding a routine" };
            yield return new object[] { "Entering name" };
            yield return new object[] { "Entering description" };
            yield return new object[] { "Entering category" };
            yield return new object[] { "Entering note" };
            yield return new object[] { "Entering periodicity" };
            yield return new object[] { "Opening string list manager" };
            yield return new object[] { "Adding string to the list" };
            yield return new object[] { "Entering string" };
            yield return new object[] { "string have been added to the list" };
            yield return new object[] { "Exiting string list manager" };
            yield return new object[] { "Sending info to the service" };
            yield return new object[] { "Routine have been added successfully" };
        }
        public static IEnumerable<object[]> GetErrorVerificationForAdding()
        {
            yield return new object[] { "1\n\nTest\nP\nTest\nM\n1\nTest 1\n1\nTest 2\n0\n0", "Value cannot be null. (Parameter 'name must not be null or empty')" };
            yield return new object[] { "1\nTest 1\n\nP\nTest\nM\n1\nTest 1\n1\nTest 2\n0\n0", "Value cannot be null. (Parameter 'description must not be null or empty')" };
            yield return new object[] { "1\nTest 1\nTest\nFake\n0", "Invalid Category Input. Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inancial, or (T)ransportation." };
            yield return new object[] { "1\nTest 1\nTest\nP\nTest Note\nFake\n0", "Invalid Schedule Input. Use (Y)early, (Q)uarterly, (M)onthly, (W)eekly, or (D)aily" };
            yield return new object[] { "1\nTest 1\nTest\nP\nTest Note\nM\n0\n0", "taskList must not be empty list" };
        }

        public void SetUpStringList(List<string> list)
        {
            list.Add("Test 1");
            list.Add("Test 2");
            list.Add("Test 3");
        }

        public void GetAllRoutines(List<Routines> routineslist, Routines routine1, Routines routine2, Routines routine3)
        {
            routineslist.Add(routine1);
            routineslist.Add(routine2);
            routineslist.Add(routine3);
        }
    }
}
