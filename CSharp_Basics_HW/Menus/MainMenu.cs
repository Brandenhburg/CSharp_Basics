using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Basics_HW.Menus
{
    internal static class MainMenu
    {
        public static void Menu()
        {
            Console.WriteLine("1. Employee Menu.");
            Console.WriteLine("2. Task Menu.");
            Console.WriteLine("3. Exit.");
            Console.Write("Type Here: ");
        }
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
                    {
                        Console.Clear();
                        EmployeeMenu.MenuHandler();
                    }
                    break;
                case 2:
                    {
                        Console.Clear();
                        TaskMenu.MenuHandler();
                    }
                    break;
                case 3:
                    {
                        Console.WriteLine("Application Exit...");
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
