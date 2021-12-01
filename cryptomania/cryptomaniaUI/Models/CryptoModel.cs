using System;
using System.Collections.Generic;
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
    }
}
