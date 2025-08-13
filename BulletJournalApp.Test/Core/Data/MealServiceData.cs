using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Data
{
    public class MealServiceData
    {
        public static IEnumerable<object[]> GetStringValue()
        {
            yield return new object[] { "Test 1" };
            yield return new object[] { "Test 2" };
            yield return new object[] { "Test 3" };
        }
        public static IEnumerable<object[]> GetValuesForUpdate()
        {
            yield return new object[] { "Test 1", "Updated Test", "Updated Description", DateTime.Today.AddDays(2), DateTime.Today.AddHours(10) };
            yield return new object[] { "Test 2", "Updated Test", "Updated Description", DateTime.Today.AddDays(2), DateTime.Today.AddHours(10) };
            yield return new object[] { "Test 3", "Updated Test", "Updated Description", DateTime.Today.AddDays(2), DateTime.Today.AddHours(10) };
        }

        public static IEnumerable<object[]> GetValuesForChangingIngredient()
        {
            yield return new object[] { "Test 1", new Ingredients("New Test", 1, 1.11, "1 Cup") };
            yield return new object[] { "Test 2", new Ingredients("New Test", 1, 1.11, "1 Cup") };
            yield return new object[] { "Test 3", new Ingredients("New Test", 1, 1.11, "1 Cup") };
        }

        public void SetUpIngredientsList(List<Ingredients> ingredients)
        {
            var ingredient1 = new Ingredients("Test 1", 1, 1.11, "1 cup");
            var ingredient2 = new Ingredients("Test 2", 1, 1.11, "1 cup");
            var ingredient3 = new Ingredients("Test 3", 1, 1.11, "1 cup");
            ingredients.Add(ingredient1);
            ingredients.Add(ingredient2);
            ingredients.Add(ingredient3);
        }
        public void SetUpMeals(MealService mealservice, Meals meal1, Meals meal2, Meals meal3)
        {
            mealservice.AddMeal(meal1);
            mealservice.AddMeal(meal2);
            mealservice.AddMeal(meal3);
        }
    }
}
