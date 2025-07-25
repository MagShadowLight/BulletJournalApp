using BulletJournalApp.Core.Interface;
using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Services
{
    public class Formatter : IFormatter
    {
        public string FormatItems(Items items)
        {

            return $"[{items.Id}]: {items.Name}\n" +
                $"- Description: {items.Description}\n" +
                $"- Category: {items.Category.ToString()}\n" +
                $"- Status: {items.Status.ToString()}\n" +
                $"- Note: {items.Notes}\n" +
                $"- Date Added: {items.DateAdded}\n" +
                $"{(items.DateBought != DateTime.MinValue ? $"- Date Bought: {items.DateBought}" : "")}";
        }

        public string FormatTasks(Tasks task)
        {
            return $"[{task.Id}]: {task.Title}\n" +
                $"- Description: {task.Description}\n" +
                $"- Priority: {task.Priority.ToString()}\n" +
                $"- Due Date: {task.DueDate.ToString()}\n" +
                $"- Status: {task.Status.ToString()}\n" +
                $"- Category: {task.Category.ToString()}\n" +
                $"- Note: {task.Notes}\n" +
                $"{(task.IsCompleted ? "Completed": "Incomplete")}\n" +
                $"{(task.IsRepeatable ? (task.EndRepeatDate == DateTime.MinValue ? "Repeating Task" : $"Repeating until {task.EndRepeatDate}") : "Repeat: N/A")}";
        }
        public string FormatMeals(Meals meal)
        {
            var ingredients = "";
            meal.Ingredients.ForEach(ingredient => {
                ingredients += FormatIngredient(ingredient);
            });
            return $"[{meal.Id}]: {meal.Name}\n" +
                $"- Description: {meal.Description}\n" +
                $"- Time of day: {meal.TimeOfDay.ToString()}\n" +
                $"- Meal Date: {meal.MealDate.ToShortDateString()}\n" +
                $"- Meal Time: {meal.MealTime.ToShortTimeString()}\n" +
                $"- Ingredients: {ingredients}";
        }
        public string FormatIngredient(Ingredients ingredient)
        {
            return $"- Name: {ingredient.Name}\n" +
                $"- Quantity: {ingredient.Quantity}\n" +
                $"- Price: ${ingredient.Price}\n" +
                $"- Measurement: {ingredient.Measurements}\n";
        }
    }
}
