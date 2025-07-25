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
using System.Xml.Linq;

namespace BulletJournalApp.UI
{
    public class ShopListManager
    {
        private Entries entries = Entries.ITEMS;
        private Boolean isRunning = true;
        private readonly IItemService _itemService;
        private readonly IConsoleLogger _consolelogger;
        private readonly IFileLogger _filelogger;
        private readonly IFormatter _formatter;
        private readonly IItemStatusService _statusservice;
        private readonly IScheduleService _scheduleservice;
        private readonly ICategoryService _categoryservice;
        private readonly IFileService _fileservice;
        private readonly IUserInput _userinput;

        public ShopListManager(IItemService itemService, IConsoleLogger consolelogger, IFileLogger filelogger, IFormatter formatter, IItemStatusService statusservice, IScheduleService scheduleservice, ICategoryService categoryservice, IFileService fileservice, IUserInput userInput)
        {
            _itemService = itemService;
            _consolelogger = consolelogger;
            _filelogger = filelogger;
            _formatter = formatter;
            _statusservice = statusservice;
            _scheduleservice = scheduleservice;
            _categoryservice = categoryservice;
            _fileservice = fileservice;
            _userinput = userInput;
        }

        public void UI()
        {
            _filelogger.Log("Shopping List Manager Opened");
            Console.WriteLine("Welcome To Shopping List");
            Console.WriteLine("");
            while (isRunning)
            {
                Console.WriteLine("Shopping List Manager Menu");
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. List All Items");
                Console.WriteLine("3. List Owned Items");
                Console.WriteLine("4. List Items Not Owned");
                Console.WriteLine("5. List Items By:");
                Console.WriteLine("6. Find Items By Name");
                Console.WriteLine("7. Mark Items As Bought");
                Console.WriteLine("8. Update Items");
                Console.WriteLine("9. Change Items Status, Category, or Schedule");
                Console.WriteLine("10. Delete Items");
                Console.WriteLine("11. File Management Options");
                Console.WriteLine("0. Exit");
                Console.Write("Choose your option: ");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        _filelogger.Log("Adding a item.");
                        AddItem();
                        break;
                    case "2":
                        _filelogger.Log("Listing all items.");
                        ListAllItems();
                        break;
                    case "3":
                        _filelogger.Log("Listing all items that are owned.");
                        ListOwnedItems();
                        break;
                    case "4":
                        _filelogger.Log("Listing all items that are not owned.");
                        ListNotOwnedItems();
                        break;
                    case "5":
                        _filelogger.Log("Listing items by options.");
                        ListItemsByOptions();
                        break;
                    case "6":
                        _filelogger.Log("Finding item by name.");
                        SearchItems();
                        break;
                    case "7":
                        _filelogger.Log("Marking item as bought");
                        MarkItemAsBought();
                        break;
                    case "8":
                        _filelogger.Log("Updating item.");
                        UpdateItem();
                        break;
                    case "9":
                        _filelogger.Log("Changing item status, schedule, or category.");
                        ChangeItemsOptions();
                        break;
                    case "10":
                        _filelogger.Log("Deleting item");
                        DeleteItem();
                        break;
                    case "11":
                        _filelogger.Log("Opening the file management");
                        
                        _filelogger.Warn ("Work in Progress");
                        FileManageOption();
                        break;
                    case "0":
                        _filelogger.Log("Closing Shopping List Manager");
                        Console.WriteLine("Goodbye");
                        isRunning = false;
                        return;
                    default:
                        _consolelogger.Error("Invalid choice. Please try again with different option.");
                        _filelogger.Error("Invalid choice. Please try again with different option.");
                        break;
                }
            }
        }

        private void FileManageOption()
        {
            Console.WriteLine("Which option do you want?");
            Console.WriteLine("1. Save");
            Console.WriteLine("2. Load");
            var input = _userinput.GetIntInput("Choose an Option: ");
            switch (input)
            {
                case 1:
                    _filelogger.Log("Saving items");
                    try
                    {
                        SaveItems();
                    } catch (Exception ex)
                    {
                        _consolelogger.Error(ex.Message);
                        _filelogger.Error(ex.Message);
                    }
                    break;
                case 2:
                    _filelogger.Log("Loading items");
                    try
                    {
                        LoadItem();
                    } catch (Exception ex)
                    {
                        _consolelogger.Error(ex.Message);
                        _filelogger.Error(ex.Message);
                    }
                    break;
                default:
                    _consolelogger.Error("Invalid choice. Try again");
                    _filelogger.Error("Invalid choice. Try again");
                    break;
            }
        
        }

