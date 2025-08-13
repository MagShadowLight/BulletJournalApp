using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Data
{
    public class TimeOfDayServiceData
    {
        public static IEnumerable<object[]> GetTimeOfDayAndStringValue()
        {
            yield return new object[] { "Test 1", TimeOfDay.None };
            yield return new object[] { "Test 1", TimeOfDay.Breakfast };
            yield return new object[] { "Test 1", TimeOfDay.Lunch };
            yield return new object[] { "Test 1", TimeOfDay.Dinner };
            yield return new object[] { "Test 1", TimeOfDay.Snacks };
            yield return new object[] { "Test 1", TimeOfDay.Dessert };
            yield return new object[] { "Test 2", TimeOfDay.None };
            yield return new object[] { "Test 2", TimeOfDay.Breakfast };
            yield return new object[] { "Test 2", TimeOfDay.Lunch };
            yield return new object[] { "Test 2", TimeOfDay.Dinner };
            yield return new object[] { "Test 2", TimeOfDay.Snacks };
            yield return new object[] { "Test 2", TimeOfDay.Dessert };
            yield return new object[] { "Test 3", TimeOfDay.None };
            yield return new object[] { "Test 3", TimeOfDay.Breakfast };
            yield return new object[] { "Test 3", TimeOfDay.Lunch };
            yield return new object[] { "Test 3", TimeOfDay.Dinner };
            yield return new object[] { "Test 3", TimeOfDay.Snacks };
            yield return new object[] { "Test 3", TimeOfDay.Dessert };
        }
        public static IEnumerable<object[]> GetTimeOfDayValue()
        {
            yield return new object[] { 1, TimeOfDay.None };
            yield return new object[] { 1, TimeOfDay.Breakfast };
            yield return new object[] { 1, TimeOfDay.Lunch };
            yield return new object[] { 1, TimeOfDay.Dinner };
            yield return new object[] { 1, TimeOfDay.Snacks };
            yield return new object[] { 1, TimeOfDay.Dessert };
        }

        public List<Ingredients> SetUpIngredients()
        {
            List<Ingredients> ingredients = new List<Ingredients>();
            var ingredient1 = new Ingredients("Test 1", 1, 1.11, "1 Cup");
            var ingredient2 = new Ingredients("Test 2", 1, 1.11, "1 Cup");
            var ingredient3 = new Ingredients("Test 3", 1, 1.11, "1 Cup");
            ingredients.Add(ingredient1);
            ingredients.Add(ingredient2);
            ingredients.Add(ingredient3);
            return ingredients;
        }
        public void SetUpMeals(MealService mealService)
        {
            var meal1 = new Meals("Test 1", "Test", SetUpIngredients(), DateTime.Today, DateTime.Today, 0, TimeOfDay.None);
            var meal2 = new Meals("Test 2", "Test", SetUpIngredients(), DateTime.Today, DateTime.Today, 0, TimeOfDay.Breakfast);
            var meal3 = new Meals("Test 3", "Test", SetUpIngredients(), DateTime.Today, DateTime.Today, 0, TimeOfDay.Lunch);
            var meal4 = new Meals("Test 4", "Test", SetUpIngredients(), DateTime.Today, DateTime.Today, 0, TimeOfDay.Dinner);
            var meal5 = new Meals("Test 5", "Test", SetUpIngredients(), DateTime.Today, DateTime.Today, 0, TimeOfDay.Snacks);
            var meal6 = new Meals("Test 6", "Test", SetUpIngredients(), DateTime.Today, DateTime.Today, 0, TimeOfDay.Dessert);
            mealService.AddMeal(meal1);
            mealService.AddMeal(meal2);
            mealService.AddMeal(meal3);
            mealService.AddMeal(meal4);
            mealService.AddMeal(meal5);
            mealService.AddMeal(meal6);
        }
    }
}
