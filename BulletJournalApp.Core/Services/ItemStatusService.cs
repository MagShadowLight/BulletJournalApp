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
    public class ItemStatusService : IItemStatusService
    {
        private readonly IItemService _itemservice;
        public ItemStatusService(IItemService itemservice)
        {
            _itemservice = itemservice;
        }

        public void ChangeStatus(string title, Entries entries, ItemStatus status)
        {
            switch (entries)
            {
                case Entries.ITEMS:
                    var item = _itemservice.FindItemsByName(title);
                    if (item == null)
                        throw new ArgumentNullException("Cannot find item");
                    item.ChangeStatus(status);
                    break;
            }
        }

        public List<Items> ListItemsByStatus(ItemStatus status)
        {
            var items = _itemservice.GetAllItems();
            return items.Where(item => item.Status == status).ToList();
        }
    }
}
