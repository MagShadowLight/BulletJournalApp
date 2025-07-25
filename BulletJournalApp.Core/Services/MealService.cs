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
    public class MealService : IMealService
    {
        public List<Meals> meals = new();
        public MealService() { }
        public void AddMeal(Meals meal)
        {
            ValidateDupe(meal.Name);
            meals.Add(meal);
        }

        private void ValidateDupe(string name)
        {
            foreach (var meal in meals)
            {
                if (meal.Name.Equals(name))
                    throw new DuplicateNameException($"This Meal is duplication of {name} found in {meals.IndexOf(meal)} index");
            }
        }

        public void ChangeMealDateTime(string name, DateTime newDate, DateTime newTime)
        {
            var meal = FindMealsByName(name);
            if (meal == null)
                throw new ArgumentNullException($"Meal: {name} not found");
            meal.ChangeMealDateAndTime(newDate, newTime);
        }

        public void ChangeMealIngredients(string name, List<Ingredients> ingredients)
        {
            var meal = FindMealsByName(name);
            if (meal == null)
                throw new ArgumentNullException($"Meal: {name} not found");
            meal.ChangeIngredients(ingredients);
        }

        public void DeleteMeals(string name)
        {
            var meal = FindMealsByName(name);
            if (meal == null)
                throw new ArgumentNullException($"Meal: {name} not found");
            meals.Remove(meal);
        }

        public Meals FindMealsByName(string name)
        {
            return meals.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Meals> GetAllMeals()
        {
            return meals;
        }

        public void UpdateMeals(string oldName, string newName, string description)
        {
            var meal = FindMealsByName(oldName);
            if (meal == null)
                throw new ArgumentNullException($"Meal: {oldName} not found");
            ValidateDupe(newName);
            meal.Update(newName, description);
        }
    }
}