        private void SaveItems()
        {
            var filename = _userinput.GetStringInput("What the name of the file you want (without extension): ");
            Console.WriteLine("Creating File");
            string path = Path.Combine("Data", Entries.ITEMS.ToString(), $"{filename}.txt");
            if (File.Exists(path))
            {
                Console.Write($"Are you sure you want to overwrite {filename}.txt?");
                var overwrite = _userinput.GetStringInput(" (Y)es or (N)o: ").ToUpper();
                if (overwrite != "Y")
                    throw new Exception("Cancelled to override tasks");
            }
            _filelogger.Log($"Saving to {filename}");
            try
            {
                List<Items> items = _itemService.GetAllItems();
                _fileservice.SaveFunction(filename, Entries.ITEMS, null, items, null);
                _filelogger.Log("File have successfully saved");
                _consolelogger.Log($"File: {filename} have successfully saved");
            }
            catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }

        private void MarkItemAsBought()
        {
            try
            {
                var name = _userinput.GetStringInput("Enter the name of the item to mark it as bought: ");
                _itemService.MarkItemsAsBought(name);
                _consolelogger.Log("Item marked as bought.");
            } catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }

        private void LoadItem()
        {
            var filename = _userinput.GetStringInput("Enter the name of the file (without extension): ");
            string path = Path.Combine("Data", Entries.ITEMS.ToString(), $"{filename}.txt");
            if (!File.Exists(path))
            {
                throw new Exception("File not found. Invalid file name. Try again.");
            }
            _filelogger.Log($"Loading items from {filename}");
            _fileservice.LoadFunction(filename, Entries.ITEMS);
            _consolelogger.Log("Items loaded successfully");
            _filelogger.Log("Items loaded successfully");
        }

        private void DeleteItem()
        {
            try
            {
                var name = _userinput.GetStringInput("Enter the name to delete item: ");
                _itemService.DeleteItems(name);
                Console.WriteLine("Item deleted successfully");
            } catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }

        private void ChangeItemsOptions()
        {
            var input = _userinput.GetStringInput("Enter which options (Use (Sc)hedule, (St)atus, or (C)ategory: ");
            switch (input)
            {
                case "Sc":
                    ChangeItemSchedule();
                    break;
                case "St":
                    ChangeItemStatus();
                    break;
                case "C":
                    ChangeItemCategory();
                    break;
                default:
                    _consolelogger.Error("Invalid choice. Try again.");
                    _filelogger.Error("Invalid choice. Try again.");
                    break;
            }
        }

        private void ChangeItemCategory()
        {
            try
            {
                var name = _userinput.GetStringInput("Enter the name of the item to change category: ");
                var newcategory = _userinput.GetCategoryInput("Enter the new category to update it. Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inancial, (T)ransportation: ");
                _categoryservice.ChangeCategory(name, entries, newcategory);
                Console.WriteLine("Item updated successfully!");
            }
            catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }

        private void ChangeItemStatus()
        {
            try
            {
                var name = _userinput.GetStringInput("Enter the name of the item to change status: ");
                var newStatus = _userinput.GetItemStatusInput("Enter the new status to update it. Use (N)otBought, (B)ought, (O)rdered, (A)rrived, (D)elayed, (C)ancelled or empty for unknown: ");
                _statusservice.ChangeStatus(name, entries, newStatus);
                Console.WriteLine("Item updated successfully!");
            }
            catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }

        private void ChangeItemSchedule()
        {
            try
            {
                var name = _userinput.GetStringInput("Enter the name of the item to change schedule: ");
                var newschedule = _userinput.GetScheduleInput("Enter the new schedule to update it. Use (Y)early, (Q)uartly, (M)onthly, (W)eekly, or (D)aily: ");
                _scheduleservice.ChangeSchedule(name, entries, newschedule);
                Console.WriteLine("Item updated successfully!");
            } catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }

        private void UpdateItem()
        {
            try
            {
                var oldname = _userinput.GetStringInput("Enter the name of item to update: ");
                var newname = _userinput.GetStringInput("Enter the new name of the item: ");
                var newdesc = _userinput.GetStringInput("Enter the new description of the item: ");
                var newnote = _userinput.GetStringInput("Enter the new note of the item: ");
                _itemService.UpdateItems(oldname, newname, newdesc, newnote);
                _consolelogger.Log("Item updated successfully");
                _filelogger.Log("Item updated successfully");
            } catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }

        private void SearchItems()
        {
            try
            {
                var name = _userinput.GetStringInput("Enter the name of the item: ");
                var item = _itemService.FindItemsByName(name);
                if (item == null)
                    throw new Exception("Item not found");
                Console.WriteLine($"Found: {_formatter.FormatItems(item)}");
            }
            catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }

