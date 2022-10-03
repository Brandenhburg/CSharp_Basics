using CSharp_Basics_HW.Data;
using CSharp_Basics_HW.Enums;
using CSharp_Basics_HW.Models;
using System.Diagnostics.SymbolStore;

namespace CSharp_Basics_HW.Menus
{
    internal static class TaskMenu
    {
        #region [Menus]
        private static void Menu()
        {
            Console.WriteLine("1. Create Task.");
            Console.WriteLine("2. Find task by Id.");
            Console.WriteLine("3. Back to Main.");
            Console.Write("Type Here: ");
        }
        private static void SelectPriorityMenu()
        {
            Console.WriteLine("Select priority level: ");
            Console.WriteLine($"1. {TaskPriority.Low}");
            Console.WriteLine($"2. {TaskPriority.Medium}");
            Console.WriteLine($"3. {TaskPriority.High}");
        }
        #endregion

        #region[Menu Handlers]

        public static void MenuHandler()
        {
            int inputVal = -1;
            string input = "";
            bool isParsed = false;

            do
            {
                Console.Clear();
                Menu();
                input = Console.ReadLine();
                isParsed = int.TryParse(input, out inputVal);

            } while (isParsed == false || inputVal < 1 || inputVal > 3);


            switch (inputVal)
            {
                case 1:
                    CreateTask();
                    break;
                case 2:
                    FindTaskById();
                    break;
                case 3:
                    Console.Clear();
                    MainMenu.MenuHandler();
                    break;
                default:
                    break;
            }
        }
        private static void TaskSubMenuHandler(Models.Task task)
        {
            ShowTaskDetails(task);
            Console.WriteLine();
            string input = "";
            int inputVal = -1;
            bool isParsed = false;

            if (task.IsAssigned)
            {
                do
                {
                    Console.WriteLine("1. Update Task");
                    Console.WriteLine("2. Back to Task Menu.");
                    Console.Write("Type Here: ");

                    input = Console.ReadLine();
                    isParsed = int.TryParse(input, out inputVal);

                } while (isParsed == false || inputVal < 1 || inputVal > 2);

                switch (inputVal)
                {
                    case 1:
                        UpdateTask(task);
                        break;
                    case 2:
                        MenuHandler();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                do
                {
                    Console.WriteLine("1. Assign this task to employee.");
                    Console.WriteLine("2. Update Task.");
                    Console.WriteLine("3. Back to Task Menu.");
                    Console.Write("Type Here: ");

                    input = Console.ReadLine();
                    isParsed = int.TryParse(input, out inputVal);

                } while (isParsed == false || inputVal < 1 || inputVal > 3);

                switch (inputVal)
                {
                    case 1:
                        {
                            EmployeesMockDatabase.GetAllEmployeesInfo();
                            AssignTaskToEmployee(task);
                        }
                        break;
                    case 2:
                        UpdateTask(task);
                        break;
                    case 3:
                        MenuHandler();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion[]
        private static void CreateTask()
        {
            Models.Task newTask = new();

            Console.WriteLine();
            Console.WriteLine("------Task Creation Menu-----");

            int taskId = TasksMockDatabase.Tasks.Count + 1;
            newTask.TaskId = taskId.ToString();
            

            #region [Type Task Name]
            do
            {
                Console.Write("Task name: ");
                newTask.TaskName = Console.ReadLine();

                if (string.IsNullOrEmpty(newTask.TaskName))
                {
                    Console.WriteLine("Task name ca not be empty.");
                }

                if (TasksMockDatabase.Tasks.Exists(t => t.TaskName == newTask.TaskName))
                {
                    Console.WriteLine("A task with the specified name already exists.");
                }

            } while (string.IsNullOrEmpty(newTask.TaskName) || TasksMockDatabase.Tasks.Exists(t => t.TaskName == newTask.TaskName));
            #endregion

            #region [Select TaskPriority]
            SelectPriorityMenu();

            string input = "";
            int inputVal = -1;
            bool isParsed = false;

            do
            {
                Console.Write("Priority: ");
                input = Console.ReadLine();
                isParsed = int.TryParse(input, out inputVal);

            } while (isParsed == false || inputVal < 1 || inputVal > 3);

            switch (inputVal)
            {
                case 1:
                    newTask.TaskPriority = TaskPriority.Low;
                    break;
                case 2:
                    newTask.TaskPriority = TaskPriority.Medium;
                    break;
                case 3:
                    newTask.TaskPriority = TaskPriority.High;
                    break;
                default:
                    break;
            }

            #endregion

            #region [Type Description]
            do
            {
                Console.Write("Description: ");
                newTask.Description = Console.ReadLine();

                if (string.IsNullOrEmpty(newTask.Description))
                {
                    Console.WriteLine("Description can not be empty.");
                }

            } while (string.IsNullOrEmpty(newTask.Description));
            #endregion


            newTask.DateOfCreation = DateTime.UtcNow;
            newTask.DateOfEffectuation = DateTime.UtcNow.AddDays(1);
            newTask.IsAssigned = false;
            newTask.State = false;

            TasksMockDatabase.Tasks.Add(newTask);

            Console.WriteLine();
            Console.WriteLine("Done. Go to the menu to modify or asign this task to someone.");
            TaskSubMenuHandler(newTask);
        }
        private static void FindTaskById()
        {
            int inputVal = -1;
            string input = "";
            bool isParsed = false;
            do
            {
                Console.Write("Type ID: ");
                input = Console.ReadLine();
                isParsed = int.TryParse(input, out inputVal);

            } while (isParsed == false);


            Models.Task task = TasksMockDatabase.Tasks.FirstOrDefault(t => t.TaskId == inputVal.ToString());


            if (task is null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No task has been found.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Thread.Sleep(2000);
                MenuHandler();
            }

            Console.Clear();
            TaskSubMenuHandler(task);

        }  
        private static void ShowTaskDetails(Models.Task task)
        {
            Console.WriteLine();
            Console.WriteLine($"ID:                   {task.TaskId}");
            Console.WriteLine($"Name:                 {task.TaskName}");
            Console.WriteLine($"Priority:             {task.TaskPriority}");
            Console.WriteLine($"Date of Creation:     {task.DateOfCreation}");
            Console.WriteLine($"Date of Effectuation: {task.DateOfEffectuation}");
            Console.WriteLine($"Is Assigned:          {task.IsAssigned}");
            Console.WriteLine($"Completed:            {task.State}");
            Console.WriteLine($"Description:          {task.Description}");
        }
        private static void AssignTaskToEmployee(Models.Task task)
        {
            Employee empl;
            string nickname = "";

            do
            {
                Console.Write("Select employee by indicating his nickname: ");
                nickname = Console.ReadLine();

                empl = EmployeesMockDatabase.Employees.FirstOrDefault(t => t.Nickname.ToLower() == nickname.ToLower());

                if (string.IsNullOrEmpty(nickname) || empl == null)
                {
                    Console.WriteLine("No employee has been found.");
                }

            } while (string.IsNullOrEmpty(nickname) || empl == null);

            
            TasksMockDatabase.Tasks.Find(t => t.TaskId == task.TaskId).IsAssigned = true;
            task.IsAssigned = true;
            EmployeesMockDatabase.AssignTaskToEmployee(task, nickname);


            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Assigned. Task Id: {task.TaskId}, Task name: {task.TaskName}");
            Console.WriteLine($"Assignee - ");
            Console.WriteLine($"    Nickname:    {empl.Nickname}");
            Console.WriteLine($"    Name:        {empl.Name}");
            Console.WriteLine($"    Surname:     {empl.Surname}");
            Console.WriteLine();


            Console.WriteLine("Returning to main menu...");
            Console.ForegroundColor = ConsoleColor.Gray;

            Thread.Sleep(3000);

            MenuHandler();        
        }


        //Not implemented
        #region [Task Update]
        private static void UpdateTask(Models.Task task)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Sorry this option is not available yet.");
            Console.WriteLine("Press any key to return.");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.ReadKey();

            TaskSubMenuHandler(task);
        }
        #endregion
    }
}
