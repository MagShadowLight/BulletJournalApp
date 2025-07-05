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
    public class ItemService : IItemService
    {
        private readonly IConsoleLogger _consolelogger;
        private readonly IFileLogger _filelogger;
        public List<Items> items = new();
        public ItemService(IConsoleLogger consolelogger, IFileLogger filelogger)
        {
            _consolelogger = consolelogger;
            _filelogger = filelogger;
        }

        public void AddItems(Items item)
        {
            ValidateDuplication(item.Name);
            items.Add(item);
        }

        public void DeleteItems(string name)
        {
            var item = FindItemsByName(name);
            if (item == null)
                throw new Exception($"{name} not found");
            items.Remove(item);
        }

        public Items FindItemsByName(string name)
        {
            return items.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Items> GetAllItems()
        {
            return items;
        }

        public List<Items> GetItemsNotOwned()
        {
            return items.Where(items => items.Status != ItemStatus.Bought).ToList();
        }

        public List<Items> GetItemsOwned()
        {
            return items.Where(item => item.Status == ItemStatus.Bought).ToList();
        }

        public void MarkItemsAsBought(string name)
        {
            var item = FindItemsByName(name);
            if (item == null)
                throw new Exception($"{name} not found");
            item.MarkAsBought();
        }

        public void UpdateItems(string oldName, string newName, string newDescription, string newNotes)
        {
            var item = FindItemsByName(oldName);
            if (item == null)
                throw new Exception($"{oldName} not found");
            ValidateDuplication(newName);
            item.Update(newName, newDescription, newNotes);
        }
        private void ValidateDuplication(string name)
        {
            foreach(var i in items)
            {
                if (i.Name.Equals(name))
                    throw new Exception("This item cannot be duplicated");
            }
        }
    }
}
