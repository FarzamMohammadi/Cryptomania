﻿using cryptomaniaUI.Commands;
using cryptomaniaUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cryptomaniaUI.Views
{
    /// <summary>
    /// Interaction logic for CryptoView.xaml
    /// </summary>
    public partial class CryptoView : UserControl
    {
        public CryptoView()
        {
            InitializeComponent();
            LoadCryptoInfo();
            CartModel.CheckCart();
        }

        private void LoadCryptoInfo()
        {
            List<CryptoModel> cryptos = CryptoModel.GetCryptos();
            SignedInModel.Cryptos = cryptos;

            for (int i = 0; i < cryptos.Count; i++)
            {
                if(i == 0)
                {
                    // Set Image
                    image.Source = new BitmapImage(new Uri("https://cryptologos.cc/logos/" + cryptos[i].Name.ToLower() + "-" + cryptos[i].Id.ToLower() + "-logo.png?v=014"));
                    cName.Content = cryptos[i].Name;
                    currencyPrice.Content = "$"+ cryptos[i].Price;
                    currencyMCap.Content = "$"+ cryptos[i].MarketCap;
                    currencyChange.Content = cryptos[i].PriceChangePct;
                }
                else if (i == 1)
                {
                    // Set Image
                    image1.Source = new BitmapImage(new Uri("https://cryptologos.cc/logos/" + cryptos[i].Name.ToLower() + "-" + cryptos[i].Id.ToLower() + "-logo.png?v=014"));
                    cName1.Content = cryptos[i].Name;
                    currencyPrice1.Content = "$" + cryptos[i].Price;
                    currencyMCap1.Content = "$" + cryptos[i].MarketCap;
                    currencyChange1.Content = cryptos[i].PriceChangePct;
                }
                else if (i == 2)
                {
                    // Set Image
                    image2.Source = new BitmapImage(new Uri("https://cryptologos.cc/logos/binance-usd-busd-logo.png?v=014"));
                    cName2.Content = cryptos[i].Name;
                    currencyPrice2.Content = "$" + cryptos[i].Price;
                    currencyMCap2.Content = "$" + cryptos[i].MarketCap;
                    currencyChange2.Content = cryptos[i].PriceChangePct;
                }
                else if (i == 3)
                {
                    // Set Image
                    image3.Source = new BitmapImage(new Uri("https://cryptologos.cc/logos/" + cryptos[i].Name.ToLower() + "-" + cryptos[i].Id.ToLower() + "-logo.png?v=014"));
                    cName3.Content = cryptos[i].Name;
                    currencyPrice3.Content = "$" + cryptos[i].Price;
                    currencyMCap3.Content = "$" + cryptos[i].MarketCap;
                    currencyChange3.Content = cryptos[i].PriceChangePct;
                }
                else if (i == 4)
                {
                    // Set Image
                    image4.Source = new BitmapImage(new Uri("https://cryptologos.cc/logos/tron-trx-logo.png?v=014"));
                    cName4.Content = cryptos[i].Name;
                    currencyPrice4.Content = "$" + cryptos[i].Price;
                    currencyMCap4.Content = "$" + cryptos[i].MarketCap;
                    currencyChange4.Content = cryptos[i].PriceChangePct;
                }
                else if (i == 5)
                {
                    // Set Image
                    image5.Source = new BitmapImage(new Uri("https://cryptologos.cc/logos/" + cryptos[i].Name.ToLower() + "-" + cryptos[i].Id.ToLower() + "-logo.png?v=014"));
                    cName5.Content = cryptos[i].Name;
                    currencyPrice5.Content = "$" + cryptos[i].Price;
                    currencyMCap5.Content = "$" + cryptos[i].MarketCap;
                    currencyChange5.Content = cryptos[i].PriceChangePct;
                }
                else if (i == 6)
                {
                    // Set Image
                    image6.Source = new BitmapImage(new Uri("https://cryptologos.cc/logos/" + cryptos[i].Name.ToLower() + "-" + cryptos[i].Id.ToLower() + "-logo.png?v=014"));
                    cName6.Content = cryptos[i].Name;
                    currencyPrice6.Content = "$" + cryptos[i].Price;
                    currencyMCap6.Content = "$" + cryptos[i].MarketCap;
                    currencyChange6.Content = cryptos[i].PriceChangePct;
                }
                else if (i == 7)
                {
                    // Set Image
                    image7.Source = new BitmapImage(new Uri("https://cryptologos.cc/logos/" + cryptos[i].Name.ToLower() + "-" + cryptos[i].Id.ToLower() + "-logo.png?v=014"));
                    cName7.Content = cryptos[i].Name;
                    currencyPrice7.Content = "$" + cryptos[i].Price;
                    currencyMCap7.Content = "$" + cryptos[i].MarketCap;
                    currencyChange7.Content = cryptos[i].PriceChangePct;
                }
                else if (i == 8)
                {
                    // Set Image
                    image8.Source = new BitmapImage(new Uri("https://cryptologos.cc/logos/terra-luna-luna-logo.png?v=014"));
                    cName8.Content = cryptos[i].Name;
                    currencyPrice8.Content = "$" + cryptos[i].Price;
                    currencyMCap8.Content = "$" + cryptos[i].MarketCap;
                    currencyChange8.Content = cryptos[i].PriceChangePct;
                }
            }
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            Mediator.Notify("GoToProfileView", "");
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {

        }
        public void AddItemToCart(string currencyId)
        {
            if(SignedInModel.CurrentCart.InCart.Length >= 10)
            {
                // If more than 2 cart items exist splits and adds new item accordingly
                string[] items = SignedInModel.CurrentCart.InCart.Split('/');
                List<string> currencyNames = new List<string>();
                List<string> qty = new List<string>();
                // Split currency name from qty 
                for (int i = 1; i < items.Length; i++)
                {
                    string[] cartItem = items[i].Split('-');
                    currencyNames.Add(cartItem[0]);
                    qty.Add(cartItem[1]);
                }
                // If the item to add already exists in cart adds 1 to existing qty
                if(currencyNames.Contains(currencyId))
                {
                    qty[currencyNames.IndexOf(currencyId)] = (int.Parse(qty[currencyNames.IndexOf(currencyId)]) + 1).ToString();
                }
                else //If it doesnt exist
                {
                    currencyNames.Add(currencyId);
                    qty.Add("1");
                }

                SignedInModel.CurrentCart.InCart = "";
                for (int i = 0; i < currencyNames.Count; i++)
                {
                    SignedInModel.CurrentCart.InCart = SignedInModel.CurrentCart.InCart + "/" + currencyNames[i] + "-" + qty[i];
                }

            }
            else if (SignedInModel.CurrentCart.InCart.Length == 0) // If cart is empty
            {
                SignedInModel.CurrentCart.InCart = "/" + currencyId + "-1";
            }
            else // If cart contains only 1 item
            {
                string[] item = SignedInModel.CurrentCart.InCart.Split('/');
                string currencyName = "";
                string qty = "";
                // Split currency name from qty 
                for (int i = 1; i < item.Length; i++)
                {
                    string[] cartItem = item[i].Split('-');
                    currencyName = cartItem[0];
                    qty = cartItem[1];                    
                }
                //If the currency to add is not the same as the existing one
                if (currencyName != currencyId)
                {
                    SignedInModel.CurrentCart.InCart = SignedInModel.CurrentCart.InCart + "/" + currencyId + "-1";
                }
                else // If it is the same then change qty to 2
                {
                    SignedInModel.CurrentCart.InCart = "/" + currencyId + "-2";
                }
            }
        }
        private void addBtn1_Click(object sender, RoutedEventArgs e)
        {
            CryptoModel newCrypto = new CryptoModel();
            newCrypto = SignedInModel.Cryptos[0];
            AddItemToCart(newCrypto.Id);
            cartItemsLbl.Content = SignedInModel.CurrentCart.InCart.Split('/').Count();
        }

        private void addBtn2_Click(object sender, RoutedEventArgs e)
        {
            CryptoModel newCrypto = new CryptoModel();
            newCrypto = SignedInModel.Cryptos[1];
            AddItemToCart(newCrypto.Id);
            cartItemsLbl.Content = SignedInModel.CurrentCart.InCart.Split('/').Count();
        }
        private void addBtn3_Click(object sender, RoutedEventArgs e)
        {
            CryptoModel newCrypto = new CryptoModel();
            newCrypto = SignedInModel.Cryptos[2];
            AddItemToCart(newCrypto.Id);
            cartItemsLbl.Content = SignedInModel.CurrentCart.InCart.Split('/').Count();
        }
        private void addBtn4_Click(object sender, RoutedEventArgs e)
        {
            CryptoModel newCrypto = new CryptoModel();
            newCrypto = SignedInModel.Cryptos[3];
            AddItemToCart(newCrypto.Id);
            cartItemsLbl.Content = SignedInModel.CurrentCart.InCart.Split('/').Count();
        }
        private void addBtn5_Click(object sender, RoutedEventArgs e)
        {
            CryptoModel newCrypto = new CryptoModel();
            newCrypto = SignedInModel.Cryptos[4];
            AddItemToCart(newCrypto.Id);
            cartItemsLbl.Content = SignedInModel.CurrentCart.InCart.Split('/').Count();
        }
        private void addBtn6_Click(object sender, RoutedEventArgs e)
        {
            CryptoModel newCrypto = new CryptoModel();
            newCrypto = SignedInModel.Cryptos[5];
            AddItemToCart(newCrypto.Id);
            cartItemsLbl.Content = SignedInModel.CurrentCart.InCart.Split('/').Count();
        }
        private void addBtn7_Click(object sender, RoutedEventArgs e)
        {
            CryptoModel newCrypto = new CryptoModel();
            newCrypto = SignedInModel.Cryptos[6];
            AddItemToCart(newCrypto.Id);
            cartItemsLbl.Content = SignedInModel.CurrentCart.InCart.Split('/').Count();
        }
        private void addBtn8_Click(object sender, RoutedEventArgs e)
        {
            CryptoModel newCrypto = new CryptoModel();
            newCrypto = SignedInModel.Cryptos[7];
            AddItemToCart(newCrypto.Id);
            cartItemsLbl.Content = SignedInModel.CurrentCart.InCart.Split('/').Count();
        }
        private void addBtn9_Click(object sender, RoutedEventArgs e)
        {
            CryptoModel newCrypto = new CryptoModel();
            newCrypto = SignedInModel.Cryptos[8];
            AddItemToCart(newCrypto.Id);
            cartItemsLbl.Content = SignedInModel.CurrentCart.InCart.Split('/').Count();
        }

        private void removeBtn1_Click(object sender, RoutedEventArgs e)
        {
            var item = SignedInModel.CryptosInCart.SingleOrDefault(x => x.Name == (SignedInModel.Cryptos[0].Name));
            if (item != null)
            {
                SignedInModel.CryptosInCart.Remove(item);
            }
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }
        private void removeBtn2_Click(object sender, RoutedEventArgs e)
        {
            var item = SignedInModel.CryptosInCart.SingleOrDefault(x => x.Name == (SignedInModel.Cryptos[1].Name));
            if (item != null)
            {
                SignedInModel.CryptosInCart.Remove(item);
            }
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }
        private void removeBtn3_Click(object sender, RoutedEventArgs e)
        {
            var item = SignedInModel.CryptosInCart.SingleOrDefault(x => x.Name == (SignedInModel.Cryptos[2].Name));
            if (item != null)
            {
                SignedInModel.CryptosInCart.Remove(item);
            }
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }
        private void removeBtn4_Click(object sender, RoutedEventArgs e)
        {
            var item = SignedInModel.CryptosInCart.SingleOrDefault(x => x.Name == (SignedInModel.Cryptos[3].Name));
            if (item != null)
            {
                SignedInModel.CryptosInCart.Remove(item);
            }
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }
        private void removeBtn5_Click(object sender, RoutedEventArgs e)
        {
            var item = SignedInModel.CryptosInCart.SingleOrDefault(x => x.Name == (SignedInModel.Cryptos[4].Name));
            if (item != null)
            {
                SignedInModel.CryptosInCart.Remove(item);
            }
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }
        private void removeBtn6_Click(object sender, RoutedEventArgs e)
        {
            var item = SignedInModel.CryptosInCart.SingleOrDefault(x => x.Name == (SignedInModel.Cryptos[5].Name));
            if (item != null)
            {
                SignedInModel.CryptosInCart.Remove(item);
            }
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }
        private void removeBtn7_Click(object sender, RoutedEventArgs e)
        {
            var item = SignedInModel.CryptosInCart.SingleOrDefault(x => x.Name == (SignedInModel.Cryptos[6].Name));
            if (item != null)
            {
                SignedInModel.CryptosInCart.Remove(item);
            }
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }
        private void removeBtn8_Click(object sender, RoutedEventArgs e)
        {
            var item = SignedInModel.CryptosInCart.SingleOrDefault(x => x.Name == (SignedInModel.Cryptos[7].Name));
            if (item != null)
            {
                SignedInModel.CryptosInCart.Remove(item);
            }
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }
        private void removeBtn9_Click(object sender, RoutedEventArgs e)
        {
            var item = SignedInModel.CryptosInCart.SingleOrDefault(x => x.Name == (SignedInModel.Cryptos[8].Name));
            if (item != null)
            {
                SignedInModel.CryptosInCart.Remove(item);
            }
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }
    }
}
