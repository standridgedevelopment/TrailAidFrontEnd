﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front_End_Console_App.Models
{
    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? StateID { get; set; }
        public string StateName { get; set; }
    }
}
