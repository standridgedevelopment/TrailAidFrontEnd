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
            Console.WriteLine($"1. Park ID: {ID}" +
                $"\n2. Name: {Name}" +
                $"\n3. Rating: {AverageTrailRating}" +
                $"\n4. City ID : {CityID}" +
                $"\n5. City Name: {CityName}" +
                $"\n6. Acreage: {Acreage}" +
                $"\n7. Hours: {Hours}" +
                $"\n8. Phone Number: {PhoneNumber}" +
                $"\n9. Website: {Website}");
        }
        public void PrintPropsForEdit()
        {
            Console.WriteLine($"\n1. Name: {Name}" +
                $"\n2. City ID : {CityID}" +
                $"\n3. Acreage: {Acreage}" +
                $"\n4. Hours: {Hours}" +
                $"\n5. Phone Number: {PhoneNumber}" +
                $"\n6. Website: {Website}" +
                $"\n7. Return to Park Menu");
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
