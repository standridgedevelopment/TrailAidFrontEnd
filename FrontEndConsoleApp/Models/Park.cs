using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front_End_Console_App.Models
{
    public class Park
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int CityID { get; set; }
        public virtual City City { get; set; }
        public string CityName { get; set; }
        public virtual List<Trail> Trails { get; set; } = new List<Trail>();
        public int Acreage { get; set; }
        public string Hours { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public double AverageTrailRating { get; set; }
        public List<Trail> TrailsInPark { get; set; } = new List<Trail>();

        public void PrintProps()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ID: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{ID}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Name: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Name}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Rating: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{AverageTrailRating}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("City ID: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{CityID}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("City Name: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{CityName}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Acreage: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Acreage}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Hours: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Hours}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Phone Number: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{PhoneNumber}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Website: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Website}");
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
            Console.Write("3. Acreage: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Acreage}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("4. Hours: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Hours}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("5. Phone Number: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{PhoneNumber}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("6. Website: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Website}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("7. Save Changes: ");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void PrintTrailsInPark()
        {
            foreach (var trail in TrailsInPark)
            {
                Console.WriteLine($"Trail Name: {trail.ID}" +
                    $"\nTrail ID {trail.ID}" +
                    $"\nTrail Rating {trail.Rating}");
            }
        }
    
    }
}
