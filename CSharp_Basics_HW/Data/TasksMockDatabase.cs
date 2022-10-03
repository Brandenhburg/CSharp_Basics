using CSharp_Basics_HW.Menus;
using CSharp_Basics_HW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Basics_HW.Data
{
    internal static class TasksMockDatabase
    {
        public static List<Models.Task> Tasks { get; set; } = new List<Models.Task>();

        public static void GetAllTasksDetails()
        {
            foreach (var task in Tasks)
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
        public static void GetTaskDetails(Models.Task task)
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
        public static void SeedDatabase()
        {
            ushort incrementor = 0;
            ++incrementor;

            Tasks.Add(new Models.Task
            {
                
                TaskId = incrementor.ToString(),
                TaskName = "Bug fixing",
                Description = "bug appears when button is pressed",
                IsAssigned = true,
                State = false,
                TaskPriority = Enums.TaskPriority.Medium,
                DateOfCreation = DateTime.Now.Date,
                DateOfEffectuation = DateTime.Now.Date
            });

            ++incrementor;
            Tasks.Add(new Models.Task
            {
                TaskId = incrementor.ToString(),
                TaskName = "Bug fixing",
                Description = "bug appears when button is pressed",
                IsAssigned = true,
                State = false,
                TaskPriority = Enums.TaskPriority.Medium,
                DateOfCreation = DateTime.Now.Date,
                DateOfEffectuation = DateTime.Now.Date
            });
        }

        public static List<Models.Task> GetAllUnassignedTasksDetails(Employee employee)
        {

            List<Models.Task> unassignedTasks = Tasks.Where(t => t.IsAssigned == false).ToList();

            if (unassignedTasks.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.WriteLine("Unassigned tasks not found");
                Console.WriteLine("Returning...");
                Console.ForegroundColor = ConsoleColor.Gray;

                Thread.Sleep(3000);

                EmployeeMenu.EmployeeSubMenuHandler(employee);

            }

            foreach (var task in unassignedTasks)
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

            return unassignedTasks;
        }
    }
}
