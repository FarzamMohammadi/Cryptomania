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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }
        private void SignUp_btn_Click(object sender, RoutedEventArgs e)
        {
            Mediator.Notify("GoToSignUpView", "");
        }

        private void Main_btn_Click(object sender, RoutedEventArgs e)
        {
            Mediator.Notify("GoToHomeView", "");
        }

    }
}
