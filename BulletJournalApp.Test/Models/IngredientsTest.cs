using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Models
{
    public class IngredientsTest
    {
        [Fact]
        public void When_Creating_An_Ingredients_With_All_The_Properties_Then_It_Should_Be_Initalize_With_Properties()
        {
            // Arrange
            Ingredients ingredient = new Ingredients("Ingredient Test", 3, 2.35, "1 Liter");
            // Act // Assert
            Assert.Contains("Ingredient Test", ingredient.Name);
            Assert.Equal(3, ingredient.Quantity);
            Assert.Equal(2.35, ingredient.Price);
            Assert.Contains("1 Liter", ingredient.Measurements);
        }
        [Fact]
        public void When_Trying_To_Create_An_Ingredients_With_Invalid_Properties_Then_It_Should_Throw_Exception()
        {
            // Arrange
            Ingredients ingredients = new Ingredients("Ingredient Test", 3, 2.35, "1 Liter");
            // Act // Assert
            Assert.Throws<ArgumentNullException>(() => { Ingredients ingredients1 = new Ingredients("", 3, 2.35, "1 Cup"); });
            Assert.Throws<ArgumentOutOfRangeException>(() => { Ingredients ingredients1 = new Ingredients("Fake Test", -5, 2.35, "1 Cup"); });
            Assert.Throws<ArgumentOutOfRangeException>(() => { Ingredients ingredients1 = new Ingredients("Fake Test", 10, -10.32, "1 Cup"); });
            Assert.Throws<FormatException>(() => { Ingredients ingredients1 = new Ingredients("Fake Test", 5, 3.21, "Made up measurement"); });
        }
        [Fact]
        public void When_Creating_An_Ingredients_With_Different_Measurements_Then_It_Should_Be_Added()
        {
            // Arrange
            List<Ingredients> ingredients = new();
            Ingredients ingredient1 = new Ingredients("Ingredient Test 1", 3, 2.35, "1 Tbsp");
            Ingredients ingredient2 = new Ingredients("Ingredient Test 2", 3, 2.35, "1 Tsp");
            Ingredients ingredient3 = new Ingredients("Ingredient Test 3", 3, 2.35, "1 g");
            Ingredients ingredient4 = new Ingredients("Ingredient Test 4", 3, 2.35, "1 lbs");
            Ingredients ingredient5 = new Ingredients("Ingredient Test 5", 3, 2.35, "1 oz");
            Ingredients ingredient6 = new Ingredients("Ingredient Test 6", 3, 2.35, "1 ml");
            Ingredients ingredient7 = new Ingredients("Ingredient Test 7", 3, 2.35, "1 gallon");
            Ingredients ingredient8 = new Ingredients("Ingredient Test 8", 3, 2.35, "1 gallons");
            Ingredients ingredient9 = new Ingredients("Ingredient Test 9", 3, 2.35, "1 quart");
            Ingredients ingredient10 = new Ingredients("Ingredient Test 10", 3, 2.35, "1 quarts");
            Ingredients ingredient11 = new Ingredients("Ingredient Test 4", 3, 2.35, "1 pint");
            Ingredients ingredient12 = new Ingredients("Ingredient Test 4", 3, 2.35, "1 pints");
            Ingredients ingredient13 = new Ingredients("Ingredient Test 4", 3, 2.35, "1 cup");
            Ingredients ingredient14 = new Ingredients("Ingredient Test 4", 3, 2.35, "1 cups");
            Ingredients ingredient15 = new Ingredients("Ingredient Test 4", 3, 2.35, "1 liter");
            // Act
            ingredients.Add(ingredient1);
            ingredients.Add(ingredient2);
            ingredients.Add(ingredient3);
            ingredients.Add(ingredient4);
            ingredients.Add(ingredient5);
            ingredients.Add(ingredient6);
            ingredients.Add(ingredient7);
            ingredients.Add(ingredient8);
            ingredients.Add(ingredient9);
            ingredients.Add(ingredient10);
            ingredients.Add(ingredient11);
            ingredients.Add(ingredient12);
            ingredients.Add(ingredient13);
            ingredients.Add(ingredient14);
            ingredients.Add(ingredient15);
            // Assert
            Assert.Equal(15, ingredients.Count);
        }
        [Fact]
        public void When_Ingredients_Were_Updating_Then_It_Should_Be_Updated_With_New_Values()
        {
            // Arrange
            Ingredients ingredient = new Ingredients("Ingredient Test", 3, 2.35, "1 Liter");
            // Act
            ingredient.Update("Updated Ingredient", 5, 10.25, "1 Cups");
            // Assert
            Assert.Contains("Updated", ingredient.Name);
            Assert.Equal(5, ingredient.Quantity);
            Assert.Equal(10.25, ingredient.Price);
            Assert.Contains("1 Cups", ingredient.Measurements);
        }
    }
}
