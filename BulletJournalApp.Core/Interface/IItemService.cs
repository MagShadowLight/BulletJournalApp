using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface IItemService
    {
        public void AddItems(Items item);
        public List<Items> GetAllItems();
        public List<Items> GetItemsNotOwned();
        public List<Items> GetItemsOwned();
        public Items FindItemsByName(string name);
        public void MarkItemsAsBought(string name);
        public void UpdateItems(string oldName, string newName, string newDescription, string newNotes);
        public void DeleteItems(string name);
    }
}
