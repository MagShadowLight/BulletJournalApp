using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Data
{
    public class IngredientServiceData
    {
        public static IEnumerable<object[]> GetStringValue()
        {
            yield return new object[] { "Test 1" };
            yield return new object[] { "Test 2" };
            yield return new object[] { "Test 3" };
        }
        public static IEnumerable<object[]> GetValuesForUpdate()
        {
            yield return new object[] { "Test 1", "Updated Test", 1, 1.11, "1 Cup" };
            yield return new object[] { "Test 2", "Updated Test", 1, 1.11, "1 Cup" };
            yield return new object[] { "Test 3", "Updated Test", 1, 1.11, "1 Cup" };
        }

        public void SetUpIngredients(IngredientService ingredientService, Ingredients ingredient1, Ingredients ingredient2, Ingredients ingredient3)
        {
            ingredientService.AddIngredient(ingredient1);
            ingredientService.AddIngredient(ingredient2);
            ingredientService.AddIngredient(ingredient3);
        }
    }
}
