using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Util
{
    public class FileInputOutput
    {
        public void ResetStream(StreamReader sr, FileStream fs)
        {
            sr.Close();
            fs.Close();
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        public void CreateFile(string path)
        {
            if (!File.Exists(path))
            {
                if (!Directory.Exists("Temp"))
                {
                    Directory.CreateDirectory("Temp");
                }
                File.Create(path).Close();
            }
        }
        public void DeleteFile(string path)
        {
            File.Delete(path);
        }
    }
}
