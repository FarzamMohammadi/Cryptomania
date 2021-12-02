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
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        private static Random random = new Random();
        public ProfileView()
        {
            InitializeComponent();
            // Sets username in UI
            usernameLbl.Content = SignedInModel.Username;
            // Checks for wallet, if it exists updates UI
            FindUserWallet();

        }

        private async void FindUserWallet()
        {
            ProfileDataGrid.Items.Clear();
            WalletModel wallet = WalletModel.CheckForUserWallet();
            List<CartDGModel> purchasedCrypto = new List<CartDGModel>();
            CartDGModel pruchase = null;

            if (wallet != null)
            {
                SignedInModel.CurrentWallet = wallet;
                walletAddyLbl.Content = wallet.WalletAddress;

                if (wallet.Purchases != "")
                {
                    string[] purchases = wallet.Purchases.Split('/');

                    for (int i = 1; i < purchases.Length; i++)
                    {
                        string[] cartItem = purchases[i].Split('-');
                        pruchase = new CartDGModel() { CurrencyName = cartItem[0], CurrencyQuantity = cartItem[1] };
                        // A cool one liner below ;)
                        purchasedCrypto.Add(pruchase);
                    }
                }
                foreach (CartDGModel datagridItem in purchasedCrypto)
                {
                    ProfileDataGrid.Items.Add(datagridItem);
                }
            }
            else
            {
                walletAddyLbl.Content = "-- NO ADDRESS --";
                WalletModel newWallet = new WalletModel() { Purchases = "", Username = SignedInModel.Username, WalletAddress = "" };
                SignedInModel.CurrentWallet = newWallet;
                bool success = await WalletModel.AddWalletAsync(newWallet);
                if (success)
                {
                    MessageBox.Show("First Login. New wallet created.");
                }
            }
        }

        private void Refresh_btn_Click(object sender, RoutedEventArgs e)
        {
            FindUserWallet();
        }

        private void Main_btn_Click(object sender, RoutedEventArgs e)
        {
            // Sign user out and return to main page
            SignedInModel.LoggedIn = false;
            Mediator.Notify("GoToHomeView", "");
        }
     
        private void Send_btn_Click(object sender, RoutedEventArgs e)
        {
            // Get values from datagri, crypto2SendAmountTBox and receiverWalletAddressTBox and send crypto from current user to that address
            // Update record in current user's wallet in database
            // Refresh current user datagrid values with same reusable GetCurrentUserWallet()
        }

        private void Purchase_btn_Click(object sender, RoutedEventArgs e)
        {
            Mediator.Notify("GoToCryptoView", "");
        }

        private void Cart_btn_Click(object sender, RoutedEventArgs e)
        {
            Mediator.Notify("GoToCartView", "");
        }

        private async void NewWallet_btn_Click(object sender, RoutedEventArgs e)
        {
            string newWalletAddress = GetNewRandomAddress();
            WalletModel walletToUpdate = SignedInModel.CurrentWallet;
            walletToUpdate.WalletAddress = newWalletAddress;

            bool success = await WalletModel.UpdateWallet(walletToUpdate);
            if (success)
            {
                walletAddyLbl.Content = newWalletAddress;
            }
        }



        public static string GetNewRandomAddress()
        {
            // Set ran string length
            int length = 18;
            // Get radnom string
            const string chars = "qwertyuiopasdfghjklzxcvbnmABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-/.,-=])(*&^%$#@";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void Clear_btn_Click(object sender, RoutedEventArgs e)
        {
            // Clear crypto2SendAmountTBox and receiverWalletAddressTBox 
            crypto2SendAmountTBox.Text = "";
            receiverWalletAddressTBox.Text = "";
        }
    }
}
