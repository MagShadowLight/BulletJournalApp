﻿using BulletJournalApp.Core.Interface;
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
                        _filelogger.Log("Loading tasks from txt file");
                        try
                        {
                            LoadTask();
                        } catch (Exception ex)
                        {
                            _consolelogger.Error(ex.Message);
                            _filelogger.Error(ex.Message);
                        }

                        break;
                    case "0":
                        _filelogger.Log("Exiting");
                        Console.WriteLine("Do you want to save the Tasks? (Y)es, (N)o, or (C)ancel");
                        var saveinput = Console.ReadLine().ToUpper();
                        if (saveinput == "Y")
                        {
                            _filelogger.Log("Saving task");
                            try
                            {
                                SaveTasks();
                            } catch (Exception ex)
                            {
                                _filelogger.Log(ex.Message);
                                break;
                            }
                        } else if (saveinput == "C")
                        {
                            _filelogger.Log("Cancelled to save tasks");
                            break;
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
            var title = UserInput.GetStringInput("Enter the task title: ");
            var description = UserInput.GetStringInput("Enter the task description: ");
            try
            {
                var dueDate = UserInput.GetDateInput("Enter the Due Date (Use MMM DD, YYYY): ");
                var priority = UserInput.GetPriorityInput("Enter the Priority (Use (L)ow, (M)edium, or (H)igh): ");
                var category = UserInput.GetCategoryInput(("Enter the Category (Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inancial): "));
                var schedule = UserInput.GetScheduleInput("Enter the Schedule (Use (Y)early, (Q)uarterly, (M)onthly, (W)eekly, or (D)aily): ");
                var notes = UserInput.GetStringInput("Enter the Note for the task: ");                
                var task = new Tasks(dueDate, title, description, schedule, priority, category, notes);
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
            Priority priority;
            try
            {
                priority = UserInput.GetPriorityInput("Enter the Priority (Use (L)ow, (M)edium, or (H)igh): ");               
            } catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
                return;
            }
            var tasks = _taskservice.ListTasksByPriority(priority);
            if (tasks.Count == 0)
            {
                _consolelogger.Error("No tasks found");
                _filelogger.Error("No tasks found");
                return;
            }
            _filelogger.Log($"Found {tasks.Count} {(tasks.Count == 1 ? "task" : "tasks")} with {priority.ToString()} Priority");
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
            Category category;
            try
            {
                category = UserInput.GetCategoryInput("Enter the Category (Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inancial): ");
            }
            catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
                return;
            }
            var tasks = _taskservice.ListTasksByCategory(category);
            if (tasks.Count == 0)
            {
                _consolelogger.Error("No tasks found");
                _filelogger.Error("No tasks found");
                return;
            }
            _filelogger.Log($"Found {tasks.Count} {(tasks.Count == 1 ? "task" : "tasks")} with {category.ToString()} Category");
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
            TasksStatus status;
            try
            {
                 status = UserInput.GetStatusInput("Enter the Status of the Task (Use (T)oDo, (I)nProgress, (D)one, (O)verdue, or (L)ate): ");
            }
            catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
                return;
            }
            var tasks = _taskservice.ListTasksByStatus(status);
            if (tasks.Count == 0)
            {
                _consolelogger.Error("No tasks found");
                _filelogger.Error("No tasks found");
                return;
            }
            _filelogger.Log($"Found {tasks.Count} {(tasks.Count == 1 ? "task" : "tasks")} with {status.ToString()} status");
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
        public void ListTaskBySchedule()
        {
            Schedule schedule;
            try
            {
                schedule = UserInput.GetScheduleInput("Enter the Schedule (Use (Y)early, (Q)uarterly, (M)onthly, (W)eekly, or (D)aily): ");
            }
            catch (Exception ex)
            {
                _consolelogger.Error(ex.Message);
                _filelogger.Error(ex.Message);
                return;
            }
            var tasks = _taskservice.ListTasksBySchedule(schedule);
            if (tasks.Count == 0)
            {
                _consolelogger.Error("No tasks found");
                _filelogger.Error("No tasks found");
                return;
            }
            _filelogger.Log($"Found {tasks.Count} {(tasks.Count == 1 ? "task" : "tasks")} with {schedule.ToString()} schedule");
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
            var title = UserInput.GetStringInput("Enter the title to search: ");
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
            var title = UserInput.GetStringInput("Enter the title to mark task as complete: ");
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
            var oldtitle = UserInput.GetStringInput("Enter the current title of task to update: ");
            var newtitle = UserInput.GetStringInput("Enter the new title: ");
            var newdescription = UserInput.GetStringInput("Enter the new description: ");
            var newnote = UserInput.GetStringInput("Enter the new note: ");
            DateTime? newduedate;
            try
            {
                newduedate = UserInput.GetDateInput("Enter the new due date (Use MMM DD, YYYY): ");
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
            var title = UserInput.GetStringInput("Enter the title of task to change priority: ");
            Console.Write("Enter the new priority (Use (L)ow, (M)edium, or (H)igh");
            Priority priority;
            try
            {
                priority = UserInput.GetPriorityInput("Enter the new priority (Use (L)ow, (M)edium, or (H)igh): ");
                _taskservice.ChangePriority(title, priority);
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
            var title = UserInput.GetStringInput("Enter the title of task to change Status: ");
            TasksStatus status;
            try
            {
                status = UserInput.GetStatusInput("Enter the new status (Use (T)oDo, (I)nProgress, (D)one, (O)verdue, or (L)ate): ");
                _taskservice.ChangeStatus(title, status);
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
            var title = UserInput.GetStringInput("Enter the task title to change Category: ");
            Category category;
            try
            {
                category = UserInput.GetCategoryInput("Enter the new category (Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, (F)inancial): ");
                _taskservice.ChangeCategory(title, category);
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
            var title = UserInput.GetStringInput("Enter the task title to change Schedule: ");
            Schedule schedule;
            try
            {
                schedule = UserInput.GetScheduleInput("Enter the new schedule (Use (Y)early, (Q)uarterly, (M)onthly, (W)eekly, or (D)aily): ");
                _taskservice.ChangeSchedule(title, schedule);
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
            var title = UserInput.GetStringInput("Enter the title to delete: ");
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
            var filename = UserInput.GetStringInput("Enter the file name of the Tasks (without extension): ");
            Console.WriteLine("Creating File");
            string path = Path.Combine("Data", "Tasks", $"{filename}.txt");
            if (File.Exists(path))
            {
                Console.Write($"Are you sure you want to overwrite {filename}.txt?");
                var overwrite = UserInput.GetStringInput(" (Y)es or (N)o: ").ToUpper();
                if (overwrite != "Y")
                    throw new Exception("Cancelled to override tasks");
            }
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
        public void LoadTask()
        {
            var filename = UserInput.GetStringInput("Enter the name of the file (without extension): ");
            string path = Path.Combine("Data", "Tasks", $"{filename}.txt");
            if (!File.Exists(path))
            {
                throw new Exception("File not found. Invalid file name. Try again.");
            }
            _filelogger.Log($"Loading tasks from {filename}");
            _taskservice.LoadTasks(filename);
            _consolelogger.Log("Tasks loaded successfully");
            _filelogger.Log("Tasks loaded successfully");
        }
    }

    public class UserInput
    {
        public static string GetStringInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public static DateTime GetDateInput(string prompt)
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

        public static Priority GetPriorityInput(string prompt)
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

        public static Category GetCategoryInput(string prompt)
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
                _ => throw new Exception("Invalid Category Input. Use (N)one, (E)ducation, (W)orks, (H)ome, (P)ersonal, or (F)inancial")
            };
        }

        public static Schedule GetScheduleInput(string prompt)
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

        public static TasksStatus GetStatusInput(string prompt)
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
    }
}
