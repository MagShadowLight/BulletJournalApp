using BulletJournalApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface IFileService
    {
        public void SaveTasks(string filename, List<Tasks> tasks);
        public void LoadTasks(string filename);
    }
}
