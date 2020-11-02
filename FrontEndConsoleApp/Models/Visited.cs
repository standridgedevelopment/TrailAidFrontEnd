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
    }
}
