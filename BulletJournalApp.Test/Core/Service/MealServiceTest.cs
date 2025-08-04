using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Test.Core.Data;
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
        private MealService _mealService;
        private MealServiceData _data = new MealServiceData();
        private List<Ingredients> ingredients = new List<Ingredients>();
        private Meals meal1;
        private Meals meal2;
        private Meals meal3;
        int num = 0;

        public MealServiceTest()
        {
            _mealService = new MealService();
            _data.SetUpIngredientsList(ingredients);
            meal1 = new Meals("Test 1", "Test", ingredients, DateTime.Today, DateTime.Today);
            meal2 = new Meals("Test 2", "Test", ingredients, DateTime.Today, DateTime.Today);
            meal3 = new Meals("Test 3", "Test", ingredients, DateTime.Today, DateTime.Today);
        }
        [Fact]
        public void Given_There_Are_No_Meals_In_The_List_When_Adding_A_Meal_Then_Meal_Should_Be_Added_To_The_List()
        {
            // Arrange
            num = 3;
            // Act
            _mealService.AddMeal(meal1);
            _mealService.AddMeal(meal2);
            _mealService.AddMeal(meal3);
            var meals = _mealService.GetAllMeals();
            // Assert
            Assert.Equal(num, meals.Count);
            Assert.Contains(meal1, meals);
            Assert.Contains(meal2, meals);
            Assert.Contains(meal3, meals);
            Assert.Throws<DuplicateNameException>(() => _mealService.AddMeal(new Meals("Test 1", "Test", ingredients, DateTime.Today, DateTime.Today)));
            Assert.Throws<ArgumentNullException>(() => _mealService.AddMeal(new Meals("", "Test", ingredients, DateTime.Today, DateTime.Today)));
            Assert.Throws<ArgumentNullException>(() => _mealService.AddMeal(new Meals("Test", "", ingredients, DateTime.Today, DateTime.Today)));
            Assert.Throws<FormatException>(() => _mealService.AddMeal(new Meals("Test", "Test", new List<Ingredients>(), DateTime.Today, DateTime.Today)));
        }
        [Fact]
        public void Given_There_Are_Meals_In_The_List_When_Getting_All_Meals_Then_It_Should_Return_List_Of_Meals()
        {
            // Arrange
            num = 3;
            _data.SetUpMeals(_mealService, meal1, meal2, meal3);
            // Act
            var meals = _mealService.GetAllMeals();
            // Assert
            Assert.Equal(num, meals.Count);
            Assert.Contains(meal1, meals);
            Assert.Contains(meal2, meals);
            Assert.Contains(meal3, meals);
        }
        [Theory]
        [MemberData(nameof(MealServiceData.GetStringValue), MemberType =typeof(MealServiceData))]
        public void Given_There_Are_Meals_In_The_List_When_Searching_For_Meal_With_Specific_Name_Then_It_Should_Return_Meal_With_That_Name(string name)
        {
            // Arrange
            _data.SetUpMeals(_mealService, meal1, meal2, meal3);
            // Act
            var meal = _mealService.FindMealsByName(name);
            // Assert
            Assert.Equal(name, meal.Name);
        }
        [Theory]
        [MemberData(nameof(MealServiceData.GetValuesForUpdate), MemberType = typeof(MealServiceData))]
        public void Given_There_Are_Meals_In_The_List_When_Changing_The_Name_And_Description_Then_Meal_Should_Be_Updated_With_New_Value(string oldname, string newname, string newdescription, DateTime newmealdate, DateTime newmealtime)
        {
            // Arrange
            num = 3;
            _data.SetUpMeals(_mealService, meal1, meal2, meal3);
            // Act
            _mealService.UpdateMeals(oldname, newname, newdescription);
            var meals = _mealService.GetAllMeals();
            var meal = _mealService.FindMealsByName(newname);
            // Assert
            Assert.Equal(num, meals.Count);
            Assert.Equal(newname, meal.Name);
            Assert.Equal(newdescription, meal.Description);
            Assert.Throws<ArgumentNullException>(() => _mealService.UpdateMeals(null, newname, newdescription));
            Assert.Throws<DuplicateNameException>(() => _mealService.UpdateMeals(newname, newname, newdescription));
            Assert.Throws<ArgumentNullException>(() => _mealService.UpdateMeals(newname, "", "Test"));
            Assert.Throws<ArgumentNullException>(() => _mealService.UpdateMeals(newname, "Test", ""));
        }
        [Theory]
        [MemberData(nameof(MealServiceData.GetValuesForUpdate), MemberType =typeof(MealServiceData))]
        public void Given_There_Are_Meals_In_The_List_When_Changing_The_Meal_Date_And_Time_Then_Meal_Should_Be_Updated_With_New_Value(string oldname, string newname, string newdescription, DateTime newmealdate, DateTime newmealtime)
        {
            // Arrange
            num = 3;
            _data.SetUpMeals(_mealService, meal1, meal2, meal3);
            // Act
            _mealService.ChangeMealDateTime(oldname, newmealdate, newmealtime);
            var meals = _mealService.GetAllMeals();
            var meal = _mealService.FindMealsByName(oldname);
            // Assert
            Assert.Equal(num, meals.Count);
            Assert.Equal(newmealdate, meal.MealDate);
            Assert.Equal(newmealtime, meal.MealTime);
            Assert.Throws<ArgumentNullException>(() => _mealService.ChangeMealDateTime(null, newmealdate, newmealtime));
        }
        [Theory]
        [MemberData(nameof(MealServiceData.GetValuesForChangingIngredient), MemberType =typeof(MealServiceData))]
        public void Given_There_Are_Meals_In_The_List_When_Changing_The_Ingredients_In_The_Ingredients_List_Then_Meal_Should_Be_Updated_With_New_Ingredient_List(string name, Ingredients ingredient)
        {
            // Arrange
            num = 3;
            var newnum = 4;
            List<Ingredients> newingredient = ingredients.ToList();
            _data.SetUpMeals(_mealService, meal1, meal2, meal3);
            // Act
            newingredient.Add(ingredient);
            _mealService.ChangeMealIngredients(name, newingredient);
            var meals = _mealService.GetAllMeals();
            // Assert
            Assert.Equal(num, meals.Count);
            Assert.Equal(num, ingredients.Count);
            Assert.Equal(newnum, newingredient.Count);
            Assert.Throws<ArgumentNullException>(() => _mealService.ChangeMealIngredients(null, newingredient));
        }
        [Theory]
        [MemberData(nameof(MealServiceData.GetStringValue), MemberType =typeof(MealServiceData))]
        public void Given_There_Are_Meals_In_The_List_When_Deleting_A_Meal_Then_Meal_Should_Be_Removed_From_The_List(string name)
        {
            // Arrange
            num = 2;
            _data.SetUpMeals(_mealService, meal1, meal2, meal3);
            var meal = _mealService.FindMealsByName(name);
            // Act
            _mealService.DeleteMeals(name);
            var meals = _mealService.GetAllMeals();
            // Assert
            Assert.Equal(num, meals.Count);
            Assert.DoesNotContain(meal, meals);
            Assert.Throws<ArgumentNullException>(() => _mealService.DeleteMeals(null));
        }
    }
}
