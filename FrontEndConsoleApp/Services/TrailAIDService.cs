using Front_End_Console_App.Models;
using FrontEndConsoleApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;

namespace FrontEndConsoleApp.Services
{
    public class TrailAIDService
    {


        private readonly HttpClient _httpClient = new HttpClient();
        private static string Url = "https://localhost:44375/"
        private static string APIUrl = "https://localhost:44375/api/";
        public string token { get; set; }


        public async Task<bool> Register(RegisterBindingModel model)
        {
            string strModel = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(strModel, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"{APIUrl}Account/Register", content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            List<string> message = response.Content.ReadAsStringAsync().Result.Split('"').ToList();
            foreach (var phrase in message)
            {
                if (phrase.Contains("The") || phrase.Contains("Passwords") || phrase.Contains("Email")) 
                    Console.WriteLine(phrase);
            }
            return false;
           
        }
        public async Task<string> GetToken(TokenCreate model)
        {
            var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username", model.username ),
                        new KeyValuePair<string, string> ( "Password", model.password )
                    };
            var content = new FormUrlEncodedContent(pairs);
            HttpResponseMessage response = _httpClient.PostAsync($"{Url}token", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Dictionary<string, string> tokenDictionary =
               JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            string token = tokenDictionary["access_token"];
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            if (response.IsSuccessStatusCode)
            {
                
                var tokenObject = await response.Content.ReadAsAsync<Token>();
                return token;
            }
            return null;
        }
        public async Task<T> GetGeneric<T>(string typeUrl)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{APIUrl}{typeUrl}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            return default(T);
        }
        public async Task<T> GetGenericByID<T>(int ID, string typeUrl)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{APIUrl}{typeUrl}/{ID}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            return default(T);
        }
        public async Task<T> GetGenericByCity<T>(string cityName, string typeUrl)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{APIUrl}{typeUrl}?cityName={cityName}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            return default(T);
        }
        public async Task<T> GetGenericByName<T>(string name, string typeUrl)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{APIUrl}{typeUrl}?name={name}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            return default(T);
        }
        public async Task<T> GetByParkName<T>(string parkName, string typeUrl)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{APIUrl}{typeUrl}?parkName={parkName}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            return default(T);
        }
        public async Task<string> PostGeneric<T>(T model, string typeUrl)
        {
            string strModel = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(strModel, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"{APIUrl}{typeUrl}", content);
            if (response.IsSuccessStatusCode)
            {
                return "true";
            }
            List<string> message = response.Content.ReadAsStringAsync().Result.Split('"').ToList();
            foreach (var phrase in message)
            {
                if (phrase.Contains("Invalid"))
                {
                    Console.WriteLine(phrase);
                    return "invalid ID";
                }
            }
            return "error";
        }
        public async Task<string> EditGeneric<T>(T model, int ID, string typeUrl)
        {
            string strModel = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(strModel, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync($"{APIUrl}{typeUrl}/{ID}", content);
            if (response.IsSuccessStatusCode)
            {
                return "true";
            }
            List<string> message = response.Content.ReadAsStringAsync().Result.Split('"').ToList();
            foreach (var phrase in message)
            {
                if (phrase.Contains("Invalid"))
                {
                    Console.WriteLine(phrase);
                    return "invalid ID";
                }
                    
            }
            return "error";
        }

        //////////Users
        public async Task<bool> PostUser(User model)
        {
            string strModel = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(strModel, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"{APIUrl}User", content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        
        public async Task<User> GetUser()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{APIUrl}User");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<User>();
            }
            return default(User);
        }
        public async Task<bool> EditUserAsync(User model)
        {
            string strModel = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(strModel, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync($"{APIUrl}User", content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
