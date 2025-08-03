using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Service
{
    public class IngredientServiceTest
    {
        List<Ingredients> ingredients;
        Ingredients ingredient1 = new Ingredients("Test 1", 1, 2.13, "1 Cups");
        Ingredients ingredient2 = new Ingredients("Test 2", 4, 1.12, "1 oz");
        Ingredients ingredient3 = new Ingredients("Test 3", 8, 5.92, "4 pints");
        [Fact]
        public void When_Ingredients_Were_Added_Then_It_Should_Succeed()
        {
            // Arrange
            var service = new IngredientService();
            // Act
            service.AddIngredient(ingredient1);
            service.AddIngredient(ingredient2);
            service.AddIngredient(ingredient3);
            ingredients = service.GetAllIngredients();
            // Assert
            Assert.Equal(3, ingredients.Count);
            Assert.Contains(ingredient1, ingredients);
            Assert.Contains(ingredient2, ingredients);
            Assert.Contains(ingredient3, ingredients);
            Assert.Throws<ArgumentNullException>(() => service.AddIngredient(new Ingredients("", 3, 2.64, "3 Pints")));
            Assert.Throws<DuplicateNameException>(() => service.AddIngredient(new Ingredients("Test 1", 6, 1.32, "1 liter")));
        }
        [Fact]
        public void When_Ingredients_Were_Changed_Then_It_Should_Succeed()
        {
            // Arrange
            var service = new IngredientService();
            service.AddIngredient(ingredient1);
            service.AddIngredient(ingredient2);
            service.AddIngredient(ingredient3);
            // Act
            service.ChangeIngredients("Test 2", "Updated Test", 2, 4.02, "3 Tbsp");
            ingredients = service.GetAllIngredients();
            // Assert
            Assert.Equal(3, ingredients.Count);
            Assert.Contains("Updated Test", ingredient2.Name);
            Assert.Equal(2, ingredient2.Quantity);
            Assert.Equal(4.02, ingredient2.Price);
            Assert.Contains("Tbsp", ingredient2.Measurements);
            Assert.Contains(ingredient1, ingredients);
            Assert.Contains(ingredient3, ingredients);
            Assert.Throws<DuplicateNameException>(() => service.ChangeIngredients("Test 1", "Test 3", 5, 3.21, "1 Gallons"));
            Assert.Throws<ArgumentNullException>(() => service.ChangeIngredients("Fake Test", "Test", 3, 1.21, "1 Tsp"));
        }
        [Fact]
        public void When_There_Is_Ingredients_Then_One_Ingredient_Should_Be_Deleted()
        {
            // Arrange
            var service = new IngredientService();
            service.AddIngredient(ingredient1);
            service.AddIngredient(ingredient2);
            service.AddIngredient(ingredient3);
            // Act
            service.DeleteIngredient("Test 3");
            ingredients = service.GetAllIngredients();
            // Assert
            Assert.Equal(2, ingredients.Count);
            Assert.Contains(ingredient1, ingredients);
            Assert.Contains(ingredient2, ingredients);
            Assert.Throws<ArgumentNullException>(() => service.DeleteIngredient("Fake Test"));
        }
    }
}
