using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cryptomania.Models
{
    public class Crypto
    {
        //id
        public string Id { get; set; }
        //name
        public string Name { get; set; }
        //logo_url
        public string LogoUrl { get; set; }
        //price
        public string Price { get; set; }
        //market_cap
        public string MarketCap { get; set; }
        //1d/price_change_pct
        public string PriceChangePct { get; set; }
    }
}
