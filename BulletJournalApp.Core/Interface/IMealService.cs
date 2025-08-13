using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface IMealService
    {
        public void AddMeal(Meals meals);
        public List<Meals> GetAllMeals();
        public Meals FindMealsByName(string name);
        public void UpdateMeals(string oldName, string newName, string description);
        public void ChangeMealDateTime(string name, DateTime newDate, DateTime newTime);
        public void ChangeMealIngredients(string name, List<Ingredients> ingredients);
        public void DeleteMeals(string name);
    }
}
