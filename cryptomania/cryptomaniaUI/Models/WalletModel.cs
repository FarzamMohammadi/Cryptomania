using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace cryptomaniaUI.Models
{
    class WalletModel
    {
        public string Id { get; set; }
        public string WalletAddress { get; set; }
        public string Username { get; set; }
        public string Purchases { get; set; }
        public static WalletModel CheckForUserWallet()
        {
            string wallet = "";
            try
            {
                var url = "https://localhost:5001/api/wallets/";
                string username = SignedInModel.Username;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + username);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    wallet = reader.ReadToEnd();
                }
                if (wallet != "")
                {
                    WalletModel returnedWallet = JsonConvert.DeserializeObject<WalletModel>(wallet);
                    return returnedWallet;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
        public static async Task<bool> AddWalletAsync(WalletModel newWallet)
        {
            try
            {
                // Post new user data to api 
                var json = JsonConvert.SerializeObject(newWallet);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "https://localhost:5001/api/wallets";
                using var client = new HttpClient();
                var response = await client.PostAsync(url, data);
                string result = response.Content.ReadAsStringAsync().Result;

                // If already exits or any other false code the return the msg below
                if (response.ReasonPhrase == "Conflict")
                {
                    return false;
                }
                return true;
            }
            catch
            {
                MessageBox.Show("Issue creating your new wallet");
                return false;
            }
        }
        public async static Task<bool> UpdateWithNewAddress(WalletModel walletToUpdate)
        {
            try
            {
                var url = "https://localhost:5001/api/wallets/";
                var json = JsonConvert.SerializeObject(walletToUpdate);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using var client = new HttpClient();
                var response = await client.PutAsync(url, data);

                // If already exits or any other false code the return the msg below
                if (response.ReasonPhrase == "Conflict")
                {
                    MessageBox.Show("Could not update wallet address.");
                    return false;
                }
                return true;
            }
            catch
            {
                MessageBox.Show("Could not update wallet address.");

                return false;
            }
        }

        public static void Serialize(Stream output, WalletModel input)
        {
            var ser = new DataContractSerializer(input.GetType());
            ser.WriteObject(output, input);
        }
    }
}
