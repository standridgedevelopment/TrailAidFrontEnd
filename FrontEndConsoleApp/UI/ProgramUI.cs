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
        RegisterService registerService = new RegisterService();
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
            var user = registerService.GetUser();
            return user.Result;
        }
        private void MainMenu(User User)
        {
            bool continueToRun = true;
            while (continueToRun)
            {
                Console.WriteLine("Main Menu");
                Console.WriteLine("\nWhat Would You Like To Do?");
                Console.WriteLine("1. Edit Profile");
                Console.WriteLine("2. Edit Cities");
                Console.WriteLine("3. Edit Parks");
                Console.WriteLine("4. Edit Trails");
                Console.WriteLine("6. Edit Park Visits");
                Console.WriteLine("5. Edit List of Tags");

                string response = Console.ReadLine().ToLower();

                if (response == "1" || response == "edit profile")
                {
                    UserProfile(User);
                }
                if (response == "2" || response == "edit cities") EditCities();
                if (response == "3" || response == "edit Parks") EditParks();
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

                bool result = registerService.Register(information).Result;
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
                    registerService.token = registerService.GetToken(information).Result;
                }
                catch { }

                if (registerService.token == null)
                {
                    Console.WriteLine("Your login information was incorrect" +
                        "\nPress any key to try again...");
                    Console.ReadKey();
                    Console.Clear();
                }
                if (registerService.token != null) continueToRun = false;

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

                    if (registerService.PostUser(newUser) != null)
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
                    User = registerService.GetUser().Result;
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
                    if (registerService.EditUserAsync(User).Result)
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
        private void EditCities()
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
            bool result = registerService.PostGeneric<City>(newCity, "City").Result;
            if (result == true)
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
                var foundCity = registerService.GetGenericByID<City>(cityID, "City").Result;
                if (foundCity != null)
                {
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
                        bool edit = registerService.EditGeneric<City>(foundCity, cityID, "City").Result;
                        if (edit)
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
            List<City> allCities = registerService.GetGeneric<List<City>>("City").Result;
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
        private void EditParks()
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

            bool result = registerService.PostGeneric<Park>(newPark, "Park").Result;
            if (result == true)
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
            var foundPark = registerService.GetGenericByID<Park>(parkID, "Park").Result;
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
                        parkMenu = false;
                        continueToRun = false;
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
        }
        private void ParkByName()
        {
            Console.Clear();
            Console.WriteLine("What is the name of the park?");
            string searchPark = Console.ReadLine();
            Console.Clear();
            var parkResults = registerService.GetGenericByName<List<Park>>(searchPark, "Park").Result;
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
            var parkResults = registerService.GetGenericByCity<List<Park>>(searchPark, "Park").Result;
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
                else if (choice == "7" || choice == "return to park menu") editPark = false;
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid option" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            bool edit = registerService.EditGeneric<Park>(Park, parkID, "Park").Result;
            if (edit)
            {
                Console.Clear();
                Console.WriteLine($"Changes Sucessful" +
                    $"\nPress any key to return to city menu");
                Console.ReadKey();
                Console.Clear();
            }

        }
    }
}
