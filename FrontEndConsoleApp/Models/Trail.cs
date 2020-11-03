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
            Console.WriteLine($"1. Trail ID: {ID}" +
                $"\n2. Name: {Name}" +
                $"\n3. City ID : {CityID}" +
                $"\n4. City Name: {CityName}" +
                $"\n5. ParkID: {ParkID}" +
                $"\n6. ParkName: {ParkName}" +
                $"\n7. Rating: {Rating}" +
                $"\n8. Difficulty: {Difficulty}" +
                $"\n9. Description: {Description}" +
                $"\n8. Distance: {Distance}" +
                $"\n8. Type of Terrain: {TypeOfTerrain}" +
                $"\n8. Tags: {Tags}" +
                $"\n8. Elevation: {Elevation}" +
                $"\n8. RouteType: {RouteType}");
        }
        public void PrintPropsForEdit()
        {
            Console.WriteLine($"1. Name: {Name}" +
                $"\n2. City ID : {CityID}" +
                $"\n3. ParkID: {ParkID}" +
                $"\n4. Difficulty: {Difficulty}" +
                $"\n5. Description: {Description}" +
                $"\n6. Distance: {Distance}" +
                $"\n7. Type of Terrain: {TypeOfTerrain}" +
                $"\n8. Tags: {Tags}" +
                $"\n9. Elevation: {Elevation}" +
                $"\n10. RouteType: {RouteType}" +
                $"\n11. Save Changes");
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
