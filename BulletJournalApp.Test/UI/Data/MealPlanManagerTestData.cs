using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.UI.Data
{
    public class MealPlanManagerTestData
    {
        public static IEnumerable<object[]> GetTimeOfDayUpdateInput()
        {
            yield return new object[] { TimeOfDay.None, "5\n2\nTest 2\nN\n0" };
            yield return new object[] { TimeOfDay.Breakfast, "5\n2\nTest 2\nB\n0" };
            yield return new object[] { TimeOfDay.Lunch, "5\n2\nTest 2\nL\n0" };
            yield return new object[] { TimeOfDay.Dinner, "5\n2\nTest 2\nDI\n0" };
            yield return new object[] { TimeOfDay.Snacks, "5\n2\nTest 2\nS\n0" };
            yield return new object[] { TimeOfDay.Dessert, "5\n2\nTest 2\nDE\n0" };
        }
        public static IEnumerable<object[]> GetTimeOfDayListInput()
        {
            yield return new object[] { TimeOfDay.None, "3\nN\n0" };
            yield return new object[] { TimeOfDay.Breakfast, "3\nB\n0" };
            yield return new object[] { TimeOfDay.Lunch, "3\nL\n0" };
            yield return new object[] { TimeOfDay.Dinner, "3\nDI\n0" };
            yield return new object[] { TimeOfDay.Snacks, "3\nS\n0" };
            yield return new object[] { TimeOfDay.Dessert, "3\nDE\n0" };
        }

        public List<Ingredients> SetUpIngredients(List<Ingredients> ingredients)
        {
            var ingredient1 = new Ingredients("Test 1", 3, 0.32, "1 Cups");
            var ingredient2 = new Ingredients("Test 2", 1, 4.21, "N/A");
            var ingredient3 = new Ingredients("Test 3", 8, 2.14, "1 Gallon");
            ingredients.Add(ingredient1);
            ingredients.Add(ingredient2);
            ingredients.Add(ingredient3);
            return ingredients;
        }
        public List<Meals> SetUpMeals(List<Meals> meals)
        {
            var ingredients = new List<Ingredients>();
            ingredients = SetUpIngredients(ingredients);
            var meal1 = new Meals("Test 1", "Test", ingredients.ToList(), DateTime.Today, DateTime.Today, 1, TimeOfDay.None);
            var meal2 = new Meals("Test 2", "Test", ingredients.ToList(), DateTime.Today, DateTime.Today, 1, TimeOfDay.Breakfast);
            var meal3 = new Meals("Test 3", "Test", ingredients.ToList(), DateTime.Today, DateTime.Today, 1, TimeOfDay.Lunch);
            var meal4 = new Meals("Test 4", "Test", ingredients.ToList(), DateTime.Today, DateTime.Today, 1, TimeOfDay.Dinner);
            var meal5 = new Meals("Test 5", "Test", ingredients.ToList(), DateTime.Today, DateTime.Today, 1, TimeOfDay.Snacks);
            var meal6 = new Meals("Test 6", "Test", ingredients.ToList(), DateTime.Today, DateTime.Today, 1, TimeOfDay.Dessert);
            meals.Add(meal1);
            meals.Add(meal2);
            meals.Add(meal3);
            meals.Add(meal4);
            meals.Add(meal5);
            meals.Add(meal6);
            return meals;
        }
    }
}
