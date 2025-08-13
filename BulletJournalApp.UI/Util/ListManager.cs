using BulletJournalApp.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.UI.Util
{
    public class ListManager
    {
        string _input;
        UserInput _userinput = new UserInput();
        private IFileLogger _logger;

        public ListManager(IFileLogger logger)
        {
            _logger = logger;
        }
        public List<string> StringListManager()
        {
            List<string> list = new List<string>();
            while (true)
            {
                string str;
                _input = _userinput.GetStringInput("Task List Manager\n1. Add task to list\n2. Edit task\n 3. Delete task from list\n0. Exit\nChoose an options: ");
                switch (_input)
                {
                    case "1":
                        _logger.Log("Adding string to the list");
                        _logger.Log("Entering string");
                        str = _userinput.GetStringInput("Enter the task: ");
                        list.Add(str);
                        Console.WriteLine("Task added to the list");
                        _logger.Log("string have been added to the list");
                        break;
                    case "2":
                        str = _userinput.GetStringInput("Enter the task: ");
                        var tempstr1 = list.FirstOrDefault(str1 => str1 == str);
                        list.Remove(tempstr1);
                        str = _userinput.GetStringInput("Enter the new task: ");
                        list.Add(str);
                        Console.WriteLine("Task have been edited");
                        break;
                    case "3":
                        str = _userinput.GetStringInput("Enter the task: ");
                        var tempstr2 = list.FirstOrDefault(str1 => str1 == str);
                        list.Remove(tempstr2);
                        Console.WriteLine("Task have been removed from the list");
                        break;
                    case "0":
                        _logger.Log("Exiting string list manager");
                        Console.WriteLine("Exiting task list manager");
                        return list;
                }
            }
        }
    }
}
