using BulletJournalApp.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.UI.Util
{
    public class UserInput : IUserInput
    {

        public UserInput() { }

        public string GetStringInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public DateTime GetDateInput(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            try
            {
                return DateTime.Parse(input);
            }
            catch (FormatException)
            {
                throw new FormatException("Invalid date format. Please use a valid date format.");
            }
        }

        public bool GetBooleanInput(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine().ToUpper();
            return input switch
            {
                "Y" => true,
                "N" => false,
                _ => throw new FormatException("Invalid Boolean Property.")
            };
        }

        public Priority GetPriorityInput(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.ToUpper();
            return input switch
            {
                "L" => Priority.Low,
                "M" => Priority.Medium,
                "H" => Priority.High,
                _ => throw new Exception("Invalid Priority Input. Use (L)ow, (M)edium, or (H)igh")
            };
        }

        public Category GetCategoryInput(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.ToUpper();
            return input switch
            {
                "N" => Category.None,
                "E" => Category.Education,
                "W" => Category.Works,
                "H" => Category.Home,
                "P" => Category.Personal,
                "F" => Category.Financial,
                "T" => Category.Transportation,
                _ => throw new Exception("Invalid Category Input. Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inancial, or (T)ransportation.")
            };
        }

        public Schedule GetScheduleInput(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.ToUpper();
            return input switch
            {
                "Y" => Schedule.Yearly,
                "Q" => Schedule.Quarterly,
                "M" => Schedule.Monthly,
                "W" => Schedule.Weekly,
                "D" => Schedule.Daily,
                _ => throw new Exception("Invalid Schedule Input. Use (Y)early, (Q)uarterly, (M)onthly, (W)eekly, or (D)aily")
            };
        }

        public TasksStatus GetTaskStatusInput(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.ToUpper();
            return input switch
            {
                "T" => TasksStatus.ToDo,
                "I" => TasksStatus.InProgress,
                "D" => TasksStatus.Done,
                "O" => TasksStatus.Overdue,
                "L" => TasksStatus.Late,
                _ => throw new Exception("Invalid Status Input. Use (T)oDo, (I)nProgress, (D)one, (O)verdue, or (L)ate")
            };
        }

        public ItemStatus GetItemStatusInput(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.ToUpper();
            return input switch
            {
                "N" => ItemStatus.NotBought,
                "B" => ItemStatus.Bought,
                "O" => ItemStatus.Ordered,
                "A" => ItemStatus.Arrived,
                "D" => ItemStatus.Delayed,
                "C" => ItemStatus.Cancelled,
                _ => ItemStatus.Unknown
            };
        }

        public int GetIntInput(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            
            if (int.TryParse(input, out int num))
            {
                return num;
            } else
            {
                throw new FormatException("Invalid Number");
            }
        }

        public DateTime GetOptionalDateInput(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (DateTime.TryParse(input, out DateTime date))
            {
                return date;
            } else
            {
                return DateTime.MinValue;
            }
        }
    }
}
