using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Util
{
    public class ConsoleInputOutput
    {
        public void ResetReader()
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
    }
}
