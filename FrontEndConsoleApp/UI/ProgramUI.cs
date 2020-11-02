using Front_End_Console_App.Models;
using FrontEndConsoleApp.Models;
using FrontEndConsoleApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FrontEndConsoleApp.UI
{
    public class ProgramUI
    {
        TrailAIDService trailAIDService = new TrailAIDService();
        public void Run()
        {
            var User = LoginMenu();
            MainMenu(User);
        }

        private User LoginMenu()
        {
            bool status;
            bool continueToRun = true;
            while (continueToRun)
            {
                Console.WriteLine("Weclome to Trail AID!");
                Console.WriteLine("\nSelect your option");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Create Account");

                string response = Console.ReadLine().ToLower();

                if (response == "1" || response == "login")
                {
                    status = Login();
                    if (status) continueToRun = false;
                }
                else if (response == "2" || response == "create account") CreateAccount();
                else
                {
                    Console.WriteLine("Please enter a valid option" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            Console.Clear();
            var user = trailAIDService.GetUser();
            return user.Result;
        }
        private void MainMenu(User User)
        {
            bool continueToRun = true;
            while (continueToRun)
            {
                Console.WriteLine("Main Menu");
                Console.WriteLine("\nWhat Would You Like To Do?");
                Console.WriteLine("1. Profile");
                Console.WriteLine("2. Cities");
                Console.WriteLine("3. Parks");
                Console.WriteLine("4. Trails");
                Console.WriteLine("6. Park Visits");
                Console.WriteLine("5. Edit List of Tags");

                string response = Console.ReadLine().ToLower();

                if (response == "1" || response == "edit profile")
                {
                    UserProfile(User);
                }
                else if (response == "2" || response == "cities") Cities();
                else if (response == "3" || response == "parks") Parks();
                else if (response == "4" || response == "trails") Trails();
                else
                {
                    Console.WriteLine("Please enter a valid option" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }

            }
        }
        //Accounts!
        private void CreateAccount()
        {
            bool continueToRun = true;
            while (continueToRun)
            {
                Console.Clear();
                var information = new RegisterBindingModel();
                Console.WriteLine("Please enter an email address");
                information.Email = Console.ReadLine();
                Console.Clear();

                Console.WriteLine("Please enter a password (must contain at least 6 characters, a capital letter, and a special character)");
                information.Password = Console.ReadLine();
                Console.Clear();

                Console.WriteLine("Please confirm the password");
                information.ConfirmPassword = Console.ReadLine();
                Console.Clear();

                bool result = trailAIDService.Register(information).Result;
                if (result)
                {
                    Console.WriteLine("Account Created!");
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    Console.Clear();
                    continueToRun = false;
                }
                else
                {
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        private bool Login()
        {
            bool continueToRun = true;
            while (continueToRun)
            {
                Console.Clear();
                var information = new TokenCreate();
                Console.WriteLine("Please enter your registered email address");
                information.username = Console.ReadLine();
                Console.Clear();

                Console.WriteLine("Please enter your password");
                information.password = Console.ReadLine();
                Console.Clear();

                try
                {
                    trailAIDService.token = trailAIDService.GetToken(information).Result;
                }
                catch { }

                if (trailAIDService.token == null)
                {
                    Console.WriteLine("Your login information was incorrect" +
                        "\nPress any key to try again...");
                    Console.ReadKey();
                    Console.Clear();
                }
                if (trailAIDService.token != null) continueToRun = false;

            }
            return true;
        }
        //User!
        private void UserProfile(User User)
        {
            Console.Clear();
            string response;
            bool continueToRun = true;
            while (continueToRun)
            {
                if (User == null)
                {
                    var newUser = CreateUser();

                    if (trailAIDService.PostUser(newUser) != null)
                    {
                        Console.WriteLine("Profile Created");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                Console.WriteLine("User Profile Menu");
                Console.WriteLine("\nWhat Would You Like To Do?" +
                        "\n1. View Profile" +
                        "\n2. Edit Profile" +
                        "\n3. View Favorite Trails" +
                        "\n4. Main Menu");
                response = Console.ReadLine().ToLower();
                Console.Clear();

                if (response == "1" || response == "view profile")
                {
                    Console.Clear();
                    User = trailAIDService.GetUser().Result;
                    Console.WriteLine($"First Name: {User.FirstName}" +
                        $"\nLast Name: {User.LastName}" +
                        $"\nState: {User.State}" +
                        $"\nCity: {User.City}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                if (response == "2" || response == "edit profile") UpdateProfile(User);
                else if (response == "3" || response == "view favorite trails") ViewFavoriteTrails(User);
                else if (response == "4" || response == "main menu") continueToRun = false;
                else
                {
                    Console.WriteLine("Please enter a valid option" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        private User CreateUser()
        {
            Console.Clear();
            var newUser = new User();
            Console.WriteLine("Let's set up your profile.");
            Console.WriteLine("Please enter your first name");
            newUser.FirstName = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Please enter your last name.");
            newUser.LastName = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Please enter your state.");
            newUser.State = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Please enter your city.");
            newUser.City = Console.ReadLine();
            Console.Clear();
            return newUser;
        }
        private void UpdateProfile(User User)
        {
            bool edits = true;
            while (edits)
            {
                Console.WriteLine($"What would you like to edit?" +
                $"\n1. First Name: {User.FirstName}" +
                $"\n2. Last Name : {User.LastName}" +
                $"\n3. City : {User.City}" +
                $"\n4. State: {User.State}" +
                $"\n5. Main Menu");
                string editProfile = Console.ReadLine().ToLower();
                if (editProfile == "1" || editProfile == "first name")
                {
                    Console.WriteLine("Please enter your first name");
                    User.FirstName = Console.ReadLine();
                    Console.Clear();
                }
                if (editProfile == "2" || editProfile == "last name")
                {
                    Console.WriteLine("Please enter your last name");
                    User.LastName = Console.ReadLine();
                    Console.Clear();
                }
                if (editProfile == "3" || editProfile == "city")
                {
                    Console.WriteLine("Please enter your city");
                    User.City = Console.ReadLine();
                    Console.Clear();
                }
                if (editProfile == "4" || editProfile == "state")
                {
                    Console.WriteLine("Please enter your state");
                    User.State = Console.ReadLine();
                    Console.Clear();
                }
                if (editProfile == "5" || editProfile == "main menu")
                {
                    Console.Clear();
                    if (trailAIDService.EditUserAsync(User).Result)
                        edits = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid option" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        private void ViewFavoriteTrails(User User)
        {
            foreach (var trail in User.Favorites)
            {
                Console.WriteLine($"Trail Name: {trail.TrailName}" +
                    $"\nTrail ID: {trail.TrailID}\n");
            }
        }

        //Cities!
        private void Cities()
        {
            bool editCities = true;
            while (editCities)
            {
                Console.Clear();
                Console.WriteLine("What would you like to do?" +
                    "\n1. Add a city" +
                    "\n2. View all cities" +
                    "\n3. Edit City" +
                    "\n4. Main Menu");
                string response = Console.ReadLine().ToLower();
                if (response == "1" || response == "add a city") AddCity();
                else if (response == "2" || response == "view all cities") GetCities();
                else if (response == "3" || response == "edit a city") EditCity();
                else if (response == "4" || response == "main menu") editCities = false;
                else
                {
                    Console.WriteLine("Please enter a valid option" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

        }
        private void AddCity()
        {
            City newCity = new City();
            Console.Clear();
            Console.WriteLine("What is the name of the city?");
            newCity.Name = Console.ReadLine();
            string result = trailAIDService.PostGeneric<City>(newCity, "City").Result;
            if (result == "true")
            {
                Console.WriteLine("City added successfully");
                Console.WriteLine($"City ID is {newCity.ID}");
            }
            else
            {
                Console.WriteLine("Could not add city");
            }
        }
        private void EditCity()
        {
            Console.Clear();
            bool continueToRun = true;
            while (continueToRun)
            {
                City editCity = new City();
                Console.Clear();
                Console.WriteLine("Enter the ID of the city to edit");
                int cityID = 0;
                try
                {
                    cityID = int.Parse(Console.ReadLine());
                    Console.Clear();
                }
                catch
                {
                    Console.WriteLine("Please enter a number" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                var foundCity = trailAIDService.GetGenericByID<City>(cityID, "City").Result;
                if (foundCity != null)
                {
                    Console.Clear();
                    Console.WriteLine($"This ID matches the city of {foundCity.Name}" +
                        $"\nWhat would you like to do?" +
                        $"\n1. Rename City" +
                        $"\n2. Search Again" +
                        $"\n3. City Menu");
                    string response = Console.ReadLine().ToLower();
                    if (response == "1" || response == "rename city")
                    {
                        string originalName = foundCity.Name;
                        Console.Clear();
                        Console.WriteLine($"What would you like to rename {foundCity.Name} to?");
                        foundCity.Name = Console.ReadLine();
                        string edit = trailAIDService.EditGeneric<City>(foundCity, cityID, "City").Result;
                        if (edit == "true")
                        {
                            Console.WriteLine($"{originalName} is now {foundCity.Name}" +
                                $"\nPress any key to return to city menu");
                            Console.ReadKey();
                            Console.Clear();
                            continueToRun = false;
                        }
                    }
                    if (response == "2" || response == "search again") continue;
                    if (response == "3" || response == "city menu") continueToRun = false;
                }
            }
        }
        private void GetCities()
        {
            Console.Clear();
            List<City> allCities = trailAIDService.GetGeneric<List<City>>("City").Result;
            foreach (var city in allCities)
            {
                Console.WriteLine($"City Name: {city.Name}" +
                    $"\nCity ID {city.ID}\n");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        //Parks
        private void Parks()
        {
            bool editParks = true;
            while (editParks)
            {
                Console.Clear();
                Console.WriteLine("What would you like to do?" +
                    "\n1. Add a Park" +
                    "\n2. Search for Parks" +
                    "\n3. Main Menu");
                string response = Console.ReadLine().ToLower();
                if (response == "1" || response == "add a park") AddPark();
                else if (response == "2" || response == "search for parks") SearchParks();
                else if (response == "3" || response == "main menu") editParks = false;
                else
                {
                    Console.WriteLine("Please enter a valid option" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        private void AddPark()
        {
            Park newPark = new Park();
            Console.Clear();
            bool cityID = true;
            bool acreage = true;
            while (cityID)
            {
                Console.WriteLine("What is the City ID?");
                try
                {
                    newPark.CityID = int.Parse(Console.ReadLine());
                    cityID = false;
                    Console.Clear();
                }
                catch
                {
                    Console.WriteLine("Please enter a valid ID" +
                        "\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            Console.WriteLine("What is the name of the Park?");
            newPark.Name = Console.ReadLine();
            Console.Clear();

            while (acreage)
            {
                Console.WriteLine("How many acres is this park?");
                try
                {
                    newPark.Acreage = int.Parse(Console.ReadLine());
                    acreage = false;
                    Console.Clear();
                }
                catch
                {
                    Console.WriteLine("Please enter a valid number" +
                        "\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Console.WriteLine("What are the hours of the park?");
            newPark.Hours = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("What is the phone number of the park?");
            newPark.PhoneNumber = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("What is the website of the park?");
            newPark.Website = Console.ReadLine();
            Console.Clear();

            string result = trailAIDService.PostGeneric<Park>(newPark, "Park").Result;
            if (result == "true")
            {
                Console.WriteLine("Park added successfuly");
                Console.WriteLine($"Park ID is {newPark.ID}" +
                    "\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Could not add park");
            }
        }
        private void SearchParks()
        {
            Console.Clear();
            bool continueToRun = true;
            while (continueToRun)
            {
                Console.Clear();
                Console.WriteLine("How would you like to search for the park?" +
                    "\n1. ID" +
                    "\n2. Name" +
                    "\n3. City" +
                    "\n4. Return To Park Menu");
                string response = Console.ReadLine().ToLower();
                if (response == "1" || response == "id") ParkByID();
                else if (response == "2" || response == "name") ParkByName();
                else if (response == "3" || response == "city") ParkByCity();
                else if (response == "4" || response == "return to park menu") continueToRun = false;
                else
                {
                    Console.WriteLine("Please enter a valid option" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        private void ParkByID()
        {
            Console.Clear();
            bool continueToRun = true;
            bool parkMenu = true;
            int parkID = 0;
            while (continueToRun)
            {
                Console.WriteLine("Enter the ID of the park");
                try
                {
                    parkID = int.Parse(Console.ReadLine());
                    Console.Clear();
                    continueToRun = false;
                }
                catch
                {
                    Console.WriteLine("Please enter a number" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
            }
            var foundPark = trailAIDService.GetGenericByID<Park>(parkID, "Park").Result;
            if (foundPark != null)
            {
                while (parkMenu)
                {
                    Console.WriteLine($"This ID matches the park of {foundPark.Name}" +
                    $"\nWhat would you like to do?" +
                    $"\n1. View Park Details" +
                    $"\n2. Edit the Park" +
                    $"\n3. Search Again" +
                    $"\n4. City Menu");
                    string response = Console.ReadLine().ToLower();
                    if (response == "1" || response == "view park details")
                    {
                        Console.Clear();
                        foundPark.PrintProps();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();

                    }
                    else if (response == "2" || response == "edit the park")
                    {
                        EditPark(foundPark, parkID);
                    }
                    else if (response == "3" || response == "search again") continue;
                    else if (response == "4" || response == "city menu")
                    {
                        parkMenu = false;
                        continueToRun = false;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid option" +
                            "\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }

            }
            else
            {
                Console.WriteLine("Park not found" +
                    "\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        private void ParkByName()
        {
            Console.Clear();
            Console.WriteLine("What is the name of the park?");
            string searchPark = Console.ReadLine();
            Console.Clear();
            var parkResults = trailAIDService.GetGenericByName<List<Park>>(searchPark, "Park").Result;
            foreach (var park in parkResults)
            {
                Console.WriteLine($"Park Name: {park.Name}" +
                    $"\nPark ID: {park.ID}\n");
            }
            Console.WriteLine("Would you like to see more details about a specific park? (y/n)");
            string response = Console.ReadLine();
            if (response == "y")
            {
                ParkByID();
            }
            Console.Clear();

        }
        private void ParkByCity()
        {
            Console.WriteLine("What City?");
            string searchPark = Console.ReadLine();
            var parkResults = trailAIDService.GetGenericByCity<List<Park>>(searchPark, "Park").Result;
            foreach (var park in parkResults)
            {
                Console.WriteLine($"Park Name: {park.Name}" +
                    $"\nPark ID: {park.ID}\n");
            }
            Console.WriteLine("Would you like to see more details about a specific park? (y/n)");
            string response = Console.ReadLine();
            if (response == "y")
            {
                ParkByID();
            }

        }
        private void EditPark(Park Park, int parkID)
        {
            bool editPark = true;
            bool success = false;
            var originalID = Park.CityID;
            while (success == false)
            {
                editPark = true;
                while (editPark)
                {
                    Console.Clear();
                    Console.WriteLine("What would you like to edit?");
                    Park.PrintPropsForEdit();
                    Console.WriteLine("\nEnter the number of your seleciton");
                    string choice = Console.ReadLine().ToLower();

                    if (choice == "1" || choice == "name")
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to rename {Park.Name} to?");
                        Park.Name = Console.ReadLine();
                    }
                    else if (choice == "2" || choice == "city id")
                    {
                        Console.Clear();
                        bool cityID = true;
                        while (cityID)
                        {
                            Console.WriteLine("What would you like to change the City ID to?");
                            try
                            {
                                Park.CityID = int.Parse(Console.ReadLine());
                                cityID = false;
                                Console.Clear();
                            }
                            catch
                            {
                                Console.WriteLine("Please enter a valid ID" +
                                    "\nPress any key to continue");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                    }
                    else if (choice == "3" || choice == "acreage")
                    {
                        Console.Clear();
                        bool acreage = true;
                        while (acreage)
                        {
                            Console.WriteLine("What would you like to change the acreage to?");
                            try
                            {
                                Park.Acreage = int.Parse(Console.ReadLine());
                                acreage = false;
                                Console.Clear();
                            }
                            catch
                            {
                                Console.WriteLine("Please enter a valid number" +
                                    "\nPress any key to continue");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                    }
                    else if (choice == "4" || choice == "hours")
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to change the Hours to?");
                        Park.Hours = Console.ReadLine();
                    }
                    else if (choice == "5" || choice == "phone number")
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to change the Phone Number to?");
                        Park.PhoneNumber = Console.ReadLine();
                    }
                    else if (choice == "6" || choice == "website")
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to change the Website to?");
                        Park.Website = Console.ReadLine();
                    }
                    else if (choice == "7" || choice == "save changes") editPark = false;
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a valid option" +
                            "\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                Console.Clear();
                string edit = trailAIDService.EditGeneric<Park>(Park, parkID, "Park").Result;
                if (edit == "true")
                {
                    Console.Clear();
                    Console.WriteLine($"Changes Sucessful" +
                        $"\nPress any key to return to city menu");
                    Console.ReadKey();
                    Console.Clear();
                    success = true;
                }
                else if (edit == "invalid ID")
                {
                    Park.CityID = originalID;
                    Console.WriteLine($"Press any key to return to try again");

                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        //Trails
        private void Trails()
        {
            bool editTrails = true;
            while (editTrails)
            {
                Console.Clear();
                Console.WriteLine("What would you like to do?" +
                    "\n1. Add a Trail" +
                    "\n2. Search for Trails" +
                    "\n3. Main Menu");
                string response = Console.ReadLine().ToLower();
                if (response == "1" || response == "add a trail") AddTrail();
                else if (response == "2" || response == "search for trails") SearchTrails();
                else if (response == "3" || response == "main menu") editTrails = false;
                else
                {
                    Console.WriteLine("Please enter a valid option" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        private void AddTrail()
        {
            Trail newTrail = new Trail();
            Console.Clear();
            bool cityID = true;
            bool distance = true;
            bool elevation = true;
            while (cityID)
            {
                Console.WriteLine("What is the City ID?");
                try
                {
                    newTrail.CityID = int.Parse(Console.ReadLine());
                    cityID = false;
                    Console.Clear();
                }
                catch
                {
                    Console.WriteLine("Please enter a valid ID" +
                        "\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
                Console.WriteLine("Is this trail in a Park? (y/n)");
                string inaPark = Console.ReadLine();
                if (inaPark == "y")
                {
                    Console.WriteLine("What is the Park ID?");
                    try
                    {
                        newTrail.ParkID = int.Parse(Console.ReadLine());
                        cityID = false;
                        Console.Clear();
                    }
                    catch
                    {
                        Console.WriteLine("Please enter a valid ID" +
                            "\nPress any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
            Console.WriteLine("What is the name of the Trail?");
            newTrail.Name = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("What is the difficulty?");
            newTrail.Difficulty = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Write a brief description?");
            newTrail.Description = Console.ReadLine();
            Console.Clear();

            while (distance)
            {
                Console.WriteLine("What is the distance in miles?");
                try
                {
                    newTrail.Distance = int.Parse(Console.ReadLine());
                    distance = false;
                    Console.Clear();
                }
                catch
                {
                    Console.WriteLine("Please enter a valid number" +
                        "\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Console.WriteLine("What is the terrain type");
            newTrail.TypeOfTerrain = Console.ReadLine();
            Console.Clear();

            while (elevation)
            {
                Console.WriteLine("What is the distance in miles?");
                try
                {
                    newTrail.Elevation = int.Parse(Console.ReadLine());
                    elevation = false;
                    Console.Clear();
                }
                catch
                {
                    Console.WriteLine("Please enter a valid number" +
                        "\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Console.WriteLine("What is route type?");
            newTrail.RouteType = Console.ReadLine();
            Console.Clear();

            string result = trailAIDService.PostGeneric<Trail>(newTrail, "Trail").Result;
            if (result == "true")
            {
                Console.WriteLine("Park added successfuly");
                Console.WriteLine($"Park ID is {newTrail.ID}" +
                    "\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        private void SearchTrails()
        {
            Console.Clear();
            bool continueToRun = true;
            while (continueToRun)
            {
                Console.Clear();
                Console.WriteLine("How would you like to search for the Trail?" +
                    "\n1. ID" +
                    "\n2. Name" +
                    "\n3. City" +
                    "\n4. Park" +
                    "\n5. Return To Park Menu");
                string response = Console.ReadLine().ToLower();
                if (response == "1" || response == "id") TrailByID();
                else if (response == "2" || response == "name") TrailByName();
                else if (response == "3" || response == "city") TrailByCity();
                else if (response == "4" || response == "park") TrailByPark();
                else if (response == "5" || response == "return to park menu") continueToRun = false;
                else
                {
                    Console.WriteLine("Please enter a valid option" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        private void TrailByID()
        {
            Console.Clear();
            bool continueToRun = true;
            bool trailMenu = true;
            int trailID = 0;
            while (continueToRun)
            {
                Console.WriteLine("Enter the ID of the trail");
                try
                {
                    trailID = int.Parse(Console.ReadLine());
                    Console.Clear();
                    continueToRun = false;
                }
                catch
                {
                    Console.WriteLine("Please enter a number" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
            }
            var foundTrail = trailAIDService.GetGenericByID<Trail>(trailID, "Trail").Result;
            if (foundTrail != null)
            {
                while (trailMenu)
                {
                    Console.WriteLine($"This ID matches the trail of {foundTrail.Name}" +
                    $"\nWhat would you like to do?" +
                    $"\n1. View Trail Details" +
                    $"\n2. Edit Trail" +
                    $"\n3. Search Again" +
                    $"\n4. Trail Menu");
                    string response = Console.ReadLine().ToLower();
                    if (response == "1" || response == "view Trail details")
                    {
                        Console.Clear();
                        foundTrail.PrintProps();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();

                    }
                    else if (response == "2" || response == "edit trail")
                    {
                        EditTrail(foundTrail, trailID);
                    }
                    else if (response == "3" || response == "search again") continue;
                    else if (response == "4" || response == "city menu")
                    {
                        trailMenu = false;
                        continueToRun = false;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid option" +
                            "\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }

            }
            else
            {
                Console.WriteLine("Trail not found" +
                    "\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        private void TrailByName()
        {
            Console.Clear();
            Console.WriteLine("What is the name of the trail?");
            string searchTrail = Console.ReadLine();
            Console.Clear();
            var trailResults = trailAIDService.GetGenericByName<List<Trail>>(searchTrail, "Trail").Result;
            foreach (var trail in trailResults)
            {
                Console.WriteLine($"Trail Name: {trail.Name}" +
                    $"\nPark ID: {trail.ID}\n");
            }
            Console.WriteLine("Would you like to see more details about a specific trail? (y/n)");
            string response = Console.ReadLine();
            if (response == "y")
            {
                TrailByID();
            }
            Console.Clear();

        }
        private void TrailByCity()
        {
            Console.WriteLine("What City?");
            string searchTrail = Console.ReadLine();
            var trailResults = trailAIDService.GetGenericByCity<List<Trail>>(searchTrail, "Trail").Result;
            foreach (var trail in trailResults)
            {
                Console.WriteLine($"Trail Name: {trail.Name}" +
                    $"\nTrail ID: {trail.ID}\n");
            }
            Console.WriteLine("Would you like to see more details about a specific trail? (y/n)");
            string response = Console.ReadLine();
            if (response == "y")
            {
                ParkByID();
            }

        }
        private void TrailByPark()
        {
            Console.WriteLine("What Park?");
            string searchTrail = Console.ReadLine();
            var trailResults = trailAIDService.GetByParkName<List<Trail>>(searchTrail, "Trail").Result;
            foreach (var trail in trailResults)
            {
                Console.WriteLine($"Trail Name: {trail.Name}" +
                    $"\nTrail ID: {trail.ID}\n");
            }
            Console.WriteLine("Would you like to see more details about a specific trail? (y/n)");
            string response = Console.ReadLine();
            if (response == "y")
            {
                ParkByID();
            }

        }
        private void EditTrail(Trail Trail, int trailID)
        {
            bool editTrail = true;
            bool success = false;
            var originalCityID = Trail.CityID;
            var originalParkID = Trail.ParkID;
            while (success == false)
            {
                editTrail = true;
                while (editTrail)
                {
                    Console.Clear();
                    Console.WriteLine("What would you like to edit?");
                    // Console.WriteLine($"1. Name: {Name}" +
                    //$"\n2. City ID : {CityID}" +
                    //$"\n3. ParkID: {ParkID}" +
                    //$"\n4. Difficulty: {Difficulty}" +
                    //$"\n5. Description: {Description}" +
                    //$"\n6. Distance: {Distance}" +
                    //$"\n7. Type of Terrain: {TypeOfTerrain}" +
                    //$"\n8. Tags: {Tags}" +
                    //$"\n9. Elevation: {Elevation}" +
                    //$"\n10. RouteType: {RouteType}" +
                    //$"\n11. Save Changes");

                    Trail.PrintPropsForEdit();
                    Console.WriteLine("\nEnter the number of your seleciton");
                    string choice = Console.ReadLine().ToLower();

                    if (choice == "1" || choice == "name")
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to rename {Trail.Name} to?");
                        Trail.Name = Console.ReadLine();
                    }
                    else if (choice == "2" || choice == "city id")
                    {
                        Console.Clear();
                        bool cityID = true;
                        while (cityID)
                        {
                            Console.WriteLine("What would you like to change the City ID to?");
                            try
                            {
                                Trail.CityID = int.Parse(Console.ReadLine());
                                cityID = false;
                                Console.Clear();
                            }
                            catch
                            {
                                Console.WriteLine("Please enter a valid ID" +
                                    "\nPress any key to continue");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                    }
                    else if (choice == "3" || choice == "park id")
                    {
                        Console.Clear();
                        bool cityID = true;
                        while (cityID)
                        {
                            Console.WriteLine("What would you like to change the Park ID to?");
                            try
                            {
                                Trail.ParkID = int.Parse(Console.ReadLine());
                                cityID = false;
                                Console.Clear();
                            }
                            catch
                            {
                                Console.WriteLine("Please enter a valid ID" +
                                    "\nPress any key to continue");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                    }

                    else if (choice == "4" || choice == "difficulty")
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to change the Difficulty to?");
                        Trail.Difficulty = Console.ReadLine();
                    }

                    else if (choice == "5" || choice == "description")
                    {
                        Console.Clear();
                        Console.WriteLine($"Write a new description");
                        Trail.Description = Console.ReadLine();
                    }

                    else if (choice == "6" || choice == "distance")
                    {
                        Console.Clear();
                        bool distance = true;
                        while (distance)
                        {
                            Console.WriteLine("What would you like to change the distance to?");
                            try
                            {
                                Trail.Distance = int.Parse(Console.ReadLine());
                                distance = false;
                                Console.Clear();
                            }
                            catch
                            {
                                Console.WriteLine("Please enter a valid ID" +
                                    "\nPress any key to continue");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                    }

                    else if (choice == "7" || choice == "type of terrain")
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to change the Terrain to?");
                        Trail.TypeOfTerrain = Console.ReadLine();
                    }

                    else if (choice == "8" || choice == "tags")
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to change the Terrain to?");
                        Trail.TypeOfTerrain = Console.ReadLine();
                    }
                    else if (choice == "9" || choice == "Elevation")
                    {
                        Console.Clear();
                        bool distance = true;
                        while (distance)
                        {
                            Console.WriteLine("What would you like to change the Elevation to?");
                            try
                            {
                                Trail.Elevation = int.Parse(Console.ReadLine());
                                distance = false;
                                Console.Clear();
                            }
                            catch
                            {
                                Console.WriteLine("Please enter a valid ID" +
                                    "\nPress any key to continue");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                    }

                    else if (choice == "10" || choice == "route type")
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to change the Route Type to?");
                        Trail.RouteType = Console.ReadLine();
                    }
                    else if (choice == "11" || choice == "save changes") editTrail = false;
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a valid option" +
                            "\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
            Console.Clear();
            string edit = trailAIDService.EditGeneric<Trail>(Trail, trailID, "Trail").Result;
            if (edit == "true")
            {
                Console.Clear();
                Console.WriteLine($"Changes Sucessful" +
                    $"\nPress any key to return to trail menu");
                Console.ReadKey();
                Console.Clear();
                success = true;
            }
            else if (edit == "ID")
            {
                Trail.CityID = originalCityID;
                Console.WriteLine($"Press any key to return to try again");

                Console.ReadKey();
                Console.Clear();
            }
        }
        //Tags
    }
}