        private void ListItemsByOptions()
        {
            var input = _userinput.GetStringInput("Which option do you want? (Sc)hedule, (St)atus, or (C)ategory: ");
            switch (input)
            {
                case "Sc":
                    ListItemsBySchedule();
                    break;
                case "St":
                    ListItemsByStatus();
                    break;
                case "C":
                    ListItemsByCategory();
                    break;
                default:
                    _consolelogger.Error("Invalid choice. Try again.");
                    _filelogger.Error("Invalid choice. Try again.");
                    break;
            }
        }

        private void ListItemsByCategory()
        {
            try
            {
                var category = _userinput.GetCategoryInput("Select which category. Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inancial, (T)ransportation. ");
                var items = _categoryservice.ListItemsByCategory(category);
                if (items.Count == 0)
                {
                    _consolelogger.Error("No items found.");
                    _filelogger.Error("No items found.");
                    return;
                }
                _filelogger.Log($"Found {items.Count} {(items.Count == 1 ? "item" : "items")}");
                foreach (var item in items)
                {
                    _filelogger.Log($"Item: {item.Name}");
                    Console.WriteLine(_formatter.FormatItems(item));
                }
            }
            catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }

        private void ListItemsByStatus()
        {
            try
            {
                var status = _userinput.GetItemStatusInput("Select which status. Use (N)otBought, (B)ought, (O)rdered, (A)rrived, (D)elayed, (C)ancelled or empty for unknown: ");
                var items = _statusservice.ListItemsByStatus(status);
                if (items.Count == 0)
                {
                    _consolelogger.Error("No items found.");
                    _filelogger.Error("No items found.");
                    return;
                }
                _filelogger.Log($"Found {items.Count} {(items.Count == 1 ? "item" : "items")}");
                foreach (var item in items)
                {
                    _filelogger.Log($"Item: {item.Name}");
                    Console.WriteLine(_formatter.FormatItems(item));
                }
            }
            catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }

        private void ListItemsBySchedule()
        {
            try
            {
                var schedule = _userinput.GetScheduleInput("Select which schedule. Use (Y)early, (Q)uartly, (M)onthly, (W)eekly, or (D)aily. ");
                var items = _scheduleservice.ListItemsBySchedule(schedule);
                if (items.Count == 0)
                {
                    _consolelogger.Error("No items found.");
                    _filelogger.Error("No items found.");
                    return;
                }
                _filelogger.Log($"Found {items.Count} {(items.Count == 1 ? "item" : "items")}");
                foreach (var item in items)
                {
                    _filelogger.Log($"Item: {item.Name}");
                    Console.WriteLine(_formatter.FormatItems(item));
                }
            } catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }

        private void ListNotOwnedItems()
        {
            var items = _itemService.GetItemsNotOwned();
            if (items.Count == 0)
            {
                _consolelogger.Error("No items found.");
                _filelogger.Error("No items found.");
                return;
            }
            _filelogger.Log($"Found {items.Count} {(items.Count == 1 ? "item" : "items")}");
            foreach (var item in items)
            {
                _filelogger.Log($"Item: {item.Name}");
                Console.WriteLine(_formatter.FormatItems(item));
            }
        }

        private void ListOwnedItems()
        {
            var items = _itemService.GetItemsOwned();
            if (items.Count == 0)
            {
                _consolelogger.Error("No items found.");
                _filelogger.Error("No items found.");
                return;
            }
            _filelogger.Log($"Found {items.Count} {(items.Count == 1 ? "item" : "items")}");
            foreach (var item in items)
            {
                _filelogger.Log($"Item: {item.Name}");
                Console.WriteLine(_formatter.FormatItems(item));
            }
        }

        private void ListAllItems()
        {
            var items = _itemService.GetAllItems();
            if (items.Count == 0)
            {
                _consolelogger.Error("No items found.");
                _filelogger.Error("No items found.");
                return;
            }
            _filelogger.Log($"Found {items.Count} {(items.Count == 1 ? "item" : "items")}");
            foreach ( var item in items )
            {
                _filelogger.Log($"Item: {item.Name}");
                Console.WriteLine(_formatter.FormatItems( item ));
            }
        }

        private void AddItem()
        {
            try
            {
                var name = _userinput.GetStringInput("Enter the item name: ");
                var description = _userinput.GetStringInput("Enter the item description: ");
                var schedule = _userinput.GetScheduleInput("Enter the Schedule. Use (D)aily, (W)eekly, (M)onthly, (Q)uarterly, or (Y)early. ");
                var category = _userinput.GetCategoryInput("Enter the Category. Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inanical, or (T)ransportion. ");
                var note = _userinput.GetStringInput("Enter the note: ");
                _itemService.AddItems(new Items(name, description, schedule, 0, category, ItemStatus.NotBought, note, DateTime.Now));
                Console.WriteLine("Item have been added successfully");
                _filelogger.Log("Item added successfully");
            } catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }
    }
}
