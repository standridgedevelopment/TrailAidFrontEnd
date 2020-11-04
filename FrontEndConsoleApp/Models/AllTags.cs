using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front_End_Console_App.Models
{
    public class AllTags
    {
        public int ID { get; set; }
        public string ListOfAllTags { get; set; }
        public string AddTags { get; set; }
        public string DeleteTags { get; set; }
        public void PrintTags()
        {
            List<String> eachTag = ListOfAllTags.Split(' ').ToList();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Current Tags:");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var tag in eachTag)
            {
                Console.WriteLine(tag);
            }
        }
    }
}
