using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Models;
using BulletJournalApp.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.UI
{
    public class TaskManager
    {
        private readonly ITaskService _taskservice;
        private readonly IConsoleLogger _consolelogger;
        private readonly IFileLogger _filelogger;
        private readonly IFormatter _formatter;
        public TaskManager(ITaskService taskservice, IConsoleLogger consolelogger, IFileLogger filelogger, IFormatter formatter)
        {
            _taskservice = taskservice;
            _consolelogger = consolelogger;
            _filelogger = filelogger;
            _formatter = formatter;
        }

        public void TaskManagerUI()
        {
            _filelogger.Log("Task Manager Opened.");
            Console.WriteLine("Welcome to To Do List");
            Console.WriteLine("");
            while (true) { 
            Console.WriteLine("Task Manager Menu");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. List All Tasks");
            Console.WriteLine("3. List Only Incomplete Tasks");
            Console.WriteLine("4. List Tasks by Priority");
            Console.WriteLine("5. List Tasks by Category");
            Console.WriteLine("6. List Tasks by Status");
            Console.WriteLine("7. List Tasks by Schedule");
            Console.WriteLine("8. Mark Tasks as Complete");
            Console.WriteLine("9. Find Tasks by Title");
            Console.WriteLine("10. Update Task");
            Console.WriteLine("11. Change Priority in Task");
            Console.WriteLine("12. Change Status in Task");
            Console.WriteLine("13. Change Category in Task");
            Console.WriteLine("14. Change Schedule in Task");
            Console.WriteLine("15. Delete Tasks");
            Console.WriteLine("16. Load Tasks");
            Console.WriteLine("0. Exit");
            Console.Write("Choose a options: ");
            var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        _filelogger.Log("Adding task");
                        AddTask();
                        break;
                    case "2":
                        _filelogger.Log("Listing all the tasks");
                        ListAllTasks();
                        break;
                    case "3":
                        _filelogger.Log("Listing all incomplete tasks");
                        ListIncompleteTask();
                        break;
                    case "4":
                        _filelogger.Log("Listing all tasks with priority");
                        ListTaskByPriority();
                        break;
                    case "5":
                        _filelogger.Log("Listing all tasks with category");
                        ListTaskByCategory();
                        break;
                    case "6":
                        _filelogger.Log("Listing all tasks with status");
                        ListTaskByStatus();
                        break;
                    case "7":
                        _filelogger.Log("Listing all tasks with schedule");
                        ListTaskBySchedule();
                        break;
                    case "8":
                        _filelogger.Log("Marking task as complete");
                        MarkTasksComplete();
                        break;
                    case "9":
                        _filelogger.Log("Searching task by title");
                        FindTaskByTitle();
                        break;
                    case "10":
                        _filelogger.Log("Updating task");
                        UpdateTask();
                        break;
                    case "11":
                        _filelogger.Log("Changing task priority");
                        ChangeTaskPriority();
                        break;
                    case "12":
                        _filelogger.Log("Changing task status");
                        ChangeTaskStatus();
                        break;
                    case "13":
                        _filelogger.Log("Changing task category");
                        ChangeTaskCategory();
                        break;
                    case "14":
                        _filelogger.Log("Changing task schedule");
                        ChangeTaskSchedule();
                        break;
                    case "15":
                        _filelogger.Log("Deleting task. Warning: once it deleted, there is no way to recover");
                        DeleteTask();
                        break;
                    case "16":
                        _filelogger.Log("This option is not ready yet, Please come back when it updated with this function");
                        Console.WriteLine("Work in progress. Please come back later");
                        break;
                    case "0":
                        _filelogger.Log("Exiting");
                        Console.WriteLine("Do you want to save the Tasks? Y or N");
                        var saveinput = Console.ReadLine().ToUpper();
                        if (saveinput == "Y")
                        {
                            _filelogger.Log("Saving task");
                            SaveTasks();
                        }
                        Console.WriteLine("Good Bye");
                        _filelogger.Log("Closing Task Manager");
                        return;
                    default:
                        _consolelogger.Error("Invalid Choice. Please Try Again");
                        _filelogger.Error("Invalid Choice. Please Try Again");
                        break;
                }
            }
        }

        public void AddTask()
        {
            Priority priorityoutput;
            Category categoryoutput;
            Schedule scheduleoutput;
            DateTime dueDateOutput;
            Console.Write("Enter the task title: ");
            var title = Console.ReadLine();
            Console.Write("Enter the task description: ");
            var description = Console.ReadLine();
            Console.Write("Enter the Due Date (Use MMM DD, YYYY): ");
            var dueDate = Console.ReadLine();
            Console.Write("Enter the Priority (Use (L)ow, (M)edium, or (H)igh: ");
            var priority = Console.ReadLine()?.ToUpper();
            Console.Write("Enter the Category (Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inancial): ");
            var category = Console.ReadLine()?.ToUpper();
            Console.Write("Enter the Schedule (Use (Y)early, (Q)uarterly, (M)onthly, (W)eekly, or (D)aily): ");
            var schedule = Console.ReadLine()?.ToUpper();
            Console.WriteLine("Enter the Note for the task:");
            var notes = Console.ReadLine();
            try
            {
                priorityoutput = ConvertToPriority(priority);
                categoryoutput = ConvertToCategory(category);
                scheduleoutput = ConvertToSchedule(schedule);
                dueDateOutput = DateTime.Parse(dueDate);
                var task = new Tasks(dueDateOutput, title, description, scheduleoutput, priorityoutput, categoryoutput, notes);
                if (_taskservice.AddTask(task))
                {
                    _consolelogger.Log("Task added successfully");
                    _filelogger.Log("Task added successfully");
                } else
                {
                    _consolelogger.Log("An Task with this title already exists");
                    _filelogger.Log("An Task with this title already exists");
                }
            } catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }

        private Schedule ConvertToSchedule(string? schedule)
        {
            return schedule switch
            {
                "Y" => Schedule.Yearly,
                "Q" => Schedule.Quarterly,
                "M" => Schedule.Monthly,
                "W" => Schedule.Weekly,
                "D" => Schedule.Daily,
                _ => throw new Exception("Invalid Schedule Input. Use (Y)early, (Q)uarterly, (M)onthly, (W)eekly, or (D)aily")
            };
        }

        private Category ConvertToCategory(string? category)
        {
            return category switch
            {
                "N" => Category.None,
                "E" => Category.Education,
                "W" => Category.Works,
                "H" => Category.Home,
                "P" => Category.Personal,
                "F" => Category.Financial,
                _ => throw new Exception("Invalid Category Input. Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, or (F)inancial")
            };
        }

        private Priority ConvertToPriority(string? priority)
        {
            return priority switch
            {
                "L" => Priority.Low,
                "M" => Priority.Medium,
                "H" => Priority.High,
                _ => throw new Exception("Invalid Priority Input. Use (L)ow, (M)edium, or (H)igh")
            };
        }

        public void ListAllTasks()
        {
            var tasks = _taskservice.ListAllTasks();
            if (tasks.Count == 0)
            {
                _consolelogger.Error("No tasks found");
                _filelogger.Error("No tasks found");
                return;
            }
            _filelogger.Log($"Found {tasks.Count} {(tasks.Count == 1 ? "task" : "tasks")}");
            foreach (var task in tasks)
            {
                _filelogger.Log($"Task: {task.Title}");
                Console.WriteLine(_formatter.Format(task));
                if (task.IsOverdue())
                {
                    _consolelogger.Warn($"This task, {task.Title}, is overdue!");
                }
            }

        }
        public void ListIncompleteTask()
        {
            var tasks = _taskservice.ListIncompleteTasks();
            if (tasks.Count == 0)
            {
                _consolelogger.Error("No tasks found");
                _filelogger.Error("No tasks found");
                return;
            }
            _filelogger.Log($"Found {tasks.Count} {(tasks.Count == 1 ? "task" : "tasks")}");
            foreach (var task in tasks)
            {
                _filelogger.Log($"Task: {task.Title}");
                Console.WriteLine(_formatter.Format(task));
                if (task.IsOverdue())
                {
                    _consolelogger.Warn($"This task, {task.Title}, is overdue!");
                }
            }

        }
        public void ListTaskByPriority()
        {
            Console.Write("Enter the Priority (Use (L)ow, (M)edium, or (H)igh): ");
            var priorityinput = Console.ReadLine();
            Priority priorityoutput;
            try
            {
               priorityoutput = ConvertToPriority(priorityinput);
            } catch (Exception ex)
            {
                _consolelogger.Error("Invalid Priority Value. Use (L)ow, (M)edium, or (H)igh");
                _filelogger.Error("Invalid Priority Value. Use (L)ow, (M)edium, or (H)igh");
                return;
            }
            var tasks = _taskservice.ListTasksByPriority(priorityoutput);
            if (tasks.Count == 0)
            {
                _consolelogger.Error("No tasks found");
                _filelogger.Error("No tasks found");
                return;
            }
            _filelogger.Log($"Found {tasks.Count} {(tasks.Count == 1 ? "task" : "tasks")} with {priorityoutput.ToString()} Priority");
            foreach (var task in tasks)
            {
                Console.WriteLine(_formatter.Format(task));
                _filelogger.Log($"Task: {task.Title}");
                if (task.IsOverdue())
                {
                    _consolelogger.Warn($"This task, {task.Title}, is overdue!");
                }
            }
            
        }
        public void ListTaskByCategory()
        {
            Console.Write("Enter the Category (Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inancial): ");
            var categoryinput = Console.ReadLine();
            Category categoryoutput;
            try
            {
                categoryoutput = ConvertToCategory(categoryinput);
            }
            catch (Exception ex)
            {
                _consolelogger.Error("Invalid Category Value");
                _filelogger.Error("Invalid Category Value");
                return;
            }
            var tasks = _taskservice.ListTasksByCategory(categoryoutput);
            if (tasks.Count == 0)
            {
                _consolelogger.Error("No tasks found");
                _filelogger.Error("No tasks found");
                return;
            }
            _filelogger.Log($"Found {tasks.Count} {(tasks.Count == 1 ? "task" : "tasks")}");
            foreach (var task in tasks)
            {
                _filelogger.Log($"Task: {task.Title}");
                Console.WriteLine(_formatter.Format(task));
                if (task.IsOverdue())
                {
                    _consolelogger.Warn($"This task, {task.Title}, is overdue!");
                }
            }
        }
        public void ListTaskByStatus()
        {
            Console.Write("Enter the Status of the Task (Use (T)oDo, (I)nProgress, (D)one, (O)verdue, (L)ate): ");
            var statusinput = Console.ReadLine();
            TasksStatus statusoutput;
            try
            {
                statusoutput = ConvertToStatus(statusinput);
            }
            catch (Exception ex)
            {
                _consolelogger.Error("Invalid Status Value");
                _filelogger.Error("Invalid Status Value");
                return;
            }
            var tasks = _taskservice.ListTasksByStatus(statusoutput);
            if (tasks.Count == 0)
            {
                _consolelogger.Error("No tasks found");
                _filelogger.Error("No tasks found");
                return;
            }
            _filelogger.Log($"Found {tasks.Count} {(tasks.Count == 1 ? "task" : "tasks")} with {statusoutput.ToString()} status");
            foreach (var task in tasks)
            {
                _filelogger.Log($"Task: {task}");
                Console.WriteLine(_formatter.Format(task));
                if (task.IsOverdue())
                {
                    _consolelogger.Warn($"This task, {task.Title}, is overdue!");
                }
            }
        }

        private TasksStatus ConvertToStatus(string? status)
        {
            return status switch
            {
                "T" => TasksStatus.ToDo,
                "I" => TasksStatus.InProgress,
                "D" => TasksStatus.Done,
                "O" => TasksStatus.Overdue,
                "L" => TasksStatus.Late,
                _ => throw new Exception("Invalid Status Input. Use (T)oDo, (I)nProgress, (D)one, (O)verdue, or (L)ate")
            };
        }

        // 
        public void ListTaskBySchedule()
        {
            Console.Write("Enter the Schedule (Use (Y)early, (Q)uarterly, (M)onthly, (W)eekly, or (D)aily): ");
            var scheduleinput = Console.ReadLine();
            Schedule scheduleoutput;
            try
            {
                scheduleoutput = ConvertToSchedule(scheduleinput);
            }
            catch (Exception ex)
            {
                _consolelogger.Error("Invalid Schedule Value");
                _filelogger.Error("Invalid Schedule Value");
                return;
            }
            var tasks = _taskservice.ListTasksBySchedule(scheduleoutput);
            if (tasks.Count == 0)
            {
                _consolelogger.Error("No tasks found");
                _filelogger.Error("No tasks found");
                return;
            }
            _filelogger.Log($"Found {tasks.Count} {(tasks.Count == 1 ? "task" : "tasks")} with {scheduleoutput.ToString()} schedule");
            foreach (var task in tasks)
            {
                _filelogger.Log($"Task: {task.Title}");
                Console.WriteLine(_formatter.Format(task));
                if (task.IsOverdue())
                {
                    _consolelogger.Warn($"This task, {task.Title}, is overdue!");
                }
            }
        }
        public void FindTaskByTitle()
        {
            Console.Write("Enter the title to search: ");
            var title = Console.ReadLine();
            var task = _taskservice.FindTasksByTitle(title);

            if (task != null)
            {
                Console.WriteLine("Found: " + _formatter.Format(task));
                _filelogger.Log($"Found: {task.Title}");
            } else
            {
                _consolelogger.Error("Task not found");
                _filelogger.Error("Task not found");
            }
        }
        public void MarkTasksComplete()
        {
            Console.Write("Enter the title to mark task as complete: ");
            var title = Console.ReadLine();
            try
            {
                _taskservice.MarkTasksComplete(title);
                _consolelogger.Log("Task marked as complete.");
                _filelogger.Log($"Task: {title}, marked as complete.");
            } catch (Exception ex)
            {
                _consolelogger.Error("Task not found.");
                _filelogger.Error($"Task: {title} not found.");
            }
        }
        public void UpdateTask()
        {
            Console.Write("Enter the current title of task to update: ");
            var oldtitle = Console.ReadLine();
            Console.Write("Enter the new title: ");
            var newtitle = Console.ReadLine();
            Console.Write("Enter the new description: ");
            var newdescription = Console.ReadLine();
            Console.Write("Enter the new note: ");
            var newnote = Console.ReadLine();
            Console.Write("Enter the new due date: ");
            var newstringduedate = Console.ReadLine();
            DateTime? newduedate;
            try
            {
                newduedate = DateTime.Parse(newstringduedate);
            } catch (Exception ex)
            {
                newduedate = null;
            }
            try
            {
                _taskservice.UpdateTask(oldtitle, newtitle, newdescription, newnote, newduedate);
                _consolelogger.Log($"Task: {oldtitle} updated successfully");
                _filelogger.Log($"Task: {oldtitle} updated successfully");
            } catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }
        public void ChangeTaskPriority()
        {
            Console.Write("Enter the title to change priority: ");
            var title = Console.ReadLine();
            Console.Write("Enter the new priority (Use (L)ow, (M)edium, or (H)igh");
            var priorityinput = Console.ReadLine();
            Priority priorityoutput;
            try
            {
                priorityoutput = ConvertToPriority(priorityinput);
                _taskservice.ChangePriority(title, priorityoutput);
                _consolelogger.Log($"Task: {title} priority changed successfully");
                _filelogger.Log($"Task: {title} priority changed successfully");
            } catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }
        public void ChangeTaskStatus()
        {
            Console.Write("Enter the title of task to change Status: ");
            var title = Console.ReadLine();
            Console.Write("Enter the new status (Use (T)oDo, (I)nProgress, (D)one, (O)verdue, or (L)ate): ");
            var statusinput = Console.ReadLine();
            TasksStatus statusoutput;
            try
            {
                statusoutput = ConvertToStatus(statusinput);
                _taskservice.ChangeStatus(title, statusoutput);
                _consolelogger.Log($"Task: {title} status changed successfully");
                _filelogger.Log($"Task: {title} status changed successfully");
            } catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }
        public void ChangeTaskCategory()
        {
            Console.Write("Enter the task title to change Category: ");
            var title = Console.ReadLine();
            Console.Write("Enter the new category (Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inancial): ");
            var categoryinput = Console.ReadLine();
            Category categoryoutput;
            try
            {
                categoryoutput = ConvertToCategory(categoryinput);
                _taskservice.ChangeCategory(title, categoryoutput);
                _consolelogger.Log($"Task: {title} category changed successfully");
                _filelogger.Log($"Task: {title} category changed successfully");
            } catch(Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }
        public void ChangeTaskSchedule()
        {
            Console.Write("Enter the task title to change Schedule: ");
            var title = Console.ReadLine();
            Console.Write("Enter the new schedule (Use (Y)early, (Q)uarterly, (M)onthly, (W)eekly, or (D)aily): ");
            var scheduleinput = Console.ReadLine();
            Schedule scheduleoutput;
            try
            {
                scheduleoutput = ConvertToSchedule(scheduleinput);
                _taskservice.ChangeSchedule(title, scheduleoutput);
                _consolelogger.Log($"Task: {title} schedule changed successfully");
                _filelogger.Log($"Task: {title} schedule changed successfully");
            }
            catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }
        public void DeleteTask()
        {
            Console.Write("Enter the title to delete: ");
            var title = Console.ReadLine();
            try
            {
                _taskservice.DeleteTask(title);
                _consolelogger.Log("Task deleted successfully");
                _filelogger.Log("Task deleted successfully");
            } catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }
        public void SaveTasks()
        {
            Console.WriteLine("What the file name of the Tasks: ");
            var filename = Console.ReadLine();
            Console.WriteLine("Creating File");
            _filelogger.Log($"Saving to {filename}");
            try
            {
                List<Tasks> tasks = _taskservice.ListAllTasks();
                _taskservice.SaveTasks(filename, tasks);
                _filelogger.Log("File have successfully saved");
                _consolelogger.Log($"File: {filename} have successfully saved");
            } catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
            }
        }
        public List<Tasks> LoadTask()
        {
            throw new NotImplementedException();
        }
    }
}
