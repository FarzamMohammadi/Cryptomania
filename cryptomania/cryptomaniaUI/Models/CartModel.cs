using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace cryptomaniaUI.Models
{
    class CartModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string InCart { get; set; }
        public async static void CheckCart()
        {
            string crypto = "";
            try
            {
                var url = "https://localhost:5001/api/carts/";
                string username = SignedInModel.Username;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + username);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    crypto = reader.ReadToEnd();
                }
                CartModel returnedCart = JsonConvert.DeserializeObject<CartModel>(crypto);
                if(returnedCart.Id != null) {
                    SignedInModel.CurrentCart = returnedCart;
                }

            }
            catch
            {
                SignedInModel.CurrentCart = null;
                await CreateNewCart();
            }
        }
        public static async Task<bool> CreateNewCart()
        {
            CartModel newCart = new CartModel() { InCart = "", Username = SignedInModel.Username };
            try
            {
                // Post new user data to api 
                var json = JsonConvert.SerializeObject(newCart);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "https://localhost:5001/api/carts";
                using var client = new HttpClient();
                var response = await client.PostAsync(url, data);
                string result = response.Content.ReadAsStringAsync().Result;

                // If already exits or any other false code the return the msg below
                if (response.ReasonPhrase == "Conflict")
                {
                    CheckCart();
                    return false;
                }
                return true;
            }
            catch
            {
                CheckCart();
                MessageBox.Show("Issue creating cart.");
                return false;
            }
        }
    }
}

