using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface IFormatter
    {
        public string FormatTasks(Tasks task);
        public string FormatItems(Items items);
        public string FormatMeals(Meals meal);
        public string FormatIngredient(Ingredients ingredient);
    }
}
