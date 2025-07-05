using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface IItemStatusService
    {
        public List<Items> ListItemsByStatus(ItemStatus status);
        public void ChangeStatus(string title, Entries entries, ItemStatus status);
    }
}
