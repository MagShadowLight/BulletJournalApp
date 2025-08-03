using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Service
{
    public class MealServiceTest
    {
        List<Meals> meals = new();
        List<Ingredients> ingredientsList1 = new List<Ingredients>();
        List<Ingredients> ingredientsList2 = new List<Ingredients>();
        public void SetUpIngredients()
        {
            Ingredients ingredient1 = new Ingredients("Test 1", 3, 2.16, "1 Cup");
            Ingredients ingredient2 = new Ingredients("Test 2", 1, 6.12, "1 tsp");
            Ingredients ingredient3 = new Ingredients("Test 3", 6, 0.52, "2 oz");
            ingredientsList1.Add(ingredient1);
            ingredientsList1.Add(ingredient2);
            ingredientsList2.Add(ingredient1);
            ingredientsList2.Add(ingredient3);
        }
        
        [Fact]
        public void When_Meals_Were_Added_Then_It_Should_Succeed()
        {
            // Arrange
            SetUpIngredients();
            Meals meal1 = new Meals("Test 1", "nom", ingredientsList1, DateTime.Today, DateTime.Today, 0, TimeOfDay.Lunch);
            Meals meal2 = new Meals("Test 2", "nom nom", ingredientsList2, DateTime.Today, DateTime.Today, 0, TimeOfDay.Dinner);
            Meals meal3 = new Meals("Test 3", "nom nom nom", ingredientsList1, DateTime.Today, DateTime.Today, 0, TimeOfDay.Breakfast);
            var service = new MealService();
            // Act
            service.AddMeal(meal1);
            service.AddMeal(meal2);
            service.AddMeal(meal3);
            meals = service.GetAllMeals();
            // Assert
            Assert.Equal(3, meals.Count);
            Assert.Contains(meal1, meals);
            Assert.Contains(meal2, meals);
            Assert.Contains(meal3, meals);
            Assert.Throws<ArgumentNullException>(() => service.AddMeal(new Meals("", "Nom", ingredientsList1, DateTime.Today, DateTime.Today, 0, TimeOfDay.None)));
            Assert.Throws<DuplicateNameException>(() => service.AddMeal(new Meals("Test 2", "nom nom nom nom", ingredientsList2, DateTime.Today, DateTime.Today, 0, TimeOfDay.None)));
        }
        [Fact]
        public void When_Meals_Were_Updated_Then_It_Should_Succeed()
        {
            // Arrange
            SetUpIngredients();
            Meals meal1 = new Meals("Test 1", "nom", ingredientsList1, DateTime.Today, DateTime.Today, 0, TimeOfDay.Lunch);
            Meals meal2 = new Meals("Test 2", "nom nom", ingredientsList2, DateTime.Today, DateTime.Today, 0, TimeOfDay.Dinner);
            Meals meal3 = new Meals("Test 3", "nom nom nom", ingredientsList1, DateTime.Today, DateTime.Today, 0, TimeOfDay.Breakfast);
            var service = new MealService();
            service.AddMeal(meal1);
            service.AddMeal(meal2);
            service.AddMeal(meal3);
            // Act
            service.UpdateMeals("Test 2", "Updated Test", "nom nom mmmm");
            meals = service.GetAllMeals();
            // Assert
            Assert.Equal(3, meals.Count);
            Assert.Contains("Updated Test", meal2.Name);
            Assert.Contains("mmmm", meal2.Description);
            Assert.Contains(meal1, meals);
            Assert.Contains(meal2, meals);
            Assert.Contains(meal3, meals);
            Assert.Throws<DuplicateNameException>(() => service.UpdateMeals("Test 3", "Test 1", "nom"));
            Assert.Throws<ArgumentNullException>(() => service.UpdateMeals("Fake Test", "Test", "nom, gross"));
        }
        [Fact]
        public void When_Meals_Were_Changed_Date_And_Time_Then_It_Should_Succeed()
        {
            // Arrange
            SetUpIngredients();
            Meals meal1 = new Meals("Test 1", "nom", ingredientsList1, DateTime.Today, DateTime.Today, 0, TimeOfDay.Lunch);
            Meals meal2 = new Meals("Test 2", "nom nom", ingredientsList2, DateTime.Today, DateTime.Today, 0, TimeOfDay.Dinner);
            Meals meal3 = new Meals("Test 3", "nom nom nom", ingredientsList1, DateTime.Today, DateTime.Today, 0, TimeOfDay.Breakfast);
            var service = new MealService();
            service.AddMeal(meal1);
            service.AddMeal(meal2);
            service.AddMeal(meal3);
            // Act
            service.ChangeMealDateTime("Test 3", DateTime.Today.AddDays(1), DateTime.Today.AddHours(4));
            meals = service.GetAllMeals();
            // Assert
            Assert.Equal(3, meals.Count);
            Assert.Equal(DateTime.Today.AddDays(1), meal3.MealDate);
            Assert.Equal(DateTime.Today.AddHours(4), meal3.MealTime);
            Assert.Contains(meal1, meals);
            Assert.Contains(meal2, meals);
            Assert.Contains(meal3, meals);
            Assert.Throws<ArgumentNullException>(() => service.ChangeMealDateTime("Fake Test", DateTime.Today.AddDays(1), DateTime.Today.AddHours(2)));
        }
        [Fact]
        public void When_Ingredients_On_Meals_Were_Changed_Then_It_Should_Succeed()
        {
            // Arrange
            SetUpIngredients();
            Meals meal1 = new Meals("Test 1", "nom", ingredientsList1, DateTime.Today, DateTime.Today, 0, TimeOfDay.Lunch);
            Meals meal2 = new Meals("Test 2", "nom nom", ingredientsList2, DateTime.Today, DateTime.Today, 0, TimeOfDay.Dinner);
            Meals meal3 = new Meals("Test 3", "nom nom nom", ingredientsList1, DateTime.Today, DateTime.Today, 0, TimeOfDay.Breakfast);
            List<Ingredients> newIngredientsList = new();
            newIngredientsList.AddRange(ingredientsList1);
            newIngredientsList.AddRange(ingredientsList2);
            var service = new MealService();
            service.AddMeal(meal1);
            service.AddMeal(meal2);
            service.AddMeal(meal3);
            // Act
            service.ChangeMealIngredients("Test 2", newIngredientsList);
            var meals = service.GetAllMeals();
            // Assert
            Assert.Equal(3, meals.Count);
            Assert.Equal(4, meal2.Ingredients.Count);
            Assert.Contains(meal1, meals);
            Assert.Contains(meal2, meals);
            Assert.Contains(meal3, meals);
            Assert.Throws<ArgumentNullException>(() => service.ChangeMealIngredients("Fake Test", ingredientsList2));
        }
        [Fact]
        public void When_Meals_Were_Deleted_Then_It_Should_Succeed()
        {
            // Arrange
            SetUpIngredients();
            Meals meal1 = new Meals("Test 1", "nom", ingredientsList1, DateTime.Today, DateTime.Today, 0, TimeOfDay.Lunch);
            Meals meal2 = new Meals("Test 2", "nom nom", ingredientsList2, DateTime.Today, DateTime.Today, 0, TimeOfDay.Dinner);
            Meals meal3 = new Meals("Test 3", "nom nom nom", ingredientsList1, DateTime.Today, DateTime.Today, 0, TimeOfDay.Breakfast);
            var service = new MealService();
            service.AddMeal(meal1);
            service.AddMeal(meal2);
            service.AddMeal(meal3);
            // Act
            service.DeleteMeals("Test 3");
            meals = service.GetAllMeals();
            // Assert
            Assert.Equal(2, meals.Count);
            Assert.Contains(meal1, meals);
            Assert.Contains(meal2, meals);
            Assert.Throws<ArgumentNullException>(() => service.DeleteMeals("Fake Test"));
        }
    }
}
