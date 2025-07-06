using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface ICategoryService
    {
        public List<Items> ListItemsByCategory(Category category);
        public List<Tasks> ListTasksByCategory(Category category);
        public void ChangeCategory(string title, Entries entries, Category category);
    }
}
