using FrontEndConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front_End_Console_App.Models
{
    public class Trail
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int? CityID { get; set; }
        public virtual City City { get; set; }
        public string CityName { get; set; }
        public int? ParkID { get; set; }
        public virtual Park Park { get; set; }
        public string ParkName { get; set; }
        public double Rating { get; set; }
        public string Difficulty { get; set; }
        public string Description { get; set; }
        public int Distance { get; set; }
        public string TypeOfTerrain { get; set; }
        public string Tags { get; set; }
        public int Elevation { get; set; }
        public string RouteType { get; set; }
        public string AddTags { get; set; }
        public string DeleteTags { get; set; }
        public virtual List<Visited> Ratings { get; set; } = new List<Visited>();

        public void PrintProps()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Trail ID: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{ID}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Name: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Name}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("City ID: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{CityID}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("City Name: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{CityName}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Park ID: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{ParkID}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Park Name: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{ParkName}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Rating: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Rating}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Difficulty: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Difficulty}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Description: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Description}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Distance: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Distance} miles");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Terrain: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{TypeOfTerrain}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Tags: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Tags}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Elevation: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Elevation} ft");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Route Type: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{RouteType}");
        }
        public void PrintPropsForEdit()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("1. Name: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Name}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("2. City ID: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{CityID}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("3. Park ID: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{ParkID}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("4. Difficulty: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Difficulty}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("5. Description: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Description}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("6. Distance: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Distance} miles");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("7. Terrain: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{TypeOfTerrain}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("8. Tags: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Tags}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("9. Elevation: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Elevation}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("10. Route Type: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{RouteType}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("11. Save Changes: ");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void PrintTags()
        {
            List<String> eachTag = Tags.Split(' ').ToList();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Current Tags:");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var tag in eachTag)
            {
                if (tag != "") Console.WriteLine(tag);
            }
        }
    }
}
