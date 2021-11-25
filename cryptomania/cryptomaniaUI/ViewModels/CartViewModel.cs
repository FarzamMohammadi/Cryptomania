using cryptomaniaUI.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace cryptomaniaUI.ViewModels
{
    class CartViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand _goToLogin;
        private ICommand _goToMain;
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
        public ICommand GoToMain
        {
            get
            {
                return _goToMain ?? (_goToMain = new RelayCommand(x =>
                {
                    Mediator.Notify("GoToHomeView", "");
                }));
            }
        }
    }
}
