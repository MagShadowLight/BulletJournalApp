using BulletJournalApp.Core.Interface;
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
    public class RoutineManager
    {
        private Entries _entries = Entries.ROUTINES;
        private readonly IRoutineService _routineService;
        private readonly ICategoryService _categoryService;
        private readonly IPeriodicityService _periodicityService;
        private readonly IUserInput _userInput;
        internal readonly IConsoleLogger _consolelogger;
        public readonly IFileLogger _filelogger;
        internal readonly IFormatter _formatter;
        private readonly IFileService _fileservice;
        private bool isRunning = true;
        private readonly ListManager _listmanager;
        public RoutineManager(IRoutineService routineService, ICategoryService categoryService, IPeriodicityService periodicityService, IUserInput userInput, IConsoleLogger consolelogger, IFileLogger filelogger, IFormatter formatter, IFileService fileservice)
        {
            _routineService = routineService;
            _categoryService = categoryService;
            _periodicityService = periodicityService;
            _userInput = userInput;
            _consolelogger = consolelogger;
            _filelogger = filelogger;
            _formatter = formatter;
            _fileservice = fileservice;
            _listmanager = new(_filelogger);
        }
        public void RoutineUI()
        {
            _filelogger.Log("Routine manager opened");
            Console.WriteLine("Welcome to Routine Entries");
            Console.WriteLine("");
            while (isRunning)
            {
                Console.WriteLine("Routine Manager Menu:\n" +
                    "1. Add a routine\n" +
                    "2. List all routines\n" +
                    "3. List all routines by types\n" +
                    "4. Search Routine\n" +
                    "5. Update Routine\n" +
                    "6. Delete Routine\n" +
                    "7. File Management\n" +
                    "0. Exit");
                var input = _userInput.GetStringInput("Choose an option: ");
                switch (input)
                {
                    case "1":
                        _filelogger.Log("Adding a routine");
                        AddRoutine();
                        break;
                    case "2":
                        ListAllRoutines();
                        break;
                    case "3":
                        ListRoutinesByOptions();
                        break;
                    case "4":
                        SearchRoutines();
                        break;
                    case "5":
                        UpdateRoutineValues();
                        break;
                    case "6":
                        DeleteRoutine();
                        break;
                    case "7":
                        _consolelogger.Warn("This feature is not implemented yet");
                        break;
                    case "0":
                        Console.WriteLine("Goodbye");
                        return;
                    default:
                        _filelogger.Log("Invalid choice. try again.");
                        _consolelogger.Log("Invalid choic. Try again.");
                        break;
                }
            }
        }
        internal void AddRoutine()
        {
            _filelogger.Log("Entering name");
            var name = _userInput.GetStringInput("Enter the name of the routine: ");
            _filelogger.Log("Entering description");
            var description = _userInput.GetStringInput("Enter the description of the routine: ");
            try
            {
                _filelogger.Log("Entering category");
                var category = _userInput.GetCategoryInput("Enter the category of the routine (Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inancial, or (T)ransportation): ");
                _filelogger.Log("Entering note");
                var note = _userInput.GetStringInput("Enter the note of the routine: ");
                _filelogger.Log("Entering schedule");
                var periodicity = _userInput.GetScheduleInput("Enter the periodicity of the routine (Use (Y)early, (Q)uarterly, (M)onthly, (W)eekly, or (D)aily): ");
                _filelogger.Log("Opening string list manager");
                List<string> list = _listmanager.StringListManager();
                var routines = _routineService.GetAllRoutines();
                var id = 0;
                if (routines != null)
                    id = routines.Any() ? routines.Max(r => r.Id) : 0;
                var routine = new Routines(name, description, category, list, periodicity, note, id);
                _filelogger.Log("Sending info to the service");
                _routineService.AddRoutine(routine);
                _filelogger.Log("Routine have been added successfully");
            } catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }
        internal void ListAllRoutines()
        {
            var routines = _routineService.GetAllRoutines();
            if (routines.Count == 0)
            {
                _consolelogger.Error("No routine found");
            }
            routines.ForEach(routine =>
            {
                Console.WriteLine(_formatter.FormatRoutines(routine));
            });
        }
        internal void ListRoutinesByOptions()
        {
            var input = _userInput.GetStringInput("Which options to get a list from? (C)ategory or (S)chedule: ");
            switch (input)
            {
                case "C":
                    ListRoutinesByCategory();
                    break;
                case "S":
                    ListRoutinesByPeriodicity();
                    break;
            }
        }
        internal void ListRoutinesByCategory()
        {
            var category = _userInput.GetCategoryInput("Enter the category (Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inancial, or (T)ransportation): ");
            var routines = _categoryService.ListRoutinesByCategory(category);
            if (routines.Count == 0)
            {
                _consolelogger.Error("No routines found");
                return;
            }
            routines.ForEach(routine =>
            {
                Console.WriteLine(_formatter.FormatRoutines(routine));
            });
        }
        internal void ListRoutinesByPeriodicity()
        {
            var periodicity = _userInput.GetScheduleInput("Enter the periodicity of the routine (Use (Y)early, (Q)uarterly, (M)onthly, (W)eekly, or (D)aily): ");
            var routines = _periodicityService.ListRoutinesByPeriodicity(periodicity);
            if (routines.Count == 0)
            {
                _consolelogger.Error("No routine found");
                return;
            }
            routines.ForEach(routine =>
            {
                Console.WriteLine(_formatter.FormatRoutines(routine));
            });
        }
        internal void SearchRoutines()
        {
            var name = _userInput.GetStringInput("Enter the name to search for specific routine: ");
            var routine = _routineService.FindRoutineByName(name);
            if (routine == null)
            {
                _consolelogger.Error($"Routine: {name} not found");
            }
            Console.WriteLine(_formatter.FormatRoutines(routine));
        }
        internal void UpdateRoutineValues()
        {
            Console.Write("Which value to update?\n" +
                "1. Name, Description, and Notes\n" +
                "2. Category\n" +
                "3. Periodicity\n" +
                "4. Task List");
            var input = _userInput.GetStringInput("Choose an options: ");
            switch (input)
            {
                case "1":
                    ChangeRoutineNameDescriptionNotes();
                    break;
                case "2":
                    ChangeRoutineCategory();
                    break;
                case "3":
                    ChangeRoutinePeriodicity();
                    break;
                case "4":
                    ChangeTasksList();
                    break;
            }
        }
        internal void ChangeRoutineNameDescriptionNotes()
        {
            var oldname = _userInput.GetStringInput("Enter the old name to update: ");
            var newname = _userInput.GetStringInput("Enter the new name: ");
            var newdescription = _userInput.GetStringInput("Enter the new description: ");
            var newnote = _userInput.GetStringInput("Enter the new note: ");
            _routineService.UpdateRoutine(oldname, newname, newdescription, newnote);
            _consolelogger.Log($"Routine: {oldname} have been updated");
        }
        internal void ChangeRoutineCategory()
        {
            var name = _userInput.GetStringInput("Enter the name to update category: ");
            var category = _userInput.GetCategoryInput("Enter the new category (Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inancial, or (T)ransportation): ");
            _categoryService.ChangeCategory(name, _entries, category);
            _consolelogger.Log($"Routine: {name} have been updated");
        }
        internal void ChangeRoutinePeriodicity()
        {
            var name = _userInput.GetStringInput("Enter the name to update periodicity: ");
            var periodicity = _userInput.GetScheduleInput("Enter the new periodicity of the routine (Use (Y)early, (Q)uarterly, (M)onthly, (W)eekly, or (D)aily): ");
            _periodicityService.ChangeSchedule(name, _entries, periodicity);
            _consolelogger.Log($"Routine: ${name} have been updated");
        }
        internal void ChangeTasksList()
        {
            var name = _userInput.GetStringInput("Enter the name to update task list: ");
            var tasklist = _listmanager.StringListManager();
            _routineService.ChangeTaskList(name, tasklist);
            _consolelogger.Log($"Routine: {name} have been updated");
        }
        internal void DeleteRoutine()
        {
            var name = _userInput.GetStringInput("Enter the name to delete the routine: ");
            _routineService.DeleteRoutine(name);
            _consolelogger.Log($"Routine: {name} have been deleted");
        }
        internal void FileManagement()
        {
            throw new NotImplementedException();
        }
        internal void SaveRoutines()
        {
            throw new NotImplementedException();
        }
        internal void LoadRoutines()
        {
            throw new NotImplementedException();
        }        
    }
}
