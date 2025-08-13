using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Test.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Service
{
    public class TimeOfDayServiceTest
    {
        private MealService _mealService;
        private TimeOfDayService _timeOfDayService;
        private TimeOfDayServiceData _data;

        public TimeOfDayServiceTest()
        {
            _mealService = new MealService();
            _timeOfDayService = new TimeOfDayService(_mealService);
            _data = new TimeOfDayServiceData();
            _data.SetUpMeals(_mealService);
        }
        [Theory]
        [MemberData(nameof(TimeOfDayServiceData.GetTimeOfDayAndStringValue), MemberType =typeof(TimeOfDayServiceData))]
        public void Given_There_Are_Meals_In_The_List_When_Changing_The_Time_Of_Day_Then_Meal_Should_Be_Updated_With_New_Time_Of_Day(string name, TimeOfDay timeOfDay)
        {
            // Arrange
            int num = 6;
            // Act
            _timeOfDayService.ChangeTimeOfDay(name, timeOfDay);
            var meals = _mealService.GetAllMeals();
            var meal = _mealService.FindMealsByName(name);
            // Assert
            Assert.Equal(num, meals.Count);
            Assert.Equal(timeOfDay, meal.TimeOfDay);
            Assert.Throws<ArgumentNullException>(() => _timeOfDayService.ChangeTimeOfDay(null, timeOfDay));
        }
        [Theory]
        [MemberData(nameof(TimeOfDayServiceData.GetTimeOfDayValue), MemberType =typeof(TimeOfDayServiceData))]
        public void Given_There_Are_Meals_In_The_List_When_Listing_The_Meals_With_Specific_Time_Of_Day_Then_It_Should_Return_Meals_With_Only_Time_Of_Day_Value(int num, TimeOfDay timeofday)
        {
            // Arrange // Act
            var meals = _timeOfDayService.GetMealsByTimeOfDay(timeofday);
            // Assert
            Assert.Equal(num, meals.Count);
        }
    }
}
