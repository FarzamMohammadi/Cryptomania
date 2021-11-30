using cryptomaniaUI.Commands;
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
using System.Net.Http;
using cryptomaniaUI.Models;

namespace cryptomaniaUI.Views
{
    /// <summary>
    /// Interaction logic for SignUpView.xaml
    /// </summary>
    public partial class SignUpView : UserControl
    {
        private static readonly HttpClient client = new HttpClient();
        public SignUpView()
        {
            InitializeComponent();
        }
        private void Login_btn_Click(object sender, RoutedEventArgs e)
        {
            Mediator.Notify("GoToLoginView", "");
        }

        private void Main_btn_Click(object sender, RoutedEventArgs e)
        {
            Mediator.Notify("GoToHomeView", "");
        }

        private void Clear_btn_Click(object sender, RoutedEventArgs e)
        {
            FNameTBox.Text = "";
            LNameTBox.Text = "";
            emailTBox.Text = "";
            passTBox.Clear();
            rePassTBox.Clear();
            
        }

        private void Register_btn_Click(object sender, RoutedEventArgs e)
        {
            string fName = FNameTBox.Text;
            string lName = LNameTBox.Text;
            string email = emailTBox.Text;
            string password = passTBox.Password;
            string passwordRe = passTBox.Password;

            if (fName != "" && lName != "" && email != "" && password != null & passwordRe != null)
            {
                if (password == passwordRe)
                {
                    UserModel newUser = new UserModel { FirstName = fName, LastName = lName, Username = email, Password = password };
                    UserModel.AddUserAsync(newUser);
                }
                else
                {
                    MessageBox.Show("Please fill out the fields.");
                }
            }
            else
            {
                MessageBox.Show("Please fill out the fields.");
            }

        }
    }
}
