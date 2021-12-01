using cryptomaniaUI.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace cryptomaniaUI.ViewModels
{
    class ProfileViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand _goToMain;
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
