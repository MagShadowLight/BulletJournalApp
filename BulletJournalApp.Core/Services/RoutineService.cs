using BulletJournalApp.Core.Interface;
using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Services
{
    public class RoutineService : IRoutineService
    {
        public List<Routines> routines = new();
        public RoutineService() { }
        public void AddRoutine(Routines routine)
        {
            ValidateDupe(routine.Name);
            routines.Add(routine);
        }

        public List<Routines> GetAllRoutines()
        {
            return routines;
        }
        public Routines FindRoutineByName(string name)
        {
            return routines.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        public void UpdateRoutine(string oldname, string newname, string newdescription, string newnote)
        {
            var routine = FindRoutineByName(oldname);
            if (routine == null)
                throw new ArgumentNullException($"Routine: {oldname} not found");
            ValidateDupe(newname);
            routine.UpdateRoutine(newname, newdescription, newnote);
        }

        public void ChangeTaskList(string name, List<string> newtasklist)
        {
            var routine = FindRoutineByName(name);
            if (routine == null)
                throw new ArgumentNullException($"Routine: {name} not found");
            routine.ChangeTaskList(newtasklist);
        }

        public void DeleteRoutine(string name)
        {
            var routine = FindRoutineByName(name);
            if (routine == null)
                throw new ArgumentNullException($"Routine: {name} not found");
            routines.Remove(routine);
        }

        private void ValidateDupe(string name)
        {
            routines.ForEach(routine =>
            {
                if (routine.Name.Equals(name))
                    throw new DuplicateNameException($"This Meal is duplication of {name} found in {routines.IndexOf(routine)} index");
            });
        }
    }
}
