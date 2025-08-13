using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Test.Core.Data;
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
        private IngredientService _ingredientService;
        Ingredients ingredient1 = new Ingredients("Test 1", 1, 1.11, "1 Cup");
        Ingredients ingredient2 = new Ingredients("Test 2", 1, 1.11, "1 Cup");
        Ingredients ingredient3 = new Ingredients("Test 3", 1, 1.11, "1 Cup");
        private IngredientServiceData _data;
        
        public IngredientServiceTest()
        {
            _ingredientService = new IngredientService();
            _data = new IngredientServiceData();
        }

        [Fact]
        public void Given_There_Are_No_Ingredients_In_The_List_When_Adding_A_New_Ingredient_Then_Ingredient_Should_Be_Added_To_The_List()
        {
            // Arrange
            int num = 3;
            // Act
            _ingredientService.AddIngredient(ingredient1);
            _ingredientService.AddIngredient(ingredient2);
            _ingredientService.AddIngredient(ingredient3);
            var ingredients = _ingredientService.GetAllIngredients();
            // Assert
            Assert.Equal(num, ingredients.Count);
            Assert.Contains(ingredient1, ingredients);
            Assert.Contains(ingredient2, ingredients);
            Assert.Contains(ingredient3, ingredients);
            Assert.Throws<DuplicateNameException>(() => _ingredientService.AddIngredient(new Ingredients("Test 1", 1, 1.11, "1 cup")));
            Assert.Throws<ArgumentNullException>(() => _ingredientService.AddIngredient(new Ingredients("", 1, 1.11, "1 cup")));
            Assert.Throws<ArgumentOutOfRangeException>(() => _ingredientService.AddIngredient(new Ingredients("Test", -5, 1.11, "1 cup")));
            Assert.Throws<ArgumentOutOfRangeException>(() => _ingredientService.AddIngredient(new Ingredients("Test", 1, -2.12, "1 cup")));
            Assert.Throws<FormatException>(() => _ingredientService.AddIngredient(new Ingredients("Test", 1, 1.11, "1 Fake")));
        }
        [Fact]
        public void Given_There_Are_Ingredients_In_The_List_When_Getting_All_Ingredient_Then_It_Should_Return_List_Of_Ingredients()
        {
            // Arrange
            int num = 3;
            _data.SetUpIngredients(_ingredientService, ingredient1, ingredient2, ingredient3);
            // Act
            var ingredients = _ingredientService.GetAllIngredients();
            // Assert
            Assert.Equal(num, ingredients.Count);
            Assert.Contains(ingredient1, ingredients);
            Assert.Contains(ingredient2, ingredients);
            Assert.Contains(ingredient3, ingredients);
        }
        [Theory]
        [MemberData(nameof(IngredientServiceData.GetStringValue), MemberType =typeof(IngredientServiceData))]
        public void Given_There_Are_Ingredients_In_The_List_When_Searching_For_An_Ingredient_With_Name_Then_It_Should_Return_That_Ingredient(string name)
        {
            // Arrange
            _data.SetUpIngredients(_ingredientService, ingredient1, ingredient2, ingredient3);
            // Act
            var ingredient = _ingredientService.FindIngredientsByName(name);
            // Assert
            Assert.Contains(name, ingredient.Name);
        }
        [Theory]
        [MemberData(nameof(IngredientServiceData.GetValuesForUpdate), MemberType =typeof(IngredientServiceData))]
        public void Given_There_Are_Ingredients_In_The_List_When_Changing_The_Values_Then_Ingredients_Should_Be_Updated_With_New_Value(string oldname, string newname, int newquantity, double newprice, string newmeasurement)
        {
            // Arrange
            int num = 3;
            _data.SetUpIngredients(_ingredientService, ingredient1, ingredient2, ingredient3);
            // Act
            _ingredientService.ChangeIngredients(oldname, newname, newquantity, newprice, newmeasurement);
            var ingredients = _ingredientService.GetAllIngredients();
            var ingredient = _ingredientService.FindIngredientsByName(newname);
            // Assert
            Assert.Equal(num, ingredients.Count);
            Assert.Equal(newname, ingredient.Name);
            Assert.Equal(newquantity, ingredient.Quantity);
            Assert.Equal(newprice, ingredient.Price);
            Assert.Equal(newmeasurement, ingredient.Measurements);
            Assert.Throws<ArgumentNullException>(() => _ingredientService.ChangeIngredients(null, newname, newquantity, newprice, newmeasurement));
            Assert.Throws<DuplicateNameException>(() => _ingredientService.ChangeIngredients(newname, newname, newquantity, newprice, newmeasurement));
            Assert.Throws<ArgumentNullException>(() => _ingredientService.ChangeIngredients(newname, null, newquantity, newprice, newmeasurement));
            Assert.Throws<ArgumentOutOfRangeException>(() => _ingredientService.ChangeIngredients(newname, oldname, -5, newprice, newmeasurement));
            Assert.Throws<ArgumentOutOfRangeException>(() => _ingredientService.ChangeIngredients(newname, oldname, newquantity, -2.31, newmeasurement));
            Assert.Throws<FormatException>(() => _ingredientService.ChangeIngredients(newname, oldname, newquantity, newprice, "1 Fake"));
        }
        [Theory]
        [MemberData(nameof(IngredientServiceData.GetStringValue), MemberType =typeof(IngredientServiceData))]
        public void Given_There_Are_Ingredients_In_The_List_When_Deleting_The_Ingredient_Then_Ingredient_Should_Be_Removed_From_The_List(string name)
        {
            // Arrange
            int num = 2;
            _data.SetUpIngredients(_ingredientService, ingredient1, ingredient2, ingredient3);
            var ingredient = _ingredientService.FindIngredientsByName(name);
            // Act
            _ingredientService.DeleteIngredient(name);
            var ingredients = _ingredientService.GetAllIngredients();
            // Assert
            Assert.Equal(num, ingredients.Count);
            Assert.DoesNotContain(ingredient, ingredients);
            Assert.Throws<ArgumentNullException>(() => _ingredientService.DeleteIngredient(null));
        }




        /*List<Ingredients> ingredients;
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
        }*/
    }
}
