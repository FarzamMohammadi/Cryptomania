using cryptomaniaUI.Commands;
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

        private void addBtn1_Click(object sender, RoutedEventArgs e)
        {
            CryptoModel newCrypto = new CryptoModel();
            newCrypto= SignedInModel.Cryptos[0];
            SignedInModel.CryptosInCart.Add(newCrypto);
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }

        private void addBtn2_Click(object sender, RoutedEventArgs e)
        {
            SignedInModel.CryptosInCart.Add(SignedInModel.Cryptos[1]);
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }
        private void addBtn3_Click(object sender, RoutedEventArgs e)
        {
            SignedInModel.CryptosInCart.Add(SignedInModel.Cryptos[2]);
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }
        private void addBtn4_Click(object sender, RoutedEventArgs e)
        {
            SignedInModel.CryptosInCart.Add(SignedInModel.Cryptos[3]);
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }
        private void addBtn5_Click(object sender, RoutedEventArgs e)
        {
            SignedInModel.CryptosInCart.Add(SignedInModel.Cryptos[4]);
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }
        private void addBtn6_Click(object sender, RoutedEventArgs e)
        {
            SignedInModel.CryptosInCart.Add(SignedInModel.Cryptos[5]);
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }
        private void addBtn7_Click(object sender, RoutedEventArgs e)
        {
            SignedInModel.CryptosInCart.Add(SignedInModel.Cryptos[6]);
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }
        private void addBtn8_Click(object sender, RoutedEventArgs e)
        {
            SignedInModel.CryptosInCart.Add(SignedInModel.Cryptos[7]);
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
        }
        private void addBtn9_Click(object sender, RoutedEventArgs e)
        {
            SignedInModel.CryptosInCart.Add(SignedInModel.Cryptos[8]);
            cartItemsLbl.Content = SignedInModel.CryptosInCart.Count.ToString();
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
