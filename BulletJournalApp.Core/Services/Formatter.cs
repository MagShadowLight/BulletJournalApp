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
        public string FormatItems(Items item)
        {

            return $"[{item.Id}]: {item.Name}\n" +
                $"- Description: {item.Description}\n" +
                $"- Quantity: {item.Quantity}\n" +
                $"- Category: {item.Category.ToString()}\n" +
                $"- Status: {item.Status.ToString()}\n" +
                $"- Note: {item.Notes}\n" +
                $"- Date Added: {item.DateAdded}\n" +
                $"{(item.DateBought != DateTime.MinValue ? $"- Date Bought: {item.DateBought}" : "- Date Bought: N/A")}";
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
                var i = meal.Ingredients.IndexOf(ingredient) + 1; 
                ingredients += FormatIngredient(ingredient, i);
            });
            return $"[{meal.Id}]: {meal.Name}\n" +
                $"- Description: {meal.Description}\n" +
                $"- Time of day: {meal.TimeOfDay.ToString()}\n" +
                $"- Meal Date: {meal.MealDate.ToShortDateString()}\n" +
                $"- Meal Time: {meal.MealTime.ToShortTimeString()}\n" +
                $"- Ingredients: \n{ingredients}";
        }
        public string FormatIngredient(Ingredients ingredient, int i)
        {
            return $"Ingredient #{i}: \n" +
                $"  - Name: {ingredient.Name}\n" +
                $"  - Quantity: {ingredient.Quantity}\n" +
                $"  - Price: ${ingredient.Price}\n" +
                $"  - Measurement: {ingredient.Measurements}\n";
        }

        public string FormatRoutines(Routines routines)
        {
            var str = "";
            foreach(var stritem in routines.TaskList)
            {
                str += stritem + "\n";
            }
            return $"[{routines.Id}]: {routines.Name}\n" +
                $"Description: {routines.Description}\n" +
                $"Periodicity: {routines.Periodicity}\n" +
                $"Category: {routines.Category}\n" +
                $"Note: {routines.Notes}\n" +
                $"Tasks:\n" +
                $"{str}";
        }
    }
}
