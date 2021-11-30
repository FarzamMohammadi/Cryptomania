using cryptomania.Controllers;
using cryptomania.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace cryptomaniaUI.Models
{
    class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static async Task<bool> AddUserAsync(UserModel newUser)
        {
            try
            {
                // Post new user data to api 
                var json = JsonConvert.SerializeObject(newUser);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "https://localhost:5001/api/users";
                using var client = new HttpClient();
                var response = await client.PostAsync(url, data);
                string result = response.Content.ReadAsStringAsync().Result;

                // If already exits or any other false code the return the msg below
                if(response.ReasonPhrase == "Conflict")
                {
                    MessageBox.Show("User Exists");
                }
                    MessageBox.Show("User Created.");
                    return true;
            }
            catch
            {
                MessageBox.Show("User was not saved. Please try again");
                return false;
            }
        }

        public static bool CheckUserCredentials(UserModel loggingInUser)
        {
            string user = "";
            try
            {
                var url = "https://localhost:5001/api/users/";
                string username = loggingInUser.Username;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url+ username);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    user = reader.ReadToEnd();
                }
                UserModel returnedUser = JsonConvert.DeserializeObject<UserModel>(user);

                if(returnedUser.Password == loggingInUser.Password)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}
