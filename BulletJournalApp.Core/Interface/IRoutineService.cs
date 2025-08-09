using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface IRoutineService
    {
        public void AddRoutine(Routines routine);
        public List<Routines> GetAllRoutines();
        public Routines FindRoutineByName(string name);
        public void UpdateRoutine(string oldname, string newname, string newdescription, string newnote);
        public void ChangeTaskList(string name, List<string> newtasklist);
        public void DeleteRoutine(string name);
    }
}
