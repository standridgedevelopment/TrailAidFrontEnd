using Front_End_Console_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndConsoleApp.Models
{
    public class Visited
    {
        public int ID { get; set; }
        public Guid? UserID { get; set; }
        public User User { get; set; }
        public int? TrailID { get; set; }
        public virtual Trail Trail { get; set; }
        public string TrailName { get; set; }
        public bool AddToFavorites { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }

        public void PrintProps()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Trail ID: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{ID}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Trail Name: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{TrailName}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Favorited: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{AddToFavorites}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Rating: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Rating}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Review: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Review}");
        }
        public void PrintPropsForEdit()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("1. Trail ID: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{ID}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("2. Favorited: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{AddToFavorites}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("3. Rating: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Rating}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("4. Review: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Review}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("5. Save Changes: ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
