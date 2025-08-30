using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.UI.Data
{
    public class ListManagerTestData
    {
        public static IEnumerable<object[]> GetInputForEdit()
        {
            yield return new object[] { "2\n\n0", "Value cannot be null. (Parameter 'str must not be null or empty string')" };
            yield return new object[] { "2\nmeow\n\n0", "Value cannot be null. (Parameter 'str must not be null or empty string')" };
        }
    }
}
