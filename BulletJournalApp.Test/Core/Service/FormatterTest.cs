using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Test.Core.Data;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BulletJournalApp.Test.Core.Service
{
    public class FormatterTest
    {
        private Formatter _formatter = new Formatter();

        [Theory]
        [MemberData(nameof(TaskFormatterData.GetNormalTasks), MemberType =typeof(TaskFormatterData))]
        public void Given_There_Are_Task_When_Formatting_A_Tasks_Into_A_String_Then_It_Should_Converted_To_String(Tasks task)
        {
            // Arrange
            var message1 = "Incomplete";
            var message2 = "Repeat: N/A";
            // Act
            var result = _formatter.FormatTasks(task);
            // Assert
            Assert.Contains(task.Id.ToString(), result);    
            Assert.Contains(task.Title, result);
            Assert.Contains(task.Description, result);
            Assert.Contains(task.Priority.ToString(), result);
            Assert.Contains(task.DueDate.ToString(), result);
            Assert.Contains(task.Status.ToString(), result);
            Assert.Contains(task.Category.ToString(), result);
            Assert.Contains(task.Notes, result);
            Assert.Contains(message1, result);
            Assert.Contains(message2, result);
        }
        [Theory]
        [MemberData(nameof(TaskFormatterData.GetCompleteTask), MemberType = typeof(TaskFormatterData))]
        public void Given_There_Are_Completed_Task_When_Formatting_Task_Into_A_String_Then_It_Should_Converted_To_String(Tasks task)
        {
            // Arrange 
            var message = "Completed";
            // Act
            var result = _formatter.FormatTasks(task);
            // Assert
            Assert.Contains(message, result);
        }
        [Theory]
        [MemberData(nameof(TaskFormatterData.GetRepeatingTask), MemberType = typeof(TaskFormatterData))]
        public void Given_There_Are_Repeating_Task_When_Formatting_Task_Into_A_String_Then_It_Should_Converted_To_String(Tasks task)
        {
            // Arrange 
            var message = "Repeating Task";
            // Act
            var result = _formatter.FormatTasks(task);
            // Assert
            Assert.Contains(message, result);
        }
        [Theory]
        [MemberData(nameof(TaskFormatterData.GetRepeatingTaskWithEndDate), MemberType = typeof(TaskFormatterData))]
        public void Given_There_Are_Repeating_Task_With_End_Date_When_Formatting_Task_Into_A_String_Then_It_Should_Converted_To_String(Tasks task)
        {
            // Arrange 
            var message = $"Repeating until {task.EndRepeatDate}";
            // Act
            var result = _formatter.FormatTasks(task);
            // Assert
            Assert.Contains(message, result);
        }
        [Theory]
        [MemberData(nameof(ItemFormatterData.GetRegularItems), MemberType =typeof(ItemFormatterData))]
        public void Given_There_Are_Item_When_Formatting_Item_Into_A_String_Then_It_Should_Converted_To_String(Items item)
        {
            // Arrange // Act
            var result = _formatter.FormatItems(item);
            // Assert
            Assert.Contains(item.Id.ToString(), result);
            Assert.Contains(item.Name, result);
            Assert.Contains(item.Description, result);
            Assert.Contains(item.Quantity.ToString(), result);
            Assert.Contains(item.Category.ToString(), result);
            Assert.Contains(item.Status.ToString(), result);
            Assert.Contains(item.Notes, result);
            Assert.Contains(item.DateAdded.ToString(), result);
        }
        [Fact]
        public void Given_There_Are_Item_With_Date_Bought_Of_Min_Date_When_Formatting_Item_Into_A_String_Then_It_Should_Converted_To_String()
        {
            // Arrange
            var item = new Items("Test 1", "Test", Periodicity.Monthly, 1, 1, Category.None, ItemStatus.NotBought, "Test", DateTime.Today, DateTime.MinValue);
            var message = "- Date Bought: N/A";
            // Act
            var result = _formatter.FormatItems(item);
            // Assert
            Assert.Contains(message, result);
        }
        [Fact]
        public void Given_There_Are_Item_With_Bought_Date_When_Formatting_Item_Into_A_String_Then_It_Should_Converted_To_String()
        {
            // Arrange
            var item = new Items("Test 1", "Test", Periodicity.Monthly, 1, 1, Category.None, ItemStatus.NotBought, "Test", DateTime.Today, DateTime.Today.AddDays(1));
            var message = $"- Date Bought: {item.DateBought}";
            Console.WriteLine(item.DateBought);
            // Act
            var result = _formatter.FormatItems(item);
            // Assert
            Assert.Contains(message, result);
        }
        [Theory]
        [MemberData(nameof(IngredientsFormatterData.GetIngredient), MemberType =typeof(IngredientsFormatterData))]
        public void Given_There_Are_Ingredient_When_Formatting_Ingredient_Into_A_String_Then_It_Should_Converted_To_String(Ingredients ingredient)
        {
            // Arrange
            var num = 1;
            // Act
            var result = _formatter.FormatIngredient(ingredient, 1);
            // Assert
            Assert.Contains(num.ToString(), result);
            Assert.Contains(ingredient.Name, result);
            Assert.Contains(ingredient.Quantity.ToString(), result);
            Assert.Contains(ingredient.Price.ToString(), result);
            Assert.Contains(ingredient.Measurements, result);
        }
        [Theory]
        [MemberData(nameof(MealsFormatterData.GetMeals), MemberType =typeof(MealsFormatterData))    ]
        public void Given_There_Are_Meal_When_Formatting_Meal_Into_A_String_Then_It_Should_Converted_To_String(Meals meal)
        {
            // Arrange // Act
            var result = _formatter.FormatMeals(meal);
            // Assert
            Assert.Contains(meal.Id.ToString(), result);
            Assert.Contains(meal.Name, result);
            Assert.Contains(meal.Description, result);
            Assert.Contains(meal.TimeOfDay.ToString(), result);
            Assert.Contains(meal.MealDate.ToShortDateString(), result);
            Assert.Contains(meal.MealTime.ToShortTimeString(), result);
        }









        
    }
}
