using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface IConsoleLogger
    {
        public void Log(string message);
        public void Warn(string message);
        public void Error(string message);
    }
}
