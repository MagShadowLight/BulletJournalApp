using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using BulletJournalApp.UI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.UI
{
    public class MealPlanManager
    {
        private Entries _entries = Entries.MEALS;
        private readonly IMealService _mealService;
        internal readonly IIngredientService _ingredientService;
        private readonly ITimeOfDayService _timeOfDayService;
        private readonly IUserInput _userInput;
        internal readonly IConsoleLogger _consolelogger;
        internal readonly IFileLogger _filelogger;
        internal readonly IFormatter _formatter;
        private readonly IFileService _fileservice;
        private Boolean isRunning = true;

        public MealPlanManager(IMealService mealService, IIngredientService ingredientService, ITimeOfDayService timeOfDayService, IUserInput userInput, IConsoleLogger consolelogger, IFileLogger filelogger, IFormatter formatter, IFileService fileservice)
        {
            _mealService = mealService;
            _ingredientService = ingredientService;
            _timeOfDayService = timeOfDayService;
            _userInput = userInput;
            _consolelogger = consolelogger;
            _filelogger = filelogger;
            _formatter = formatter;
            _fileservice = fileservice;
        }

        public void MealPlanUI()
        {
            _filelogger.Log("Meal Plan Manager opened.");
            Console.WriteLine("Welcome to Meal Plan");
            Console.WriteLine("");
            while(isRunning)
            {
                Console.WriteLine("Meal Plan Manager Menu");
                Console.WriteLine("1. Add a meal");
                Console.WriteLine("2. Get all meals");
                Console.WriteLine("3. Get a list of meals by time of day");
                Console.WriteLine("4. Find meal by name");
                Console.WriteLine("5. Update meals");
                Console.WriteLine("6. Delete meals");
                Console.WriteLine("7. File managements");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        _filelogger.Log("Adding a meal");
                        AddMeal();
                        break;
                    case "2":
                        _filelogger.Log("Listing all meals");
                        ListAllMeals();
                        break;
                    case "3":
                        _filelogger.Log("Listing meals by time of day");
                        ListMealsByTimeOfDay();
                        break;
                    case "4":
                        _filelogger.Log("Finding meal by name");
                        SearchMeal();
                        break;
                    case "5":
                        _filelogger.Log("Updating the meal");
                        UpdateMeal();
                        break;
                    case "6":
                        _filelogger.Log("Deleting the meal");
                        DeleteMeal();
                        break;
                    case "7":
                        _filelogger.Log("Opening the file management for this entries");
                        MealFileManager();
                        break;
                    case "0":
                        _filelogger.Log("Closing meal plan manager");
                        Console.WriteLine("Goodbye");
                        isRunning = false;
                        break;
                    default:
                        _consolelogger.Error("Invalid choice. Try again.");
                        _filelogger.Error("Invalid choice. Try again.");
                        break;
                }
            }
        }

        public void MealFileManager()
        {
            throw new NotImplementedException();
        }

        public void DeleteMeal()
        {
            var name = _userInput.GetStringInput("Enter the name of the meal to delete: ");
            var meal = _mealService.FindMealsByName(name);
            foreach(var mealItem in meal.Ingredients)
            {
                _ingredientService.DeleteIngredient(mealItem.Name);
            }
            _mealService.DeleteMeals(name);
        }

        public void UpdateMeal()
        {
            Console.WriteLine("Which value do you want to change: ");
            Console.WriteLine("1. Name and description.");
            Console.WriteLine("2. Time of day");
            Console.WriteLine("3. Date and Time");
            Console.WriteLine("4. Ingredients");
            Console.Write("Choose an options: ");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    var oldname = _userInput.GetStringInput("Enter the name of the meal to change the name and description: ");
                    var newname = _userInput.GetStringInput("Enter the new name for the meal: ");
                    var newdescription = _userInput.GetStringInput("Enter the new description for the meal: ");
                    _mealService.UpdateMeals(oldname, newname, newdescription);
                    _consolelogger.Log("Meal updated successfully");
                    break;
                case "2":
                    var name1 = _userInput.GetStringInput("Enter the name of the meal to change the time of day: ");
                    var timeofday = _userInput.GetTimeOfDayInput("Enter the time of the day (Use (B)reakfast, (L)unch, (Di)nner, (S)nacks, or (De)ssert or leave it blank: ");
                    _timeOfDayService.ChangeTimeOfDay(name1, timeofday);
                    _consolelogger.Log("Meal updated successfully");
                    break;
                case "3":
                    var name2 = _userInput.GetStringInput("Enter the name of the meal to change the date and time: ");
                    var newdate = _userInput.GetDateInput("Enter the new date for the meal: ");
                    var newtime = _userInput.GetDateInput("Enter the new time for the meal: ");
                    _mealService.ChangeMealDateTime(name2, newdate, newtime);
                    _consolelogger.Log("Meal updated successfully");
                    break;
                case "4":
                    var name3 = _userInput.GetStringInput("What the name to change the ingredients: ");
                    var meal = _mealService.FindMealsByName(name3);
                    var ingredients = meal.Ingredients;
                    ingredients = IngredientManager.IngredientManagerUI(this, meal);
                    _mealService.ChangeMealIngredients(name3, ingredients);
                    _consolelogger.Log("Meal updated successfully");
                    break;
                default:
                    _consolelogger.Log("Invalid choice. Try again");
                    break;
            }
            

        }

        public void SearchMeal()
        {
            var name = _userInput.GetStringInput("Enter the name to search for specific meal: ");
            var meal = _mealService.FindMealsByName(name);
            Console.WriteLine($"Found: {_formatter.FormatMeals(meal)}");
        }

        public void ListMealsByTimeOfDay()
        {
            var timeofday = _userInput.GetTimeOfDayInput("Enter the time of the day (Use (B)reakfast, (L)unch, (Di)nner, (S)nacks, or (De)ssert or leave it blank: ");
            var meals = _timeOfDayService.GetMealsByTimeOfDay(timeofday);
            foreach (var meal in meals)
            {
                Console.WriteLine(_formatter.FormatMeals(meal));
            }
        }

        public void ListAllMeals()
        {
            var meals = _mealService.GetAllMeals();
            foreach ( var meal in meals )
            {
                Console.WriteLine(_formatter.FormatMeals(meal));
            }
        }

        public void AddMeal()
        {
            var name = _userInput.GetStringInput("Enter the name of the meal: ");
            var description = _userInput.GetStringInput("Enter the description of the meal: ");
            var timeOfDay = _userInput.GetTimeOfDayInput("Enter the time of the day (Use (B)reakfast, (L)unch, (Di)nner, (S)nacks, or (De)ssert or leave it blank");
            var mealDate = _userInput.GetDateInput("Enter the date for the meal like July 1, 2001: ");
            var mealTime = _userInput.GetDateInput("Enter the time for the meal like 12:00 PM: ");
            _filelogger.Log("Opening ingredient manager");
            List<Ingredients> ingredients = IngredientManager.IngredientManagerUI(this, null);
            var meal = new Meals(name, description, ingredients, mealDate, mealTime, 0, timeOfDay);
            _filelogger.Log("Ingredient manager closed");
            _mealService.AddMeal(meal);
            _filelogger.Log("Meal added successfully");
            _consolelogger.Log("Meal added successfully");
        }


    }

    internal class IngredientManager
    {
        private static Entries _entries = Entries.MEALS;
        private static readonly IConsoleLogger _consolelogger = new ConsoleLogger();
        private static readonly IFormatter _formatter = new Formatter();
        private static readonly IUserInput _userInput = new UserInput();
        private static readonly IIngredientService _ingredientService = new IngredientService();
        private static List<Ingredients> ingredients = new List<Ingredients>();

        internal IngredientManager() { }

        public static List<Ingredients> IngredientManagerUI(MealPlanManager mealPlan, Meals? meal)
        {
            mealPlan._filelogger.Log("Ingredient manager opened");
            if (meal != null)
            {
                ingredients = meal.Ingredients.ToList();
            }
            while (true)
            {
                Console.WriteLine("Ingredients Manager Menu");
                Console.WriteLine("1. Add ingredient");
                Console.WriteLine("2. Edit ingredients");
                Console.WriteLine("3. Delete ingredients");
                Console.WriteLine("4. Get all ingredients");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        mealPlan._filelogger.Log("Adding an ingredient");
                        AddIngredients(mealPlan);
                        break;
                    case "2":
                        EditIngredient(mealPlan);
                        break;
                    case "3":
                        DeleteIngredient(mealPlan);
                        break;
                    case "4":
                        GetAllIngredients(mealPlan);
                        break;
                    case "0":
                        if (ingredients.Count > 0)
                        {
                            Console.WriteLine("Ingredient List have been added");
                            mealPlan._filelogger.Log("Closing ingredient manager");
                        }
                        else
                        {
                            mealPlan._filelogger.Log("Closing ingredient manager without adding ingredients");
                        }
                        return ingredients;
                    default:
                        _consolelogger.Error("Invalid choice. Try again.");
                        //_filelogger.Error("Invalid choice. Try again.");
                        break;
                }
            }
        }

        private static void GetAllIngredients(MealPlanManager mealPlan)
        {
            var ingredients = mealPlan._ingredientService.GetAllIngredients();
            foreach(var ingredient in ingredients)
            {
                Console.WriteLine(mealPlan._formatter.FormatIngredient(ingredient));
            }
        }

        private static void DeleteIngredient(MealPlanManager mealPlan)
        {
            var name = _userInput.GetStringInput("Enter the name of the ingredient to delete: ");
            mealPlan._ingredientService.DeleteIngredient(name);
            var ingredient = ingredients.Find(x => x.Name == name);
            ingredients.Remove(ingredient);
            _consolelogger.Log("Ingredient deleted successfully");
        }

        private static void EditIngredient(MealPlanManager mealPlan)
        {
            var oldname = _userInput.GetStringInput("Enter the name of the ingredient to update: ");
            var newname = _userInput.GetStringInput("Enter the new name of the ingredient: ");
            var newquantity = _userInput.GetIntInput("Enter the new quantity: ");
            var newprice = _userInput.GetDoubleInput("Enter the new price: ");
            var newmeasurement = _userInput.GetStringInput("Enter the new measurement for the ingredients (Must be specific to measurement in kitchen or type in N/A): ");
            mealPlan._ingredientService.ChangeIngredients(oldname, newname, newquantity, newprice, newmeasurement);
            var ingredient = new Ingredients(newname, newquantity, newprice, newmeasurement);
            var oldingredient = ingredients.Find(x => x.Name == oldname);
            ingredients.Remove(oldingredient);
            ingredients.Add(ingredient);
            _consolelogger.Log("Ingredient updated successfully");
        }

        private static void AddIngredients(MealPlanManager mealPlan)
        {
            var name = _userInput.GetStringInput("Enter the name of the ingredient: ");
            var quantity = _userInput.GetIntInput("Enter the quantity of the ingredient: ");
            var price = _userInput.GetDoubleInput("Enter the price of the ingredient: ");
            var measurement = _userInput.GetStringInput("Enter the measurements of the ingredient: (Must be specific to measurement in the kitchen like cups or type in N/A): ");
            var ingredient = new Ingredients(name, quantity, price, measurement);
            mealPlan._ingredientService.AddIngredient(ingredient);
            ingredients.Add(ingredient);
            mealPlan._filelogger.Log("Ingredient added successfully");
            _consolelogger.Log("Ingredient added successfully");
        }
    }
}
