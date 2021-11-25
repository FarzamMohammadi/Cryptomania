using cryptomaniaUI.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace cryptomaniaUI.ViewModels
{
    class HomeViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand _goToLogin;
        private ICommand _goToSignUp;
        public ICommand GoToLogin
        {
            get
            {
                return _goToLogin ?? (_goToLogin = new RelayCommand(x =>
                {
                    Mediator.Notify("GoToLoginView", "");
                }));
            }
        }
        public ICommand GoToSignUp
        {
            get
            {
                return _goToSignUp ?? (_goToSignUp = new RelayCommand(x =>
                {
                    Mediator.Notify("GoToHomeView", "");
                }));
            }
        }
    }
}
