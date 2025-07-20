using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Models
{
    public class MealsTest
    {
        [Fact]
        public void When_Creating_A_Meal_With_All_Properties_Then_It_Should_Be_Added()
        {
            // Arrange
            List<Ingredients> ingredients = new List<Ingredients>();
            Ingredients ingredient1 = new Ingredients("Ingredient No 1", 3, 2.12, "1 Cup");
            Ingredients ingredient2 = new Ingredients("Ingredient No 2", 5, 1.25, "1 Pint");
            ingredients.Add(ingredient1);
            ingredients.Add(ingredient2);
            // Act
            Meals mealTest = new Meals("Meal Test", "nom nom nom", ingredients, DateTime.Today, DateTime.Today, 0, TimeOfDay.Lunch);
            // Assert
            Assert.Contains("Meal Test", mealTest.Name);
            Assert.Contains("nom", mealTest.Description);
            Assert.Equal(2, mealTest.Ingredients.Count);
            Assert.Equal(DateTime.Today, mealTest.MealDate);
            Assert.Equal(DateTime.Today, mealTest.MealTime);
            Assert.Equal(TimeOfDay.Lunch, mealTest.TimeOfDay);
            Assert.Equal(0, mealTest.Id);
        }
        [Fact]
        public void When_Trying_To_Create_A_Meal_With_Invalid_Properties_Then_It_Should_Throw_Exception()
        {
            // Arrange
            List<Ingredients> invalidIngredients = new();
            List<Ingredients> ingredients = new List<Ingredients>();
            Ingredients ingredient1 = new Ingredients("Ingredient No 1", 3, 2.12, "1 Cup");
            Ingredients ingredient2 = new Ingredients("Ingredient No 2", 5, 1.25, "1 Pint");
            ingredients.Add(ingredient1);
            ingredients.Add(ingredient2);
            // Act
            Meals mealTest = new Meals("Meal Test", "nom nom nom", ingredients, DateTime.Today, DateTime.Today, 0, TimeOfDay.Lunch);
            // Assert
            Assert.Throws<ArgumentNullException>(() => { var invalidMeals = new Meals("", "Nom Nom Nom", ingredients, DateTime.Now, DateTime.Now, 0, TimeOfDay.Lunch); });
            Assert.Throws<ArgumentNullException>(() => { var invalidMeals = new Meals("Invalid Meal", "", ingredients, DateTime.Now, DateTime.Now, 0, TimeOfDay.Lunch); });
            Assert.Throws<FormatException>(() => { var invalidMeals = new Meals("Invalid Meal", "Nom Nom Nom", invalidIngredients, DateTime.Now, DateTime.Now, 0, TimeOfDay.Lunch); });
        }
        [Fact]
        public void When_Meals_Are_Updating_Then_It_Should_Be_Updated_With_New_Value()
        {
            // Assert
            List<Ingredients> ingredients = new List<Ingredients>();
            List<Ingredients> newIngredients = new List<Ingredients>();
            Ingredients ingredient1 = new Ingredients("Ingredient No 1", 3, 2.12, "1 Cup");
            Ingredients ingredient2 = new Ingredients("Ingredient No 2", 5, 1.25, "1 Pint");
            Ingredients ingredient3 = new Ingredients("Ingredient No 3", 9, 5.21, "1 Gallon");
            ingredients.Add(ingredient1);
            ingredients.Add(ingredient2);
            newIngredients.Add(ingredient1);
            newIngredients.Add(ingredient2);
            newIngredients.Add(ingredient3);
            Meals mealTest = new Meals("Meal Test", "nom nom nom", ingredients, DateTime.Today, DateTime.Today, 0, TimeOfDay.Lunch);
            // Act
            mealTest.Update("Updated Meal", "nom nom nom nom mmmm");
            mealTest.ChangeTimeOfDay(TimeOfDay.Snacks);
            mealTest.ChangeMealDateAndTime(DateTime.Today.AddDays(1), DateTime.Today.AddHours(12));
            mealTest.ChangeIngredients(newIngredients);
            // Assert
            Assert.Contains("Updated Meal", mealTest.Name);
            Assert.Contains("mmmm", mealTest.Description);
            Assert.Equal(3, mealTest.Ingredients.Count);
            Assert.Equal(DateTime.Today.AddDays(1), mealTest.MealDate);
            Assert.Equal(DateTime.Today.AddHours(12), mealTest.MealTime);
            Assert.Equal(TimeOfDay.Snacks, mealTest.TimeOfDay);
        }
    }
}
