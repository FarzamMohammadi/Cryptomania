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
    /// Interaction logic for CartView.xaml
    /// </summary>
    public partial class CartView : UserControl
    {
        public CartView()
        {
            InitializeComponent();
            CartModel.CheckCart();
            SetDataGridValues();
        }

        private void SetDataGridValues()
        {
            List<CartDGModel> datagridItems = new List<CartDGModel>();
            if (SignedInModel.CurrentCart.InCart != null)
            {
                string[] items = SignedInModel.CurrentCart.InCart.Split('/');
                for (int i = 1; i < items.Length; i++)
                {
                    string[] cartItem = items[i].Split('-');
                    CartDGModel pendingCartItem = new CartDGModel() { CurrencyName = cartItem[0], CurrencyQuantity = cartItem[1] };
                    datagridItems.Add(pendingCartItem);
                }

                foreach (CartDGModel datagridItem in datagridItems)
                {
                    CartDataGrid.Items.Add(datagridItem);
                }
            }
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            Mediator.Notify("GoToProfileView", "");
            SaveCart();

        }
        public async void SaveCart()
        {
            _ = await CartModel.AddCurrentCart();
        }
        private void ClearCart_btn_Click(object sender, RoutedEventArgs e)
        {
            CartDataGrid.Items.Clear();
            SignedInModel.CurrentCart.InCart = "";
            SaveCart();
            SetDataGridValues();
        }

        public async void SubmitPurchase()
        {
            //GetAndAddPreviousPurchases();
            _=await WalletModel.UpdateWallet(SignedInModel.CurrentWallet);
        }

        private void GetAndAddPreviousPurchases()
        {
            List<CartDGModel> listToReturn = new List<CartDGModel>();
            CartDGModel purchase = new CartDGModel();
            string pruchaseToReturn = "";

            if (SignedInModel.CurrentCart.InCart != "")
            {
                if (SignedInModel.CurrentWallet.Purchases != "")
                {
                    string[] cartItems = SignedInModel.CurrentCart.InCart.Split('/');
                    // Split currency name from qty 
                    for (int i = 1; i < cartItems.Length; i++)
                    {
                        string[] cartItem = cartItems[i].Split('-');
                        purchase = new CartDGModel() { CurrencyName = cartItem[0], CurrencyQuantity = cartItem[1] };
                        listToReturn.Add(purchase);
                    }

                    string[] walletItems = SignedInModel.CurrentCart.InCart.Split('/');
                    // Split currency name from qty 
                    for (int i = 1; i < walletItems.Length; i++)
                    {
                        string[] walletItem = walletItems[i].Split('-');
                        purchase = new CartDGModel() { CurrencyName = walletItem[0], CurrencyQuantity = walletItem[1] };
                        listToReturn.Add(purchase);
                    }
                }
                foreach(CartDGModel item in listToReturn)
                {
                    pruchaseToReturn = pruchaseToReturn + "/" + item.CurrencyName + "-" + item.CurrencyQuantity;
                }
                SignedInModel.CurrentWallet.Purchases = pruchaseToReturn;
            }
        }

        private void Buy_btn_Click(object sender, RoutedEventArgs e)
        {
            SignedInModel.CurrentWallet.Purchases = SignedInModel.CurrentCart.InCart;
            SignedInModel.CurrentCart.InCart = "";
            SubmitPurchase();
            SaveCart();
            Mediator.Notify("GoToProfileView", "");
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            int location = CartDataGrid.Items.IndexOf(CartDataGrid.SelectedItem);
            if (CartDataGrid.SelectedItem != null)
            {
                CartDGModel selectedItem = (CartDGModel)CartDataGrid.SelectedItem;
                selectedItem.CurrencyQuantity = (int.Parse(selectedItem.CurrencyQuantity) + 1).ToString();

                CartDataGrid.Items.RemoveAt(location);
                CartDataGrid.Items.Add(selectedItem);
            }
        }
        private void Subtract_btn_Click(object sender, RoutedEventArgs e)
        {
            int location = CartDataGrid.Items.IndexOf(CartDataGrid.SelectedItem);
            if (CartDataGrid.SelectedItem != null)
            {
                CartDGModel selectedItem = (CartDGModel)CartDataGrid.SelectedItem;
                selectedItem.CurrencyQuantity = (int.Parse(selectedItem.CurrencyQuantity) - 1).ToString();

                CartDataGrid.Items.RemoveAt(location);
                if (int.Parse(selectedItem.CurrencyQuantity) >= 1)
                {
                    CartDataGrid.Items.Add(selectedItem);
                }
                
            }
        }
    }
}
