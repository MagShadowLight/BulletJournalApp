using BulletJournalApp.Core.Models.Enum;
using BulletJournalApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface ICategoryService
    {
        public List<Tasks> ListTasksByCategory(Category category);
        public void ChangeCategory(string title, Category category);
    }
}
