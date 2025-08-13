using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Data
{
    public class LogMessageData
    {
        public static IEnumerable<object[]> GetMessage()
        {
            yield return new object[] { "Opening Logger" };
            yield return new object[] { "Write something" };
            yield return new object[] { "Add something" };
            yield return new object[] { "Update something" };
            yield return new object[] { "Edit something" };
            yield return new object[] { "Delete something" };
            yield return new object[] { "Listing something" };
            yield return new object[] { "Closing Logger" };
        }
    }
}
