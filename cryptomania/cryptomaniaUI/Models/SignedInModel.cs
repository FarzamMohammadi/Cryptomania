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
        public static WalletModel CurrentWallet { get; set; }

      
    }
}
