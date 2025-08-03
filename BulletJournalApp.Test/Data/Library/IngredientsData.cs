using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Data.Library
{
    public class IngredientsData
    {
        public static IEnumerable<object[]> GetValidIngredients()
        {
            yield return new object[] { "Test 1", 1, 1.32, "1 Tbsp", "Updated Test 1", 3, 0.21, "2 Cups" };
            yield return new object[] { "Test 2", 5, 8.32, "2 Tsp", "Updated Test 2", 2, 3.94, "1 Cup" };
            yield return new object[] { "Test 3", 5, 8.32, "1 g", "Updated Test 3", 6, 3.94, "1 Cup" };
            yield return new object[] { "Test 4", 5, 8.32, "1 lbs", "Updated Test 4", 6, 3.94, "1 Cup" };
            yield return new object[] { "Test 5", 5, 8.32, "1 oz", "Updated Test 5", 6, 3.94, "1 Cup" };
            yield return new object[] { "Test 6", 5, 8.32, "1 ml", "Updated Test 6", 6, 3.94, "1 Cup" };
            yield return new object[] { "Test 7", 5, 8.32, "1 Gallon", "Updated Test 7", 6, 3.94, "1 Cup" };
            yield return new object[] { "Test 8", 5, 8.32, "2 Gallons", "Updated Test 8", 6, 3.94, "1 Cup" };
            yield return new object[] { "Test 9", 5, 8.32, "1 Quart", "Updated Test 9", 6, 3.94, "1 Cup" };
            yield return new object[] { "Test 10", 5, 8.32, "2 Quarts", "Updated Test 10", 6, 3.94, "1 Cup" };
            yield return new object[] { "Test 11", 5, 8.32, "1 Pint", "Updated Test 11", 6, 3.94, "1 Cup" };
            yield return new object[] { "Test 12", 5, 8.32, "2 Pints", "Updated Test 12", 6, 3.94, "1 Cup" };
            yield return new object[] { "Test 13", 5, 8.32, "1 Cup", "Updated Test 13", 6, 3.94, "1 Cup" };
            yield return new object[] { "Test 14", 5, 8.32, "2 Cups", "Updated Test 14", 6, 3.94, "1 Cup" };
            yield return new object[] { "Test 15", 5, 8.32, "1 Liter", "Updated Test 15", 6, 3.94, "1 Cup" };
            yield return new object[] { "Test 16", 5, 8.32, "2 Liters", "Updated Test 16", 6, 3.94, "1 Cup" };
            yield return new object[] { "Test 17", 5, 8.32, "N/A", "Updated Test 17", 6, 3.94, "1 Cup" };
            yield return new object[] { "Test 17", 5, 0.00, "N/A", "Updated Test 17", 6, 3.94, "1 Cup" };
        }
        public static IEnumerable<object[]> GetIngredientsWithEmptyString()
        {
            yield return new object[] { "", "Test", 1, 1.32, "1 Tbsp", "", 3, 0.21, "2 Cups" };
            yield return new object[] { "", "Test", 1, 1.32, "1 Tsp", "", 3, 0.21, "2 Cups" };
            yield return new object[] { "", "Test", 1, 1.32, "1 Tsp", "", 3, 0.21, "2 Cups" };
        }
        public static IEnumerable<object[]> GetIngredientsWithOutOfRangeValue()
        {
            yield return new object[] { "Test", -5, 1.32, 1, 1.32, "1 Tbsp", "Updated Test", -5, 1.32, "2 Cups" };
            yield return new object[] { "Test", 1, -3.21, 1, 1.32, "1 Tbsp", "Updated Test", 1, -3.21, "2 Cups" };
            yield return new object[] { "Test", -8, -3.21, 1, 1.32, "1 Tbsp", "Updated Test", -8, -3.21, "2 Cups" };
            yield return new object[] { "Test", 0, -3.21, 1, 1.32, "1 Tbsp", "Updated Test", 0, -3.21, "2 Cups" };
        }
        public static IEnumerable<object[]> GetIngredientsWithIncorrectFormatValue()
        {
            yield return new object[] { "Test", 1, 1.32, "1 Cat", "1 Tbsp", "Updated Test", 3, 0.21, "1 Cat" };
            yield return new object[] { "Test", 1, 1.32, "5 Meow", "1 Tbsp", "Updated Test", 3, 0.21, "5 Meow" };
            yield return new object[] { "Test", 1, 1.32, "", "1 Tbsp", "Updated Test", 3, 0.21, "" };
        }
    }
}
