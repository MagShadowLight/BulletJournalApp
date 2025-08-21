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

        public static IEnumerable<object[]> GetLogsVerification()
        {
            yield return new object[] { "1\nTest 1\nTest\nP\nTest note\nM\n1\nTest Task 1\n1\nTest Task 2\n1\nTest Task 3\n0\n0", "Adding a routine" };
            yield return new object[] { "1\nTest 1\nTest\nP\nTest note\nM\n1\nTest Task 1\n1\nTest Task 2\n1\nTest Task 3\n0\n0", "Opening string list manager" };
            yield return new object[] { "1\nTest 1\nTest\nP\nTest note\nM\n1\nTest Task 1\n1\nTest Task 2\n1\nTest Task 3\n0\n0", "Adding string to the list" };
            yield return new object[] { "1\nTest 1\nTest\nP\nTest note\nM\n1\nTest Task 1\n1\nTest Task 2\n1\nTest Task 3\n0\n0", "string have been added to the list" };
            yield return new object[] { "1\nTest 1\nTest\nP\nTest note\nM\n1\nTest Task 1\n1\nTest Task 2\n1\nTest Task 3\n0\n0", "Exiting string list manager" };
            yield return new object[] { "1\nTest 1\nTest\nP\nTest note\nM\n1\nTest Task 1\n1\nTest Task 2\n1\nTest Task 3\n0\n0", "Routine have been added successfully" };
            yield return new object[] { "2\n0", "Listing all the routines" };
            yield return new object[] { "3\nmeow\n0", "Listing all routines by options selected" };
            yield return new object[] { "3\nC\nN\n0", "Listing all routines by Category" };
            yield return new object[] { "3\nS\nN\n0", "Listing all routines by Periodicity" };
            yield return new object[] { "4\nMeow\n0", "Searching for routine" };
            yield return new object[] { "5\nMeow\n0", "Updating Routine" };
            yield return new object[] { "5\n1\nMeow\nMrow\nMrrp\nMeow\n0", "Updating routine name, description, and notes" };
            yield return new object[] { "5\n2\nMeow\nN\n0", "Updating routine category" };
            yield return new object[] { "5\n3\nMeow\nM\n0", "Updating routine periodicity" };
            yield return new object[] { "5\n4\nMeow\n0\n0", "Updating routine task list" };
            yield return new object[] { "6\nmeow\n0", "Deleting routine" };
            //yield return new object[] { "5\n1\nMeow\nMrow\nMrrp\nMeow\n0", "Routine: Meow have been updated" };
        }
        public static IEnumerable<object[]> GetErrorVerificationForAdding()
        {
            yield return new object[] { "1\n\nTest\nP\nTest\nM\n1\nTest 1\n1\nTest 2\n0\n0", "Value cannot be null. (Parameter 'name must not be null or empty')" };
            yield return new object[] { "1\nTest 1\n\nP\nTest\nM\n1\nTest 1\n1\nTest 2\n0\n0", "Value cannot be null. (Parameter 'description must not be null or empty')" };
            yield return new object[] { "1\nTest 1\nTest\nFake\n0", "Invalid Category Input. Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inancial, or (T)ransportation." };
            yield return new object[] { "1\nTest 1\nTest\nP\nTest Note\nFake\n0", "Invalid Schedule Input. Use (Y)early, (Q)uarterly, (M)onthly, (W)eekly, or (D)aily" };
            yield return new object[] { "1\nTest 1\nTest\nP\nTest Note\nM\n0\n0", "taskList must not be empty list" };
        }
        public static IEnumerable<object[]> GetValueForError()
        {
            yield return new object[] { "Meow\n0", "Invalid choice. Try again." };
            yield return new object[] { "3\n\n0", "Invalid choice. Try again." };
            yield return new object[] { "5\n\n0", "Invalid choice. Try again." };
            yield return new object[] { "3\nC\nMeow\n0", "Invalid Category Input. Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inancial, or (T)ransportation." };
            yield return new object[] { "3\nS\nMeow\n0", "Invalid Schedule Input. Use (Y)early, (Q)uarterly, (M)onthly, (W)eekly, or (D)aily" };
            yield return new object[] { "4\nmeow\n0", "Routine: meow not found" };
            yield return new object[] { "5\n2\nmeow\nmeow\n0", "Invalid Category Input. Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inancial, or (T)ransportation." };
            yield return new object[] { "5\n3\nmeow\nmeow\n0", "Invalid Schedule Input. Use (Y)early, (Q)uarterly, (M)onthly, (W)eekly, or (D)aily" };
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
