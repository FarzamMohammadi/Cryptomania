using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace cryptomaniaUI.Models
{
    class SignedInModel
    {
        public static bool LoggedIn { get; set; }
        public static string Username { get; set; }

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
                if(wallet != "")
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
    }
}
