using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface IFileService
    {
        public void SaveFunction(string filename, Entries entries, List<Tasks>? tasks, List<Items>? items);
        public void LoadFunction(string filename, Entries entries);
    }
}
