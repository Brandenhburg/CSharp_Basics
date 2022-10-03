using CSharp_Basics_HW.Data;
using CSharp_Basics_HW.Models;
using System.IO.Pipes;

namespace CSharp_Basics_HW.Menus
{
    internal static class EmployeeMenu
    {
        #region [Menus]
        public static void Menu()
        {
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. Show Employees");
            Console.WriteLine("3. Choose Employee");
            Console.WriteLine("4. Modify Employee");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Type Here: ");
        }
        private static void EmployeeSubMenu(Employee employee)
        {
            Console.WriteLine();
            Console.Write("EMPLOYEE - ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{employee.Name}, {employee.Surname}");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine($"1. Show all tasks of the employee.");
            Console.WriteLine($"2. Assign a task to the employee.");
            Console.WriteLine($"3. Complete one of the current tasks.");
            Console.WriteLine($"4. Return.");
            Console.Write("Type Here: ");
        }
        private static void SelectEmployeeMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Select the employee and proceed to options.");
            Console.WriteLine("2. Back to main.");
            Console.WriteLine("Type Here: ");
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
                Menu();
                input = Console.ReadLine();
                isParsed = int.TryParse(input, out inputVal);

            } while (isParsed == false || inputVal < 1 || inputVal > 5);


            switch (inputVal)
            {
                case 1:
                    AddEmployeeMenu();
                    break;
                case 2:
                    EmployeesMockDatabase.GetAllEmployeesInfo();
                    MenuHandler();
                    break;
                case 3:
                    SelectEmployeeMenuHandler();
                    break;
                case 4:
                    ModifyEmployee();
                    break;
                case 5:
                    MainMenu.MenuHandler();
                    break;
                default:
                    break;
            }
        }
        public static void EmployeeSubMenuHandler(Employee employee)
        {
            //EmployeesMockDatabase.GetAllEmployeesInfo();

            int inputVal = -1;
            string input = "";
            bool isParsed = false;

            do
            {
                EmployeeSubMenu(employee);
                input = Console.ReadLine();
                isParsed = int.TryParse(input, out inputVal);

            } while (isParsed == false || inputVal < 1 || inputVal > 4);


            switch (inputVal)
            {
                case 1:
                    ShowEmployeeTasks(employee);
                    break;
                case 2:
                    AssignTaskToEmployee(employee);
                    break;
                case 3:
                    CompleteCurrentTasks(employee);
                    break;
                case 4:
                    Console.Clear();
                    MenuHandler();
                    break;
                default:
                    break;
            }
        }
        public static void SelectEmployeeMenuHandler()
        {
            EmployeesMockDatabase.GetAllEmployeesInfo();

            int inputVal = -1;
            string input = "";
            bool isParsed = false;

            do
            {
                SelectEmployeeMenu();

                input = Console.ReadLine();
                isParsed = int.TryParse(input, out inputVal);

            } while (isParsed == false || inputVal < 1 || inputVal > 2);

            Employee employee = null;

            switch (inputVal)
            {
                case (1):
                    employee = ChooseEmployee();
                    EmployeeSubMenuHandler(employee);
                    break;
                case (2):
                    MenuHandler();
                    break;
                default:
                    break;

            }
        }
        #endregion

        private static void AssignTaskToEmployee(Employee employee)
        {
            Console.Clear();

            if (TasksMockDatabase.Tasks.Count == 0)
            {
                Console.WriteLine("No tasks have been created.");
                EmployeeSubMenuHandler(employee);
            }

            List<Models.Task> unassignedTasks = TasksMockDatabase.GetAllUnassignedTasksDetails(employee);

            string input = "";
            int inputVal = -1;
            bool isParsed = false;
            do
            {
                Console.WriteLine($"Type the ID of the available tasks");
                Console.Write("Id: ");

                input = Console.ReadLine();

                isParsed = int.TryParse(input, out inputVal);

            } while (isParsed == false);

            Models.Task selectedTask = unassignedTasks.FirstOrDefault(t => t.TaskId == input);

            if (selectedTask is null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Wrong Id. Returning...");
                Console.ForegroundColor = ConsoleColor.Gray;
                Thread.Sleep(3000);

                EmployeeSubMenuHandler(employee);
            }

            selectedTask.IsAssigned = true;

            EmployeesMockDatabase.AssignTaskToEmployee(selectedTask, employee.Nickname);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Assigning the Task...");
            Thread.Sleep(2000);
            Console.WriteLine("Done. Returning to menu...");
            Console.ForegroundColor = ConsoleColor.Gray;
            Thread.Sleep(2000);

            Console.Clear();

            EmployeeSubMenuHandler(employee);
        }
        private static Employee ChooseEmployee()
        {
            EmployeesMockDatabase.GetAllEmployeesInfo();

            Console.WriteLine();
            Console.Write("Specify Nickname: ");

            string nickname = "";

            do
            {
                nickname = Console.ReadLine();

            } while (string.IsNullOrEmpty(nickname));

            Employee employee = EmployeesMockDatabase.Employees.FirstOrDefault(empl => empl.Nickname.ToLower() == nickname.ToLower()) ;

            if (employee is null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No employee has been found.");
                Console.WriteLine("Returning...");
                Console.ForegroundColor = ConsoleColor.Gray;
                Thread.Sleep(3000);
                Console.Clear();

                SelectEmployeeMenuHandler();
            }

            return employee;
        }
        private static void AddEmployeeMenu()
        {
            Employee employee = new Employee();

            Console.Clear();

            bool employeeExists = true;

            do
            {

                Console.Write("Nickname: ");
                employee.Nickname = Console.ReadLine();

                if (string.IsNullOrEmpty(employee.Nickname))
                {
                    Console.WriteLine("Nickname can not be empty.");
                }

                employeeExists = EmployeesMockDatabase.Employees.Exists(empl => empl.Nickname.ToLower() == employee.Nickname.ToLower());

                if (employeeExists)
                {
                    Console.WriteLine("This Nickname is unavailable.");
                }

            } while (employeeExists || string.IsNullOrEmpty(employee.Nickname));

            do
            {
                Console.Write("Name: ");
                employee.Name = Console.ReadLine();

                if (string.IsNullOrEmpty(employee.Name))
                {
                    Console.WriteLine("Name can not be empty.");
                }

            } while (string.IsNullOrEmpty(employee.Name));

            do
            {
                Console.Write("Surname: ");
                employee.Surname = Console.ReadLine();

                if (string.IsNullOrEmpty(employee.Surname))
                {
                    Console.WriteLine("Surname can not be empty.");
                }
            } while (string.IsNullOrEmpty(employee.Surname));


            EmployeesMockDatabase.Employees.Add(employee);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Saved. Returning...");
            Console.ForegroundColor = ConsoleColor.Gray;

            Thread.Sleep(3000);

            Console.Clear();

            MenuHandler();
        }
        private static void ShowEmployeeTasks(Employee employee)
        {

            if (employee.AllTasks is null)
            {
                Console.WriteLine($"{employee.Name} {employee.Surname} tasks: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("There are no tasks assigned to current employee.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Thread.Sleep(3000);
                EmployeeSubMenuHandler(employee);
            }


            Console.WriteLine("-----------------");
            Console.WriteLine($"{employee.Name} {employee.Surname} tasks: ");

            foreach (Models.Task task in employee.AllTasks)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine($"    [ID]:                   {task.TaskId}");
                Console.WriteLine($"    [Name]:                 {task.TaskName}");
                Console.WriteLine($"    [Priority]:             {task.TaskPriority}");
                Console.WriteLine($"    [Date of Creation]:     {task.DateOfCreation}");
                Console.WriteLine($"    [Date of Effectuation]: {task.DateOfEffectuation}");
                Console.WriteLine($"    [Is Assigned]:          {task.IsAssigned}");
                Console.WriteLine($"    [Completed]:            {task.State}");
                Console.WriteLine($"    [Description]:          {task.Description}");
                Console.WriteLine("-----------------");
            }

            EmployeeSubMenuHandler(employee);
        }
        private static Employee ShowAllEmployeesAndSelect()
        {
            EmployeesMockDatabase.GetAllEmployeesInfo();
            Employee employee;
            string nickName = "";
            do
            {
                Console.WriteLine();
                Console.Write("Enter Nickname: ");
                nickName = Console.ReadLine();
                employee = EmployeesMockDatabase.Employees.Find(empl => empl.Nickname == nickName);

                if (employee is null)
                {
                    Console.WriteLine("No Employee found.");
                }

            } while (string.IsNullOrEmpty(nickName) || employee == null);

            return employee;
        }
        private static void ModifyEmployee()
        {
            Employee selectedEmployee = ShowAllEmployeesAndSelect();

            Employee modifyiedEmployee = new Employee()
            {
                Nickname = selectedEmployee.Nickname,
                Name = selectedEmployee.Name,
                Surname = selectedEmployee.Surname,
                AllTasks = selectedEmployee.AllTasks
            };

            Console.WriteLine();
            Console.WriteLine($"EMPLOYEE - Name: {modifyiedEmployee.Name} /Surname:  {modifyiedEmployee.Surname} /Nickname: {modifyiedEmployee.Nickname}");
            Console.WriteLine(" ---Press enter to skip the step----");
            Console.Write($"Change the Name: ");

            string newName = Console.ReadLine();

            if (!string.IsNullOrEmpty(newName))
            {
                modifyiedEmployee.Name = newName;
                Console.WriteLine("Done!");
            }

            Console.WriteLine();
            Console.WriteLine($"EMPLOYEE - {modifyiedEmployee.Name} / {modifyiedEmployee.Surname} / {modifyiedEmployee.Nickname}");
            Console.WriteLine(" ---Press enter to skip the step----");
            Console.Write($"Change the surname: ");

            string newSurname = Console.ReadLine();

            if (!string.IsNullOrEmpty(newSurname))
            {
                modifyiedEmployee.Surname = newSurname;
                Console.WriteLine("Done!");
            }

            Console.WriteLine();
            Console.WriteLine($"EMPLOYEE - {modifyiedEmployee.Name} / {modifyiedEmployee.Surname} / {modifyiedEmployee.Nickname}");
            Console.WriteLine(" ---Press enter to skip the step----");
            Console.Write($"Change the Nickname: ");

            string newNickName = Console.ReadLine();

            if (!string.IsNullOrEmpty(newNickName) && EmployeesMockDatabase.Employees.Exists(empl => empl.Nickname.ToLower() == newNickName.ToLower()))
            {
                do
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("This nickname is unavailable.Try one more time.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"Change the nickname: ");
                    newNickName = Console.ReadLine();

                } while (EmployeesMockDatabase.Employees.Exists(empl => empl.Nickname.ToLower() == newNickName.ToLower()));
            }

            modifyiedEmployee.Nickname = newNickName;
            Console.WriteLine("Done!");

            EmployeesMockDatabase.Employees.Remove(selectedEmployee);
            EmployeesMockDatabase.Employees.Add(modifyiedEmployee);


            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Changes have been saved.");
            Console.WriteLine("Returning...");
            Console.ForegroundColor = ConsoleColor.Gray;
            Thread.Sleep(3000);
            Console.Clear();

            MenuHandler();
        }
        private static void CompleteCurrentTasks(Employee employee)
        {
            if (employee.AllTasks == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No task asigned to current employee");
                Console.ForegroundColor = ConsoleColor.Gray;

                EmployeeSubMenuHandler(employee);
            }



            GetEmployeeTaskDetails(employee);

            string input = "";
            int inputVal = -1;
            bool isParsed = false;

            Models.Task task;

            do
            {
                Console.Write("Enter Task Id: ");
                input = Console.ReadLine();
                isParsed = int.TryParse(input, out inputVal);

                task = employee.AllTasks.FirstOrDefault(task => task.TaskId == inputVal.ToString());

                if (task is null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Wrong Id. Returning...");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    Thread.Sleep(2000);
                    
                    EmployeeSubMenuHandler(employee);
                }

                if (task.State)
                {
                    Console.WriteLine("This task is already completed. Select another one");
                }

            } while (isParsed == false || task.State == true);

            Console.WriteLine("Completing the task....");

            employee.AllTasks.FirstOrDefault(task => task.TaskId == input.ToString()).State = true;

            EmployeesMockDatabase.Employees.
                FirstOrDefault(employee => employee.Nickname == employee.Nickname)
                .AllTasks.FirstOrDefault(task => task.TaskId == input.ToString()).State = true;

            Thread.Sleep(3000);
            Console.WriteLine($"Task Id: {task.TaskId}");
            Console.WriteLine($"Task Name: {task.TaskName}");
            Console.WriteLine($"Status:  Completed");
            Thread.Sleep(3000);
            Console.WriteLine();

            MenuHandler();
        }
        private static void GetEmployeeTaskDetails(Employee employee)
        {
            foreach (var task in employee.AllTasks)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine($"    [ID]:                   {task.TaskId}");
                Console.WriteLine($"    [Name]:                 {task.TaskName}");
                Console.WriteLine($"    [Priority]:             {task.TaskPriority}");
                Console.WriteLine($"    [Date of Creation]:     {task.DateOfCreation}");
                Console.WriteLine($"    [Date of Effectuation]: {task.DateOfEffectuation}");
                Console.WriteLine($"    [Is Assigned]:          {task.IsAssigned}");
                Console.WriteLine($"    [Completed]:            {task.State}");
                Console.WriteLine($"    [Description]:          {task.Description}");
                Console.WriteLine("-----------------");
            }
        }
    }
}
