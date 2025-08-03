using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Data.Library
{
    public class MealsData
    {
        private static List<Ingredients> ingredients1 = SetIngredientsList1(new List<Ingredients>());
        private static List<Ingredients> ingredients2 = SetIngredientsList2(new List<Ingredients>());
        
        private static List<Ingredients> SetIngredientsList1(List<Ingredients> ingredients)
        {
            ingredients.Add(new Ingredients("Test 1", 5, 2.31, "1 Cup"));
            ingredients.Add(new Ingredients("Test 2", 10, 0.32, "N/A"));
            ingredients.Add(new Ingredients("Test 3", 1, 10.40, "1 Pint"));
            return ingredients;
        }
        private static List<Ingredients> SetIngredientsList2(List<Ingredients> ingredients)
        {
            ingredients.Add(new Ingredients("Test 1", 5, 2.31, "1 Cup"));
            ingredients.Add(new Ingredients("Test 2", 10, 0.32, "N/A"));
            ingredients.Add(new Ingredients("Test 3", 1, 10.40, "1 Pint"));
            return ingredients;
        }
        public static IEnumerable<object[]> GetValidMeals()
        {
            yield return new object[] { "Test 1", "Test", ingredients1, DateTime.Today, DateTime.Today.AddHours(5), 0, TimeOfDay.None, "Updated Test 1", "Updated Test", TimeOfDay.Breakfast, DateTime.Today.AddDays(1), DateTime.Today.AddHours(10), ingredients2 };
            yield return new object[] { "Test 2", "Test", ingredients1, DateTime.Today, DateTime.Today.AddHours(5), 0, TimeOfDay.Breakfast, "Updated Test 2", "Updated Test", TimeOfDay.Lunch, DateTime.Today.AddDays(1), DateTime.Today.AddHours(10), ingredients2 };
            yield return new object[] {"Test 3", "Test", ingredients1, DateTime.Today, DateTime.Today.AddHours(5), 0, TimeOfDay.Lunch, "Updated Test 3", "Updated Test", TimeOfDay.Dinner, DateTime.Today.AddDays(1), DateTime.Today.AddHours(10), ingredients2 };
            yield return new object[] {"Test 4", "Test", ingredients1, DateTime.Today, DateTime.Today.AddHours(5), 0, TimeOfDay.Dinner, "Updated Test 4", "Updated Test", TimeOfDay.Snacks, DateTime.Today.AddDays(1), DateTime.Today.AddHours(10), ingredients2 };
            yield return new object[] {"Test 5", "Test", ingredients1, DateTime.Today, DateTime.Today.AddHours(5), 0, TimeOfDay.Snacks, "Updated Test 5", "Updated Test", TimeOfDay.Dessert, DateTime.Today.AddDays(1), DateTime.Today.AddHours(10), ingredients2 };
            yield return new object[] {"Test 6", "Test", ingredients1, DateTime.Today, DateTime.Today.AddHours(5), 0, TimeOfDay.Dessert, "Updated Test 6", "Updated Test", TimeOfDay.None, DateTime.Today.AddDays(1), DateTime.Today.AddHours(10), ingredients2 };
        }
        public static IEnumerable<object[]> GetMealsWithEmptyString()
        {
            yield return new object[] { "", "Test", ingredients1, DateTime.Today, DateTime.Today.AddHours(5), "Test", "Test", "", "Updated Test" };
            yield return new object[] { "Test", "", ingredients1, DateTime.Today, DateTime.Today.AddHours(5), "Test", "Test", "Updated Test", "" };
            yield return new object[] { "", "", ingredients1, DateTime.Today, DateTime.Today.AddHours(5), "Test", "Test", "", "" };
        }
        public static IEnumerable<object[]> GetMealsWithEmptyList()
        {
            yield return new object[] { "Test 1", "Test", new List<Ingredients>(), DateTime.Today, DateTime.Today.AddHours(5), ingredients1 };
            yield return new object[] { "Test 2", "Test", new List<Ingredients>(), DateTime.Today, DateTime.Today.AddHours(10), ingredients1 };
            yield return new object[] { "Test 3", "Test", new List<Ingredients>(), DateTime.Today, DateTime.Today.AddHours(7), ingredients1 };
        }
    }
}
