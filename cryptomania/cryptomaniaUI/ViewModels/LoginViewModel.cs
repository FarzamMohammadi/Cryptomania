using cryptomaniaUI.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace cryptomaniaUI.ViewModels
{
    class LoginViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand _goToSignUp;
        private ICommand _goToMain;


        public ICommand GoToSignUp
        {
            get
            {
                return _goToSignUp ?? (_goToSignUp = new RelayCommand(x =>
                {
                    Mediator.Notify("GoToSignUpView", "");
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
