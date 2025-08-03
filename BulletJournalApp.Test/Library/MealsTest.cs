using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Test.Data.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Library
{
    public class MealsTest
    {
        [Theory]
        [MemberData(nameof(MealsData.GetValidMeals), MemberType =typeof(MealsData))]
        public void Given_There_Are_Valid_Values_When_Meal_Were_Added_Then_It_Should_Assigned_With_Those_Value(string name, string desc, List<Ingredients> ingredients, DateTime mealdate, DateTime mealtime, int id, TimeOfDay timeofday, string newname, string newdesc, TimeOfDay newtimeofday, DateTime newmealdate, DateTime newmealtime, List<Ingredients> newingredients)
        {
            // Arrange // Act
            var meal = new Meals(name, desc, ingredients, mealdate, mealtime, id, timeofday);
            // Assert
            Assert.Equal(name, meal.Name);
            Assert.Equal(desc, meal.Description);
            Assert.Equal(ingredients.Count, meal.Ingredients.Count);
            Assert.Equal(mealdate, meal.MealDate);
            Assert.Equal(mealtime, meal.MealTime);
            Assert.Equal(id, meal.Id);
            Assert.Equal(timeofday, meal.TimeOfDay);
        }
        [Theory]
        [MemberData(nameof(MealsData.GetMealsWithEmptyString), MemberType = typeof(MealsData))]
        public void Given_There_Are_Values_With_Empty_String_When_Trying_To_Add_Meal_Then_It_Should_Throw_Exception(string name, string desc, List<Ingredients> ingredients, DateTime mealdate, DateTime mealtime, string fakename, string fakedesc, string newname, string newdesc)
        {
            Assert.Throws<ArgumentNullException>(() => { new Meals(name, desc, ingredients, mealdate, mealtime); });
        }
        [Theory]
        [MemberData(nameof(MealsData.GetMealsWithEmptyList), MemberType = typeof(MealsData))]
        public void Given_There_Are_Values_With_Empty_List_When_Trying_To_Add_Meal_Then_It_Should_Throw_Exception(string name, string desc, List<Ingredients> ingredients, DateTime mealdate, DateTime mealtime, List<Ingredients> fakeingredients)
        {
            Assert.Throws<FormatException>(() => { new Meals(name, desc, ingredients, mealdate, mealtime); });
        }
        [Theory]
        [MemberData(nameof(MealsData.GetValidMeals), MemberType = typeof(MealsData))]
        public void Given_There_Are_Meals_When_Updating_The_Meal_Then_It_Should_Reassigned_With_Those__New_Value(string name, string desc, List<Ingredients> ingredients, DateTime mealdate, DateTime mealtime, int id, TimeOfDay timeofday, string newname, string newdesc, TimeOfDay newtimeofday, DateTime newmealdate, DateTime newmealtime, List<Ingredients> newingredients)
        {
            // Arrange
            var meal = new Meals(name, desc, ingredients, mealdate, mealtime, id, timeofday);
            // Act
            meal.Update(newname, newdesc);
            meal.ChangeTimeOfDay(newtimeofday);
            meal.ChangeMealDateAndTime(newmealdate, newmealtime);
            meal.ChangeIngredients(newingredients);
            // Assert
            Assert.Equal(newname, meal.Name);
            Assert.Equal(newdesc, meal.Description);
            Assert.Equal(newtimeofday, meal.TimeOfDay);
            Assert.Equal(newmealdate, meal.MealDate);
            Assert.Equal(newmealtime, meal.MealTime);
            Assert.Equal(newingredients, meal.Ingredients);
        }
        [Theory]
        [MemberData(nameof(MealsData.GetMealsWithEmptyString), MemberType = typeof(MealsData))]
        public void Given_There_Are_Meal_When_Trying_To_Update_Meal_With_Empty_String_Then_It_Should_Throw_Exception(string name, string desc, List<Ingredients> ingredients, DateTime mealdate, DateTime mealtime, string fakename, string fakedesc, string newname, string newdesc)
        {
            // Arrange
            var meal = new Meals(fakename, fakedesc, ingredients, mealdate, mealtime);
            // Act // Assert
            Assert.Throws<ArgumentNullException>(() => meal.Update(newname, newdesc));
        }
        [Theory]
        [MemberData(nameof(MealsData.GetMealsWithEmptyList), MemberType = typeof(MealsData))]
        public void Given_There_Are_Meal_With_Empty_List_When_Trying_To_Update_Meal_With_Empty_List_Then_It_Should_Throw_Exception(string name, string desc, List<Ingredients> invalidingredients, DateTime mealdate, DateTime mealtime, List<Ingredients> ingredients)
        {
            // Arrange
            var meal = new Meals(name, desc, ingredients, mealdate, mealtime);
            // Act // Assert
            Assert.Throws<FormatException>(() => { meal.ChangeIngredients(invalidingredients); });
        }




        /*
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
        */
    }
}
