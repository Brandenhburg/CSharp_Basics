using CSharp_Basics_HW.Menus;
using CSharp_Basics_HW.Models;
using System.Security.Cryptography;

namespace CSharp_Basics_HW.Data
{
    internal static class EmployeesMockDatabase
    {

        public static List<Employee> Employees { get; set; } = new List<Employee>();

        public static void GetAllEmployeesInfo()
        {
            Console.WriteLine();
            Console.WriteLine($"Employees count: {Employees.Count}");

            foreach (Employee employee in Employees)
            {
                Console.WriteLine("---------------");
                Console.WriteLine($"NickName:   {employee.Nickname}");
                Console.WriteLine($"Name:       {employee.Name}");
                Console.WriteLine($"Surname:    {employee.Surname}");
                Console.WriteLine("---------------");
            }

            Console.WriteLine();
        }
        public static void AssignTaskToEmployee(Models.Task task, string nickname)
        {
            Models.Task[] oldEmployeeTaskArray = Employees.FirstOrDefault(empl => empl.Nickname.ToLower() == nickname.ToLower()).AllTasks;

            if (oldEmployeeTaskArray is null)
            {
                oldEmployeeTaskArray = new Models.Task[1] { task };
                Employees.FirstOrDefault(empl => empl.Nickname.ToLower() == nickname.ToLower()).AllTasks = oldEmployeeTaskArray;
            }
            else
            {
                Models.Task[] newEmployeeTaskArray = new Models.Task[oldEmployeeTaskArray.Length + 1];

                for (int j = 0; j < oldEmployeeTaskArray.Length; j++)
                    newEmployeeTaskArray[j] = oldEmployeeTaskArray[j];

                newEmployeeTaskArray[newEmployeeTaskArray.Length - 1] = task;

                Employees.FirstOrDefault(empl => empl.Nickname.ToLower() == nickname.ToLower()).AllTasks = newEmployeeTaskArray;
            }  
        }

        public static void SeedDatabase()
        {
            Employees.Add(new Employee
            {
                Name = "David",
                Surname = "Jones",
                Nickname = "dav",
                AllTasks = new Models.Task[1]

            });

            Employees.Add(new Employee
            {
                Name = "Eydis",
                Surname = "Evensens",
                Nickname = "noSense",
                AllTasks = new Models.Task[1]
            });

            Employee dav = Employees.Find(e => e.Nickname == "dav");
            TasksMockDatabase.Tasks.Find(t => t.TaskId == "1").IsAssigned = true;
            dav.AllTasks[0] = TasksMockDatabase.Tasks.Find(t => t.TaskId == "1");

            Employee noSense = Employees.Find(e => e.Nickname == "noSense");
            TasksMockDatabase.Tasks.Find(t => t.TaskId == "2").IsAssigned = true;
            noSense.AllTasks[0] = TasksMockDatabase.Tasks.Find(t => t.TaskId == "2");
        }
    }
}
