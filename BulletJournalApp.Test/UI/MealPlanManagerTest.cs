using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.Test.Util;
using BulletJournalApp.UI;
using BulletJournalApp.UI.Util;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.UI
{
    [Collection("Sequential")]
    public class MealPlanManagerTest
    {
        private Entries entries = Entries.MEALS;
        private Mock<IConsoleLogger> consoleLoggerMock = new();
        private Mock<IFileLogger> fileLoggerMock = new();
        private Mock<IFormatter> formatterMock = new();
        private Mock<IFileService> fileMock = new();
        private Mock<UserInput> inputMock = new();
        private UserInput userInput = new UserInput();
        private Mock<IMealService> mealMock = new();
        private Mock<IIngredientService> ingredientMock = new();
        private Mock<ITimeOfDayService> timeOfDayMock = new();
        private ConsoleInputOutput stream = new();
        private List<Meals> meals;

        private List<Ingredients> SetIngredientsList()
        {
            var ingredients = new List<Ingredients>();
            var ingredient1 = new Ingredients("Test 1", 3, 0.32, "1 Cups");
            var ingredient2 = new Ingredients("Test 2", 1, 4.21, "N/A");
            var ingredient3 = new Ingredients("Test 3", 8, 2.14, "1 Gallon");
            ingredients.Add(ingredient1);
            ingredients.Add(ingredient2);
            ingredients.Add(ingredient3);
            return ingredients;
        }
        [Fact]
        public void When_User_Selected_To_Add_Meals_Then_It_Should_Be_Added_To_Meal_Plan()
        {
            // Arrange
            var ui = new MealPlanManager(mealMock.Object, ingredientMock.Object, timeOfDayMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            // Act
            using var input = new StringReader("1\nTest meal\nTest\nL\nJuly 24, 2025\n12:00 PM\n1\nTest ingredients 1\n2\n2.13\nN/A\n1\nTest ingredients 2\n6\n0.59\n1 Cup\n0\n0");
            Console.SetIn(input);
            ui.MealPlanUI();
            // Assert
            mealMock.Verify(user => user.AddMeal(It.Is<Meals>(meal => meal.Name == "Test meal" && meal.Description == "Test" && meal.TimeOfDay == TimeOfDay.Lunch && meal.MealDate == DateTime.Parse("July 24, 2025") && meal.MealTime == DateTime.Parse("12:00 PM") && meal.Ingredients.Count == 2)), Times.Once);
            fileLoggerMock.Verify(log => log.Log("Adding a meal"), Times.Once);
            fileLoggerMock.Verify(log => log.Log("Opening ingredient manager"), Times.Once);
            fileLoggerMock.Verify(log => log.Log("Ingredient manager opened"), Times.Once);
            fileLoggerMock.Verify(log => log.Log("Adding an ingredient"), Times.AtLeastOnce);
            fileLoggerMock.Verify(log => log.Log("Ingredient added successfully"), Times.AtLeastOnce);
            fileLoggerMock.Verify(log => log.Log("Closing ingredient manager"), Times.Once);
            fileLoggerMock.Verify(log => log.Log("Ingredient manager closed"), Times.Once);
            fileLoggerMock.Verify(log => log.Log("Meal added successfully"), Times.Once);
            consoleLoggerMock.Verify(log => log.Log("Meal added successfully"), Times.Once);
            stream.ResetReader();
        }
        [Fact]
        public void When_User_Selected_To_Change_Ingredients_For_The_Selected_Meals_Then_Ingredients_Should_Be_Changed()
        {
            // Arrange
            var ingredients = SetIngredientsList();
            var ingredient2 = ingredients.Find(x => x.Name == "Test 2");
            var ingredient3 = ingredients.Find(x => x.Name == "Test 3");
            var mealTest = new Meals("Testmeal", "Test", ingredients.ToList(), DateTime.Today, DateTime.Today);
            var ui = new MealPlanManager(mealMock.Object, ingredientMock.Object, timeOfDayMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            // Act
            mealMock.Setup(service => service.FindMealsByName(mealTest.Name)).Returns(mealTest);
            ingredientMock.Setup(service => service.FindIngredientsByName("Test 2")).Returns(ingredient2);
            ingredientMock.Setup(service => service.FindIngredientsByName("Test 3")).Returns(ingredient3);
            ingredients.Add(new Ingredients("Test 4", 7, 2.34, "2 Pints"));
            ingredients.Remove(ingredient2);
            ingredient2.Update("Updated Test", 2, 1.53, "1 Tbsp");
            ingredients.Add(ingredient2);
            ingredients.Remove(ingredient3);
            ingredientMock.Setup(service => service.GetAllIngredients()).Returns(ingredients);
            using var input = new StringReader("5\n4\nTestmeal\n1\nTest 4\n7\n2.34\n2 Pints\n2\nTest 2\nUpdated Test\n2\n1.53\n1 Tbsp\n3\nTest 3\n4\n0\n0");
            Console.SetIn(input);
            ui.MealPlanUI();
            // Assert
            mealMock.Verify(user => user.ChangeMealIngredients("Testmeal", It.IsAny<List<Ingredients>>()), Times.Once);
        }
        [Fact]
        public void When_User_Selected_To_Change_Name_And_Description_For_Selected_Meal_Then_Those_Values_Should_Be_Changed()
        {
            // Arrange
            var ingredients = SetIngredientsList();
            var mealTest = new Meals("Test meal", "Test", ingredients.ToList(), DateTime.Today, DateTime.Today);
            var ui = new MealPlanManager(mealMock.Object, ingredientMock.Object, timeOfDayMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            // Act
            mealMock.Setup(service => service.UpdateMeals("Test meal", "Updated meal", "Updated"));
            using var input = new StringReader("5\n1\nTest meal\nUpdated meal\nUpdated\n0");
            Console.SetIn(input);
            ui.MealPlanUI();
            // Assert
            mealMock.Verify(user => user.UpdateMeals("Test meal", "Updated meal", "Updated"), Times.Once);
        }
        [Fact]
        public void When_User_Selected_To_Change_Time_Of_Day_For_Selected_Meal_Then_That_Value_Should_Be_Changed()
        {
            // Arrange
            var ingredients = SetIngredientsList();
            var mealTest = new Meals("Test meal", "Test", ingredients.ToList(), DateTime.Today, DateTime.Today);
            var ui = new MealPlanManager(mealMock.Object, ingredientMock.Object, timeOfDayMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            // Act
            timeOfDayMock.Setup(service => service.ChangeTimeOfDay("Test meal", TimeOfDay.Dinner));
            using var input = new StringReader("5\n2\nTest meal\nDI\n0");
            Console.SetIn(input);
            ui.MealPlanUI();
            // Assert
            timeOfDayMock.Verify(user => user.ChangeTimeOfDay("Test meal", TimeOfDay.Dinner), Times.Once);
        }
        [Fact]
        public void When_User_Selected_To_Change_The_Date_And_Time_For_Selected_Meal_Then_Those_Value_Should_Be_Changed()
        {
            // Arrange
            var ingredients = SetIngredientsList();
            var mealTest = new Meals("Test meal", "Test", ingredients.ToList(), DateTime.Today, DateTime.Today);
            var ui = new MealPlanManager(mealMock.Object, ingredientMock.Object, timeOfDayMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            // Act
            mealMock.Setup(service => service.ChangeMealDateTime("Test meal", DateTime.Parse("July 25, 2025"), DateTime.Parse("12:00 PM")));
            using var input = new StringReader("5\n3\nTest meal\nJuly 25, 2025\n12:00 PM\n0");
            Console.SetIn(input);
            ui.MealPlanUI();
            // Assert
            mealMock.Verify(user => user.ChangeMealDateTime("Test meal", DateTime.Parse("July 25, 2025"), DateTime.Parse("12:00 PM")), Times.Once);
        }
        [Fact]
        public void When_User_Selected_To_Delete_A_Meal_Then_That_Meal_Should_Be_Deleted()
        {
            // Arrange
            var ingredients = SetIngredientsList();
            var ingredient1 = ingredients.Find(x => x.Name == "Test 1");
            var ingredient2 = ingredients.Find(x => x.Name == "Test 2");
            var ingredient3 = ingredients.Find(x => x.Name == "Test 3");
            var mealTest = new Meals("Test meal", "Test", ingredients, DateTime.Today, DateTime.Today);
            var ui = new MealPlanManager(mealMock.Object, ingredientMock.Object, timeOfDayMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            // Act
            mealMock.Setup(service => service.DeleteMeals("Test meal"));
            mealMock.Setup(service => service.FindMealsByName(mealTest.Name)).Returns(mealTest);
            using var input = new StringReader("6\nTest meal\n0");
            Console.SetIn(input);
            ui.MealPlanUI();
            // Assert
            mealMock.Verify(user => user.DeleteMeals("Test meal"), Times.Once);
        }
        [Fact]
        public void When_User_Selected_To_Search_The_Specific_Meal_Then_That_Meal_Should_Be_Found()
        {
            // Arrange
            var ingredients = SetIngredientsList();
            var mealTest = new Meals("Test meal", "Test", ingredients, DateTime.Today, DateTime.Today);
            var ui = new MealPlanManager(mealMock.Object, ingredientMock.Object, timeOfDayMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            // Act
            mealMock.Setup(service => service.FindMealsByName(mealTest.Name)).Returns(mealTest);
            using var input = new StringReader("4\nTest meal\n0");
            Console.SetIn(input);
            ui.MealPlanUI();
            // Assert
            mealMock.Verify(user => user.FindMealsByName("Test meal"), Times.Once);
        }
        [Fact]
        public void When_User_Selected_To_List_All_Meals_Then_It_Should_Return_A_List_Of_Meals()
        {
            // Arrange
            List<Meals> meals = new List<Meals>();
            var ingredients = SetIngredientsList();
            var mealTest = new Meals("Test meal", "Test", ingredients, DateTime.Today, DateTime.Today);
            meals.Add(mealTest);
            var ui = new MealPlanManager(mealMock.Object, ingredientMock.Object, timeOfDayMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            // Act
            mealMock.Setup(service => service.GetAllMeals()).Returns(meals);
            using var input = new StringReader("2\n0");
            Console.SetIn(input);
            ui.MealPlanUI();
            // Assert
            mealMock.Verify(user => user.GetAllMeals(), Times.Once);
        }
        [Fact]
        public void When_User_Selected_To_List_Meals_By_Time_Of_Day_Then_It_Should_Return_List_Of_Meals_Specific_To_That_Value()
        {
            // Arrange
            List<Meals> meals = new List<Meals>();
            var ingredients = SetIngredientsList();
            var mealTest = new Meals("Test meal", "Test", ingredients, DateTime.Today, DateTime.Today, 0, TimeOfDay.Lunch);
            meals.Add(mealTest);
            var ui = new MealPlanManager(mealMock.Object, ingredientMock.Object, timeOfDayMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            // Act
            timeOfDayMock.Setup(service => service.GetMealsByTimeOfDay(TimeOfDay.Lunch)).Returns(meals);
            using var input = new StringReader("3\nL\n0");
            Console.SetIn(input);
            ui.MealPlanUI();
            // Assert
            timeOfDayMock.Verify(user => user.GetMealsByTimeOfDay(TimeOfDay.Lunch), Times.Once);
        }
        [Fact]
        public void When_User_Selected_To_Save_Meals_Lists_Then_It_Should_Be_Saved()
        {
            // Arrange
            List<Meals> meals = new List<Meals>();
            var ingredients = SetIngredientsList();
            var mealTest = new Meals("Test meal", "Test", ingredients, DateTime.Today, DateTime.Today, 0, TimeOfDay.Lunch);
            meals.Add(mealTest);
            var ui = new MealPlanManager(mealMock.Object, ingredientMock.Object, timeOfDayMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            // Act
            mealMock.Setup(service => service.GetAllMeals()).Returns(meals);
            fileMock.Setup(user => user.SaveFunction("Faketest", Entries.MEALS, null, null, meals));
            using var input = new StringReader("7\n1\nFaketest\n0");
            Console.SetIn(input);
            ui.MealPlanUI();
            // Assert
            fileMock.Verify(user => user.SaveFunction("Faketest", Entries.MEALS, null, null, meals), Times.Once);
        }
        [Fact]
        public void When_User_Selected_To_Load_Meals_Lists_Then_It_Should_Be_Loaded()
        {
            // Arrange
            List<Meals> meals = new List<Meals>();
            var ingredients = SetIngredientsList();
            var mealTest = new Meals("Test meal", "Test", ingredients, DateTime.Today, DateTime.Today, 0, TimeOfDay.Lunch);
            meals.Add(mealTest);
            var ui = new MealPlanManager(mealMock.Object, ingredientMock.Object, timeOfDayMock.Object, userInput, consoleLoggerMock.Object, fileLoggerMock.Object, formatterMock.Object, fileMock.Object);
            var taskservice = new Mock<ITaskService>();
            var itemservice = new Mock<IItemService>();
            var service = new FileService(formatterMock.Object, consoleLoggerMock.Object, fileLoggerMock.Object, taskservice.Object, itemservice.Object, mealMock.Object);
            service.SaveFunction("test", Entries.MEALS, null, null, meals);
            // Act
            mealMock.Setup(service => service.GetAllMeals()).Returns(meals);
            fileMock.Setup(user => user.LoadFunction("test", Entries.MEALS));
            
            using var input = new StringReader("7\n2\ntest\n0");
            Console.SetIn(input);
            ui.MealPlanUI();
            // Assert
            fileMock.Verify(user => user.LoadFunction("test", Entries.MEALS), Times.Once);
        }
    }
}
