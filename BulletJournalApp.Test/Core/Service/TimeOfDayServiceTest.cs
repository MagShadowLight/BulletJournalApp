using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Service
{
    public class TimeOfDayServiceTest
    {
        private readonly MealService _mealService = new MealService();
        List<Ingredients> ingredientsList1 = new List<Ingredients>();
        List<Ingredients> ingredientsList2 = new List<Ingredients>();

        void SetUpMeals()
        {
            Ingredients ingredient1 = new Ingredients("Test 1", 3, 2.16, "1 Cup");
            Ingredients ingredient2 = new Ingredients("Test 2", 1, 6.12, "1 tsp");
            Ingredients ingredient3 = new Ingredients("Test 3", 6, 0.52, "2 oz");
            ingredientsList1.Add(ingredient1);
            ingredientsList1.Add(ingredient2);
            ingredientsList2.Add(ingredient1);
            ingredientsList2.Add(ingredient3);
            Meals meal1 = new Meals("Test 1", "nom", ingredientsList1, DateTime.Today, DateTime.Today, 0, TimeOfDay.Lunch);
            Meals meal2 = new Meals("Test 2", "nom nom", ingredientsList2, DateTime.Today, DateTime.Today, 0, TimeOfDay.Dinner);
            Meals meal3 = new Meals("Test 3", "nom nom nom", ingredientsList1, DateTime.Today, DateTime.Today, 0, TimeOfDay.Breakfast);
            _mealService.AddMeal(meal1);
            _mealService.AddMeal(meal2);
            _mealService.AddMeal(meal3);
        }

        [Fact]
        public void When_Meals_Were_Changed_With_Different_Time_Then_Time_Of_Day_Should_Changed()
        {
            // Arrange
            SetUpMeals();
            var service = new TimeOfDayService(_mealService);
            var meal1 = _mealService.FindMealsByName("Test 1");
            var meal2 = _mealService.FindMealsByName("Test 2");
            var meal3 = _mealService.FindMealsByName("Test 3");
            // Act
            service.ChangeTimeOfDay("Test 2", TimeOfDay.Dessert);
            var meals = _mealService.GetAllMeals();
            // Assert
            Assert.Equal(3, meals.Count);
            Assert.Contains(meal1, meals);
            Assert.Contains(meal2, meals);
            Assert.Contains(meal3, meals);
            Assert.Equal(TimeOfDay.Dessert, meal2.TimeOfDay);
            Assert.Throws<ArgumentNullException>(() => service.ChangeTimeOfDay("Fake Test", TimeOfDay.None));
        }
        [Fact]
        public void When_Time_Of_Day_Were_Selected_Then_It_Should_Return_With_That_Meals()
        {
            // Arrange
            SetUpMeals();
            var service = new TimeOfDayService(_mealService);
            var meal1 = _mealService.FindMealsByName("Test 1");
            var meal2 = _mealService.FindMealsByName("Test 2");
            var meal3 = _mealService.FindMealsByName("Test 3");
            Meals meal4 = new Meals("Test 4", "nom nom nom nom", ingredientsList1, DateTime.Today, DateTime.Today, 0, TimeOfDay.Lunch);
            _mealService.AddMeal(meal4);
            // Act
            var lunchMeals = service.GetMealsByTimeOfDay(TimeOfDay.Lunch);
            var meals = _mealService.GetAllMeals();
            // Assert
            Assert.Equal(4, meals.Count);
            Assert.Equal(2, lunchMeals.Count);
            Assert.Contains(meal1, lunchMeals);
            Assert.Contains(meal4, lunchMeals);
        }
    }
}
