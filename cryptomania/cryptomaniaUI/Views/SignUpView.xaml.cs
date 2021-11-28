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

namespace cryptomaniaUI.Views
{
    /// <summary>
    /// Interaction logic for SignUpView.xaml
    /// </summary>
    public partial class SignUpView : UserControl
    {
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

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //clear text fields
            //passTBox, repassTBox, FNameTBox, LNameTBox, emailTBox
            FNameTBox.Text = String.Empty;
            LNameTBox.Text = String.Empty;
            emailTBox.Text = String.Empty;
            passTBox.Clear();
            rePassTBox.Clear();
            
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            //add and add user to the database

        }
    }
}
