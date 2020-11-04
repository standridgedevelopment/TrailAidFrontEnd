using Front_End_Console_App.Models;
using FrontEndConsoleApp.Models;
using FrontEndConsoleApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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

                if (response.Contains("1") || response.Contains("login"))
                {
                    status = Login();
                    if (status) continueToRun = false;
                }
                else if (response.Contains("2") || response.Contains("create")) CreateAccount();
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
                Console.WriteLine("5. Trail Visits");
                Console.WriteLine("6. Edit List of Tags");

                string response = Console.ReadLine().ToLower();

                if (response.Contains("1") || response.Contains("profile")) UserProfile(User);
                else if (response.Contains("2") || response.Contains("cities")) Cities();
                else if (response.Contains("3") || response.Contains("parks")) Parks();
                else if (response.Contains("4") || response.Contains("trails")) Trails();
                else if (response.Contains("5") || response.Contains("visit")) Visits();
                else if (response.Contains("6") || response.Contains("tags")) EditAllTags();
                else
                {
                    Console.WriteLine("Please enter a valid option" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }

            }
        }
        //Accounts
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
                    Console.WriteLine("\nTry again? (y/n)");
                    string response = Console.ReadLine();
                    if (response.Contains("n")) continueToRun = false;
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
                    string result = trailAIDService.PostGeneric<AllTags>(new AllTags(), "AllTags").Result;
                }
                catch { }

                if (trailAIDService.token == null)
                {
                    Console.WriteLine("Your login information was incorrect" +
                        "\nTry again? (y/n)");
                    string response = Console.ReadLine();
                    Console.Clear();
                    if (response.Contains("n")) return false;
                    Console.Clear();
                }
                if (trailAIDService.token != null) continueToRun = false;

            }
            return true;
        }

        //User
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

                if (response.Contains("1") || response.Contains("profile"))
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
                else if (response.Contains("2") || response.Contains("profile")) UpdateProfile(User);
                else if (response.Contains("3") || response.Contains("favorite")) ViewFavoriteTrails(User);
                else if (response.Contains("4") || response.Contains("menu")) continueToRun = false;
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
                Console.Clear();
                if (editProfile.Contains("1") || editProfile.Contains("first"))
                {
                    Console.WriteLine("Please enter your first name");
                    User.FirstName = Console.ReadLine();
                    Console.Clear();
                }
                if (editProfile.Contains("2") || editProfile.Contains("last"))
                {
                    Console.WriteLine("Please enter your last name");
                    User.LastName = Console.ReadLine();
                    Console.Clear();
                }
                if (editProfile.Contains("3") || editProfile.Contains("city"))
                {
                    Console.WriteLine("Please enter your city");
                    User.City = Console.ReadLine();
                    Console.Clear();
                }
                if (editProfile.Contains("4") || editProfile.Contains("state"))
                {
                    Console.WriteLine("Please enter your state");
                    User.State = Console.ReadLine();
                    Console.Clear();
                }
                if (editProfile.Contains("5") || editProfile.Contains("menu"))
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
                    $"\nTrail ID: {trail.TrailID}\n" +
                    $"\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        //Cities
        private void Cities()
        {
            bool editCities = true;
            while (editCities)
            {
                Console.Clear();
                Console.WriteLine("What would you like to do?" +
                    "\n1. Add a City" +
                    "\n2. View all Cities" +
                    "\n3. Edit City" +
                    "\n4. Main Menu");
                string response = Console.ReadLine().ToLower();
                if (response.Contains("1") || response.Contains("add")) AddCity();
                else if (response.Contains("2") || response.Contains("all")) GetCities();
                else if (response.Contains("3") || response.Contains("edit")) EditCity();
                else if (response.Contains("4") || response.Contains("menu")) editCities = false;
                else
                {
                    Console.WriteLine("Please enter a valid option" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                Console.Clear();
            }

        }
        private void AddCity()
        {
            City newCity = new City();
            Console.Clear();

            Console.WriteLine("What is the name of the city?");
            newCity.Name = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("What State is this City in?");
            string state = Console.ReadLine();
            Console.Clear();
            var stateResult = trailAIDService.GetGenericByName<State>(state, "State").Result;
            if (stateResult != null && stateResult.Name == state) newCity.StateID = stateResult.ID;
            else
            {
                Console.WriteLine($"{state} does not exist" +
                    $"\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            string result = trailAIDService.PostGeneric<City>(newCity, "City").Result;
            if (result == "true")
            {
                Console.WriteLine("City added successfully" +
                    "\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Failure. City not added." +
                   "\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
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
                        $"\n1. Edit City" +
                        $"\n2. Search Again" +
                        $"\n3. City Menu");
                    string response = Console.ReadLine().ToLower();
                    Console.Clear();
                    if (response.Contains("1") || response.Contains("rename"))
                    {
                        string originalName = foundCity.Name;
                        Console.Clear();
                        Console.WriteLine($"What would you like to edit?" +
                            $"\n1. Name: {foundCity.Name}" +
                            $"\n2. State: {foundCity.StateName}");

                        response = Console.ReadLine().ToLower();
                        Console.Clear();
                        if (response.Contains("1") || response.Contains("name"))
                        {
                            Console.WriteLine($"What would will {foundCity.Name} be renamed to?");
                            foundCity.Name = Console.ReadLine();
                        }
                        else if (response.Contains("2") || response.Contains("state"))
                        {
                            bool editState = true;
                            while (editState)
                            {
                                editState = false;
                                Console.WriteLine("What State is this City in?");
                                string state = Console.ReadLine();
                                Console.Clear();
                                var stateResult = trailAIDService.GetGenericByName<State>(state, "State").Result;
                                if (stateResult != null && stateResult.Name == state) foundCity.StateID = stateResult.ID;
                                else
                                {
                                    Console.WriteLine($"{state} does not exist" +
                                        $"\nPress any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                    editState = true;
                                }
                            }
                        }
                        string edit = trailAIDService.EditGeneric<City>(foundCity, cityID, "City").Result;
                        if (edit == "true")
                        {
                            Console.WriteLine($"Success!" +
                                $"\nPress any key to return to city menu");
                            Console.ReadKey();
                            Console.Clear();
                            continueToRun = false;
                        }
                    }
                    else if (response.Contains("2") || response.Contains("search")) continue;
                    else if (response.Contains("3") || response.Contains("menu")) continueToRun = false;
                    else
                    {
                        Console.WriteLine("Please enter a valid option" +
                            "\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    Console.Clear();
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
                    $"\nCity ID: {city.ID}" +
                    $"\nState: {city.StateName}\n");
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
                if (response.Contains("1") || response.Contains("add")) AddPark();
                else if (response.Contains("2") || response.Contains("search")) SearchParks();
                else if (response.Contains("3") || response.Contains("menu")) editParks = false;
                else
                {
                    Console.WriteLine("Please enter a valid option" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                Console.Clear();
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
                if (response.Contains("1") || response.Contains("id")) ParkByID();
                else if (response.Contains("2") || response.Contains("name")) ParkByName();
                else if (response.Contains("3") || response.Contains("city")) ParkByCity();
                else if (response.Contains("4") || response.Contains("return")) continueToRun = false;
                else
                {
                    Console.WriteLine("Please enter a valid option" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                Console.Clear();
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
                    parkMenu = true;
                }
                catch
                {
                    Console.WriteLine("Please enter a number" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
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
                        if (response.Contains("1") || response.Contains("details"))
                        {
                            Console.Clear();
                            foundPark.PrintProps();
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();
                            Console.Clear();

                        }
                        else if (response.Contains("2") || response.Contains("edit")) EditPark(foundPark, parkID);
                        else if (response.Contains("3") || response.Contains("again")) parkMenu = false;
                        else if (response.Contains("4") || response.Contains("menu"))
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
                        Console.Clear();
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
            Console.Clear();
            Console.WriteLine("What City?");
            string searchPark = Console.ReadLine();
            var parkResults = trailAIDService.GetGenericByCity<List<Park>>(searchPark, "Park").Result;
            Console.Clear();
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

                    if (choice.Contains("1") || choice.Contains("name"))
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to rename {Park.Name} to?");
                        Park.Name = Console.ReadLine();
                    }
                    else if (choice.Contains("2") || choice.Contains("city"))
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
                    else if (choice.Contains("3") || choice.Contains("acreage"))
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
                    else if (choice.Contains("4") || choice.Contains("hours"))
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to change the Hours to?");
                        Park.Hours = Console.ReadLine();
                    }
                    else if (choice.Contains("5") || choice.Contains("phone"))
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to change the Phone Number to?");
                        Park.PhoneNumber = Console.ReadLine();
                    }
                    else if (choice.Contains("6") || choice.Contains("website"))
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to change the Website to?");
                        Park.Website = Console.ReadLine();
                    }
                    else if (choice.Contains("7") || choice.Contains("save")) editPark = false;
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
                    Console.WriteLine($"Changes Successful" +
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
                if (response.Contains("1") || response.Contains("add")) AddTrail();
                else if (response.Contains("2") || response.Contains("search")) SearchTrails();
                else if (response.Contains("3") || response.Contains("main")) editTrails = false;
                else
                {
                    Console.WriteLine("Please enter a valid option" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                Console.Clear();
            }
        }
        private void AddTrail()
        {
            Trail newTrail = new Trail();
            Console.Clear();
            bool cityID = true;
            bool parkID = true;
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
            }
            Console.WriteLine("Is this trail in a Park? (y/n)");
            string inaPark = Console.ReadLine();
            if (inaPark == "y")
            {
                while (parkID)
                {
                    Console.WriteLine("What is the Park ID?");
                    try
                    {
                        newTrail.ParkID = int.Parse(Console.ReadLine());
                        parkID = false;
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
                    newTrail.Distance = double.Parse(Console.ReadLine());
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
            Console.WriteLine("What is Type of Terrain?");
            newTrail.TypeOfTerrain = Console.ReadLine();
            Console.Clear();

            while (elevation)
            {
                Console.WriteLine("What is elevation?");
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
                Console.WriteLine("Trail added successfuly");
                Console.WriteLine($"Trail ID is {newTrail.ID}" +
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
                    "\n5. Trail Menu");
                string response = Console.ReadLine().ToLower();
                if (response.Contains("1") || response.Contains("id")) TrailByID();
                else if (response.Contains("2") || response.Contains("name")) TrailByName();
                else if (response.Contains("3") || response.Contains("city")) TrailByCity();
                else if (response.Contains("4") || response.Contains("park")) TrailByPark();
                else if (response.Contains("5") || response.Contains("menu")) continueToRun = false;
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
                Console.Clear();
                Console.WriteLine("Enter the ID of the trail");
                try
                {
                    trailID = int.Parse(Console.ReadLine());
                    Console.Clear();
                    trailMenu = true;
                }
                catch
                {
                    Console.WriteLine("Please enter a number" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
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
                        $"\n4. Search Menu");
                        string response = Console.ReadLine().ToLower();
                        if (response.Contains("1") || response == "view")
                        {
                            Console.Clear();
                            foundTrail.PrintProps();
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();
                            Console.Clear();

                        }
                        else if (response.Contains("2") || response.Contains("edit"))
                        {
                            EditTrail(foundTrail, trailID);
                        }
                        else if (response.Contains("3") || response.Contains("search")) trailMenu = false;
                        else if (response.Contains("4") || response.Contains("menu"))
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

        }
        private void TrailByName()
        {
            bool search = true;
            while (search)
            {
                Console.Clear();
                Console.WriteLine("What is the name of the trail?");
                string searchTrail = Console.ReadLine();
                Console.Clear();
                var trailResults = trailAIDService.GetGenericByName<List<Trail>>(searchTrail, "Trail").Result;
                if (trailResults != null)
                {
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
                    else { search = false; }

                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Trail not found. Search again? (y/n)");
                    string searchAgain = Console.ReadLine();
                    if (searchAgain.Contains("y")) continue;
                    else search = false;
                }
            }
        }
        private void TrailByCity()
        {
            Console.Clear();
            Console.WriteLine("What City?");
            string searchTrail = Console.ReadLine();
            var trailResults = trailAIDService.GetGenericByCity<List<Trail>>(searchTrail, "Trail").Result;
            Console.Clear();
            foreach (var trail in trailResults)
            {
                Console.WriteLine($"Trail Name: {trail.Name}" +
                    $"\nTrail ID: {trail.ID}\n");
            }
            Console.WriteLine("Would you like to see more details about a specific trail? (y/n)");
            string response = Console.ReadLine();
            if (response == "y")
            {
                TrailByID();
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
                TrailByID();
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

                    Trail.PrintPropsForEdit();
                    Console.WriteLine("\nEnter the number of your seleciton");
                    string choice = Console.ReadLine().ToLower();

                    if (choice == "1" || choice.Contains("name"))
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to rename {Trail.Name} to?");
                        Trail.Name = Console.ReadLine();
                    }
                    else if (choice.Contains("2") || choice.Contains("city"))
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
                    else if (choice.Contains("3") || choice.Contains("park"))
                    {
                        Console.Clear();
                        bool parkID = true;
                        while (parkID)
                        {
                            Console.WriteLine("What would you like to change the Park ID to?");
                            try
                            {
                                Trail.ParkID = int.Parse(Console.ReadLine());
                                parkID = false;
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

                    else if (choice.Contains("4") || choice.Contains("difficulty"))
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to change the Difficulty to?");
                        Trail.Difficulty = Console.ReadLine();
                    }

                    else if (choice.Contains("5") || choice.Contains("description"))
                    {
                        Console.Clear();
                        Console.WriteLine($"Write a new description");
                        Trail.Description = Console.ReadLine();
                    }

                    else if (choice.Contains("6") || choice.Contains("distance"))
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

                    else if (choice.Contains("7") || choice.Contains("terrain"))
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to change the Terrain to?");
                        Trail.TypeOfTerrain = Console.ReadLine();
                    }

                    else if (choice.Contains("8") || choice.Contains("tags"))
                    {
                        string tagResult = "";
                        bool editTags = true;
                        while (editTags)
                        {
                            Console.Clear();
                            Console.WriteLine($"What would you like to do?" +
                                $"\n1. Add Tag" +
                                $"\n2. Delete tag" +
                                $"\n3. Edit Menu");
                            string tagChoice = Console.ReadLine().ToLower();
                            Console.Clear();
                            if (tagChoice == ("1") || tagChoice.Contains("add"))
                            {
                                Trail.PrintTags();
                                Console.WriteLine("\nWhat Tag would you like to add?");
                                Trail.AddTags = Console.ReadLine();
                                Console.Clear();
                                tagResult = trailAIDService.EditGeneric<Trail>(Trail, trailID, "Trail").Result;
                                if (tagResult == "true")
                                {
                                    Trail.Tags = trailAIDService.GetGenericByID<Trail>(trailID, "Trail").Result.Tags;

                                    Console.WriteLine("Success!" +
                                        "\nPress any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                if (tagResult == "tag")
                                {
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                Trail.AddTags = null;
                            }
                            if (tagChoice.Contains("2") || tagChoice.Contains("delete"))
                            {
                                Trail.PrintTags();
                                Console.WriteLine("\nWhat Tag would you like to delete?");
                                Trail.DeleteTags = Console.ReadLine();
                                Console.Clear();
                                tagResult = trailAIDService.EditGeneric<Trail>(Trail, trailID, "Trail").Result;
                                if (tagResult == "true")
                                {
                                    Trail.DeleteTags = null;
                                    Trail.Tags = trailAIDService.GetGenericByID<Trail>(trailID, "Trail").Result.Tags;
                                    Console.WriteLine("Success!" +
                                        "\nPress any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();

                                }
                                if (tagResult == "false")
                                {
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                Trail.DeleteTags = null;
                            }
                            if (tagChoice.Contains("3") || tagChoice.Contains("menu")) editTags = false;
                        }
                        Console.Clear();
                    }
                    else if (choice.Contains("9") || choice.Contains("elevation"))
                    {
                        Console.Clear();
                        bool elevation = true;
                        while (elevation)
                        {
                            Console.WriteLine("What would you like to change the Elevation to?");
                            try
                            {
                                Trail.Elevation = int.Parse(Console.ReadLine());
                                elevation = false;
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

                    else if (choice.Contains("10") || choice.Contains("type"))
                    {
                        Console.Clear();
                        Console.WriteLine($"What would you like to change the Route Type to?");
                        Trail.RouteType = Console.ReadLine();
                    }
                    else if (choice.Contains("11") || choice.Contains("save")) editTrail = false;
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
                string edit = trailAIDService.EditGeneric<Trail>(Trail, trailID, "Trail").Result;
                if (edit == "true")
                {
                    Console.Clear();
                    Console.WriteLine($"Changes Successful" +
                        $"\nPress any key to return to trail menu");
                    Console.ReadKey();
                    Console.Clear();
                    success = true;
                }
                else if (edit == "city & park")
                {
                    Trail.CityID = originalCityID;
                    Trail.ParkID = originalParkID;
                    Console.WriteLine($"Press any key to return to try again");

                    Console.ReadKey();
                    Console.Clear();
                }
                else if (edit == "city")
                {
                    Trail.CityID = originalCityID;
                    Console.WriteLine($"Press any key to return to try again");

                    Console.ReadKey();
                    Console.Clear();
                }
                else if (edit == "park")
                {
                    Trail.ParkID = originalParkID;
                    Console.WriteLine($"Press any key to return to try again");

                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("catastrophic error" +
                        "\nPress any key to continue...");
                    Console.ReadKey();

                }
            }
        }

        //Visits
        private void Visits()
        {
            bool editVisits = true;
            while (editVisits)
            {
                Console.Clear();
                Console.WriteLine("What would you like to do?" +
                    "\n1. Add a Visit" +
                    "\n2. View all Visited Trails" +
                    "\n3. Update Visit" +
                    "\n4. Main Menu");
                string response = Console.ReadLine().ToLower();
                if (response.Contains("1") || response.Contains("add")) AddVisit();
                else if (response.Contains("2") || response.Contains("all")) GetVisits();
                else if (response.Contains("3") || response.Contains("update")) VisitByTrailID();
                else if (response.Contains("4") || response.Contains("main")) editVisits = false;
                else
                {
                    Console.WriteLine("Please enter a valid option" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                Console.Clear();
            }
        }
        private void GetVisits()
        {
            Console.Clear();
            List<Visited> allVisits = trailAIDService.GetGeneric<List<Visited>>("Visited").Result;
            foreach (var visited in allVisits)
            {
                Console.WriteLine($"Trail Name: {visited.TrailName}" +
                    $"\nTrail ID: {visited.TrailID}" +
                    $"\nRating: {visited.Rating}\n");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        private void AddVisit()
        {
            Visited newVisit = new Visited();
            Console.Clear();
            bool trailID = true;
            bool rating = true;
            bool review = true;
            bool favorites = true;
            string response = "";
            while (trailID)
            {
                Console.WriteLine("What is the Trail ID?");
                try
                {
                    newVisit.TrailID = int.Parse(Console.ReadLine());
                    Console.Clear();
                }
                catch
                {
                    Console.WriteLine("Please enter a valid ID" +
                        "\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
                var foundTrail = trailAIDService.GetGenericByID<Trail>((int)newVisit.TrailID, "Trail").Result;
                if (foundTrail != null)
                {
                    Console.WriteLine($"This ID matches a visit for the trail {foundTrail.Name}");
                    trailID = false;
                    Console.WriteLine("Did you Visit this Trail?(y/n)");
                    response = Console.ReadLine();
                    Console.Clear();
                    if (response.Contains("y")) { }
                    else
                    {
                        trailID = false;
                        rating = false;
                        review = false;
                        favorites = false;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Cannot find a tail by that ID." +
                        "\nTry a new ID? (y/n)");
                    response = Console.ReadLine();
                    Console.Clear();
                    if (response.Contains("y")) continue;
                    else
                    {
                        trailID = false;
                        rating = false;
                        review = false;
                        favorites = false;
                        break;
                    }
                }
                while (rating)
                {
                    Console.WriteLine("What would you rate this trail? (1-5)");
                    try
                    {
                        newVisit.Rating = int.Parse(Console.ReadLine());
                        Console.Clear();
                    }
                    catch
                    {
                        Console.WriteLine("Please enter a valid number" +
                            "\nPress any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                    if (newVisit.Rating > 0 && newVisit.Rating <= 5) rating = false;
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a number 1-5" +
                            "\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }

                while (review)
                {
                    review = false;
                    Console.WriteLine("Would you like to review this trail? (y/n)");
                    string writeReview = Console.ReadLine().ToLower();
                    Console.Clear();
                    if (writeReview.Contains("y"))
                    {
                        Console.WriteLine("Write a short review for this trail");
                        newVisit.Review = Console.ReadLine();
                        Console.Clear();
                    }
                    else if (writeReview.Contains("n")) { }
                    else
                    {
                        review = true;
                        Console.Clear();
                        Console.WriteLine("Please enter a valid option" +
                            "\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }

                }

                while (favorites)
                {
                    favorites = false;
                    Console.WriteLine("Would you like this trail to be on your favorites? (y/n)");
                    string addToFavorites = Console.ReadLine().ToLower();
                    if (addToFavorites.Contains("y")) newVisit.AddToFavorites = true;
                    else if (addToFavorites.Contains("n")) newVisit.AddToFavorites = false;
                    else
                    {
                        favorites = true;
                        Console.Clear();
                        Console.WriteLine("Please enter a valid option" +
                            "\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }

                }

                string result = trailAIDService.PostGeneric<Visited>(newVisit, "Visited").Result;
                if (result == "true")
                {
                    Console.WriteLine("Visit added successfuly");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Could not add visit" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        private void VisitByTrailID()
        {
            Console.Clear();
            bool continueToRun = true;
            bool visitMenu = true;
            int trailID = 0;
            while (continueToRun)
            {
                Console.Clear();
                Console.WriteLine("Enter the ID of the trail you've visited");
                try
                {
                    trailID = int.Parse(Console.ReadLine());
                    Console.Clear();
                    visitMenu = true;
                }
                catch
                {
                    Console.WriteLine("Please enter a number" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                var foundVisit = trailAIDService.GetGenericByID<Visited>(trailID, "Visited").Result;
                if (foundVisit != null)
                {
                    while (visitMenu)
                    {
                        Console.WriteLine($"This ID matches a visit for the trail trail {foundVisit.TrailName}" +
                        $"\nWhat would you like to do?" +
                        $"\n1. View Visit Details" +
                        $"\n2. Edit Visit" +
                        $"\n3. Search Again" +
                        $"\n4. Visit Menu");
                        string response = Console.ReadLine().ToLower();
                        if (response.Contains("1") || response == "view")
                        {
                            Console.Clear();
                            foundVisit.PrintProps();
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();
                            Console.Clear();

                        }
                        else if (response.Contains("2") || response.Contains("edit"))
                        {
                            EditVisit(foundVisit, trailID);
                        }
                        else if (response.Contains("3") || response.Contains("search")) visitMenu = false;
                        else if (response.Contains("4") || response.Contains("menu"))
                        {
                            visitMenu = false;
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
        }
        private void EditVisit(Visited Visited, int trailID)
        {
            bool editVisit = true;
            bool success = false;
            var originalTrailID = Visited.TrailID;
            while (success == false)
            {
                editVisit = true;
                while (editVisit)
                {
                    Console.Clear();
                    Console.WriteLine("What would you like to edit?");

                    Visited.PrintPropsForEdit();
                    Console.WriteLine("\nEnter the number of your seleciton");
                    string choice = Console.ReadLine().ToLower();

                    if (choice.Contains("1") || choice.Contains("id"))
                    {
                        Console.Clear();
                        bool id = true;
                        while (id)
                        {
                            Console.WriteLine("What would you like to change the Trail ID to?");
                            try
                            {
                                Visited.TrailID = int.Parse(Console.ReadLine());
                                id = false;
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
                    else if (choice.Contains("2") || choice.Contains("favorite"))
                    {
                        Console.Clear();
                        bool favorites = true;
                        while (favorites)
                        {
                            favorites = false;
                            Console.WriteLine("Would you like this trail to be on your favorites? (y/n)");
                            string addToFavorites = Console.ReadLine().ToLower();
                            if (addToFavorites.Contains("y")) Visited.AddToFavorites = true;
                            else if (addToFavorites.Contains("n")) Visited.AddToFavorites = false;
                            else
                            {
                                favorites = true;
                                Console.Clear();
                                Console.WriteLine("Please enter a valid option" +
                                    "\nPress any key to continue...");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        Console.Clear();
                    }
                    else if (choice.Contains("3") || choice.Contains("rating"))
                    {
                        Console.Clear();
                        bool rating = true;
                        while (rating)
                        {
                            Console.WriteLine("What would you like to change the Rating to? (1-5)");
                            try
                            {
                                Visited.Rating = int.Parse(Console.ReadLine());
                                Console.Clear();
                            }
                            catch
                            {
                                Console.WriteLine("Please enter a valid number" +
                                    "\nPress any key to continue");
                                Console.ReadKey();
                                Console.Clear();
                                continue;
                            }
                            if (Visited.Rating > 0 && Visited.Rating <= 5) rating = false;
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Please enter a number 1-5" +
                                    "\nPress any key to continue...");
                                Console.ReadKey();
                                Console.Clear();
                            }

                        }
                    }

                    else if (choice.Contains("4") || choice.Contains("review"))
                    {
                        Console.Clear();
                        Console.WriteLine($"Write a small review for {Visited.TrailName}");
                        Visited.Review = Console.ReadLine();
                    }
                    else if (choice.Contains("5") || choice.Contains("save")) editVisit = false;
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
                string edit = trailAIDService.EditGeneric<Visited>(Visited, trailID, "Visited").Result;
                if (edit == "true")
                {
                    Console.Clear();
                    Console.WriteLine($"Changes Successful" +
                        $"\nPress any key to return to visited menu");
                    Console.ReadKey();
                    Console.Clear();
                    success = true;
                }
                else if (edit == "trail")
                {
                    Visited.TrailID = originalTrailID;
                    Console.WriteLine($"Press any key to return to try again");

                    Console.ReadKey();
                    Console.Clear();
                }

                else
                {
                    Console.WriteLine("catastrophic error" +
                        "\nPress any key to continue...");
                    Console.ReadKey();

                }
            }
        }

        //Tags
        private void EditAllTags()
        {
            List<AllTags> result = trailAIDService.GetGeneric<List<AllTags>>("AllTags").Result;
            //var allTags = result[0];
            var allTags = trailAIDService.GetAllTags().Result;
            bool editTags = true;
            string tagResult = "";
            while (editTags)
            {
                Console.Clear();
                Console.WriteLine($"What would you like to do?" +
                    $"\n1. View Tags" +
                    $"\n2. Add Tag" +
                    $"\n3. Delete tag" +
                    $"\n4. Main Menu");
                string tagChoice = Console.ReadLine().ToLower();
                Console.Clear();
                if (tagChoice == ("1") || tagChoice.Contains("view"))
                {
                    allTags.PrintTags();

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                if (tagChoice == ("2") || tagChoice.Contains("add"))
                {
                    allTags.PrintTags();
                    Console.WriteLine("\nWhat Tag would you like to add?");
                    allTags.AddTags = Console.ReadLine();
                    Console.Clear();
                    tagResult = trailAIDService.EditTags(allTags).Result;
                    if (tagResult == "true")
                    {
                        allTags.ListOfAllTags = trailAIDService.GetAllTags().Result.ListOfAllTags;

                        Console.WriteLine("Success!" +
                            "\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    if (tagResult == "tag")
                    {
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    allTags.AddTags = null;
                }
                if (tagChoice.Contains("3") || tagChoice.Contains("delete"))
                {
                    allTags.PrintTags();
                    Console.WriteLine("\nWhat Tag would you like to delete?");
                    allTags.DeleteTags = Console.ReadLine();
                    Console.Clear();
                    tagResult = trailAIDService.EditTags(allTags).Result;
                    if (tagResult == "true")
                    {
                        allTags.DeleteTags = null;
                        allTags.ListOfAllTags = trailAIDService.GetAllTags().Result.ListOfAllTags;
                        Console.WriteLine("Success!" +
                            "\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();

                    }
                    if (tagResult == "false")
                    {
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    allTags.DeleteTags = null;
                }
                if (tagChoice.Contains("4") || tagChoice.Contains("menu")) editTags = false;
            }
            Console.Clear();
        }
    }
}

