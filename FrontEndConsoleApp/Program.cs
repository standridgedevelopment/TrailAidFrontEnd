using FrontEndConsoleApp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var programUI = new ProgramUI();
            programUI.Run();
            HttpClient httpClient = new HttpClient();
        }
    }
}
