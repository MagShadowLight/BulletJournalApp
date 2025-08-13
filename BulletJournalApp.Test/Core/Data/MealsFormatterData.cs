using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Data
{
    public class MealsFormatterData
    {
        public static List<Ingredients> ingredients = new List<Ingredients>
        {
            new Ingredients("Test 1", 1, 1.11, "1 Cup"),
            new Ingredients("Test 2", 1, 1.11, "1 Cup"),
            new Ingredients("Test 3", 1, 1.11, "1 Cup")
        };
        public static IEnumerable<object[]> GetMeals()
        {
            yield return new object[] { new Meals(
                    "Test 1",
                    "Test",
                    ingredients,
                    DateTime.Today,
                    DateTime.Today,
                    1,
                    TimeOfDay.Lunch
                ) 
            };
            yield return new object[] { new Meals(
                    "Test 2",
                    "Test",
                    ingredients,
                    DateTime.Today,
                    DateTime.Today,
                    1,
                    TimeOfDay.Lunch
                ) 
            };
            yield return new object[] { new Meals(
                    "Test 3",
                    "Test",
                    ingredients,
                    DateTime.Today,
                    DateTime.Today,
                    1,
                    TimeOfDay.Lunch
                ) 
            };
        }
    }
}
