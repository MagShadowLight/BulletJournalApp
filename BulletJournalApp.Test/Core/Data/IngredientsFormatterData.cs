using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Data
{
    public class IngredientsFormatterData
    {
        public static IEnumerable<object[]> GetIngredient()
        {
            yield return new object[] { new Ingredients(
                    "Test 1",
                    1,
                    1.11,
                    "1 Cup"
                ) 
            };
            yield return new object[] { new Ingredients(
                    "Test 2",
                    1,
                    1.11,
                    "1 Cup"
                ) 
            };
            yield return new object[] { new Ingredients(
                    "Test 3",
                    1,
                    1.11,
                    "1 Cup"
                ) 
            };
        }
    }
}
