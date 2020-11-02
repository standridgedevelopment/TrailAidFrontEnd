using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front_End_Console_App.Models
{
    public class TrailEdit
    {
        public string Name { get; set; }
        public int? CityID { get; set; }
        public int? ParkID { get; set; }
        public string ParkName { get; set; }
        public string Difficulty { get; set; }
        public string Description { get; set; }
        public int Distance { get; set; }
        public string TypeOfTerrain { get; set; }
        public int Elevation { get; set; }
        public string RouteType { get; set; }
        public string AddTags { get; set; }
        public string DeleteTags { get; set; }

    }
}
