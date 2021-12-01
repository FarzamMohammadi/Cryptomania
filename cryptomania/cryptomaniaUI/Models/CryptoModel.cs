using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace cryptomaniaUI.Models
{
    class CryptoModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string Price { get; set; }
        public string MarketCap { get; set; }
        public string PriceChangePct { get; set; }

        public static List<CryptoModel> GetCryptos()
        {
            string cryptos = "";
            try
            {
                var url = "https://localhost:5001/api/cryptos";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    cryptos = reader.ReadToEnd();
                }
                List<CryptoModel> cryptoList = JsonConvert.DeserializeObject<List<CryptoModel>>(cryptos);
                return cryptoList;
            }
            catch
            {
                return null;
            }
        }
    }
}
