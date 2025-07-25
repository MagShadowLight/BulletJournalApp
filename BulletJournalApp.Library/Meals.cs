using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Library
{
    public class Meals
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeOfDay TimeOfDay { get; set; }
        public DateTime MealDate { get; set; }
        public DateTime MealTime { get; set; }
        public List<Ingredients>? Ingredients { get; set; }

        public Meals(string name, string description, List<Ingredients> ingredients, DateTime mealDate, DateTime mealTime, int id = 0, TimeOfDay timeOfDay = TimeOfDay.None)
        {
            ValidateString(name, nameof(name));
            ValidateString(description, nameof(description));
            ValidateList(ingredients, nameof(ingredients));
            Name = name;
            Description = description;
            TimeOfDay = timeOfDay;
            Ingredients = ingredients;
            MealDate = mealDate;
            MealTime = mealTime;
            Id = id;
        }
        public void Update(string newName, string newDescription)
        {
            ValidateString(newName, nameof(newName));
            ValidateString(newDescription, nameof(newDescription));
            Name = newName;
            Description = newDescription;
        }

        public void ChangeTimeOfDay(TimeOfDay newTimeOfDay)
        {
            TimeOfDay = newTimeOfDay;
        }

        public void ChangeMealDateAndTime(DateTime newMealDate, DateTime newMealTime)
        {
            MealDate = newMealDate;
            MealTime = newMealTime;
        }
        public void ChangeIngredients(List<Ingredients> ingredients)
        {
            ValidateList(ingredients, nameof(ingredients));
            Ingredients = ingredients;
        }

        public void ValidateString(string input, string fieldname)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException($"{fieldname} must not be null or empty");
        }
        public void ValidateList(List<Ingredients> input, string fieldname)
        {
            if (input.Count == 0)
                throw new FormatException($"{fieldname} must not be empty list");
        }
    }
}
