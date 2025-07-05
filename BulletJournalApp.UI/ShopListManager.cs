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
    public class ShopListManager
    {
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
                Console.WriteLine("7. Update Items");
                Console.WriteLine("8. Change Items Status, Category, or Schedule");
                Console.WriteLine("9. Delete Items");
                Console.WriteLine("10. Load Items");
                Console.WriteLine("0. Exit");
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
                        _filelogger.Log("Updating item.");
                        UpdateItem();
                        break;
                    case "8":
                        _filelogger.Log("Changing item status, schedule, or category.");
                        ChangeItemsOptions();
                        break;
                    case "9":
                        _filelogger.Log("Deleting item");
                        DeleteItem();
                        break;
                    case "10":
                        _filelogger.Log("Loading item");
                        LoadItem();
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

        private void LoadItem()
        {
            //throw new NotImplementedException();
        }

        private void DeleteItem()
        {
            //throw new NotImplementedException();
        }

        private void ChangeItemsOptions()
        {
            //throw new NotImplementedException();
        }

        private void UpdateItem()
        {
            //throw new NotImplementedException();
        }

        private void SearchItems()
        {
            //throw new NotImplementedException();
        }

        private void ListItemsByOptions()
        {
            //throw new NotImplementedException();
        }

        private void ListNotOwnedItems()
        {
            //throw new NotImplementedException();
        }

        private void ListOwnedItems()
        {
            //throw new NotImplementedException();
        }

        private void ListAllItems()
        {
            var items = _itemService.GetAllItems();
            if (items.Count == 0)
            {
                _consolelogger.Error("No items found.");
                _filelogger.Error("No items found.");
            }
            _filelogger.Log($"Found {items.Count} {(items.Count == 1 ? "item" : "items")}");
            foreach ( var item in items )
            {
                _filelogger.Log($"Item: {item.Name}");
                _formatter.FormatItems( item );
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
