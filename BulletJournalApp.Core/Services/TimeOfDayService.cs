using BulletJournalApp.Core.Interface;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Services
{
    public class TimeOfDayService : ITimeOfDayService
    {
        private readonly IMealService _mealservice;
        public TimeOfDayService(IMealService mealService)
        {
            _mealservice = mealService;
        }
        public void ChangeTimeOfDay(string name, TimeOfDay timeOfDay)
        {
            var meal = _mealservice.FindMealsByName(name);
            if (meal == null)
                throw new ArgumentNullException($"Meal: {name} not found");
            meal.TimeOfDay = timeOfDay;
        }

        public List<Meals> GetMealsByTimeOfDay(TimeOfDay timeOfDay)
        {
            var meals = _mealservice.GetAllMeals();
            return meals.Where(time => time.TimeOfDay == timeOfDay).ToList();
        }
    }
}
