using cryptomaniaUI.Commands;
using cryptomaniaUI.Models;
using System;
using System.Collections.Generic;
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
        public ProfileView()
        {
            InitializeComponent();
        }

        private void Main_btn_Click(object sender, RoutedEventArgs e)
        {
            // Sign user out and return to main page
            SignedInModel.LoggedIn = false;
            Mediator.Notify("GoToHomeView", "");
        }
        public void GetCurrentUserWallet()
        {
            // Set usernameLbl as SignedInModel.Username
            // Check db to see if wallet address exists for current user based on SignedInModel.Username
            // If exists update walletAddyLbl 
            // Update datagrid with user's wallet details
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

        private void NewWallet_btn_Click(object sender, RoutedEventArgs e)
        {
            // Create new wallet address and update database record 
            // Change walletAddyLbl to new address 
        }

        private void Clear_btn_Click(object sender, RoutedEventArgs e)
        {
            // Clear crypto2SendAmountTBox and receiverWalletAddressTBox 
            crypto2SendAmountTBox.Text = String.Empty;
            receiverWalletAddressTBox.Text = String.Empty;
        }
    }
}
