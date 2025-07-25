using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface ITimeOfDayService
    {
        public List<Meals> GetMealsByTimeOfDay(TimeOfDay timeOfDay);
        public void ChangeTimeOfDay(string name, TimeOfDay timeOfDay);
    }
}
