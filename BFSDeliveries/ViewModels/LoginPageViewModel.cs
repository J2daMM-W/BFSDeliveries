using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;

namespace BFSDeliveries.ViewModels
{
    public class LoginPageViewModel
    {
        public DelegateCommand LogInCommand { get; private set; }

        public LoginPageViewModel()
        {
            //LogInCommand = new DelegateCommand(LogInAsync);
        }

        //private async Task LogInAsync()
        //{
        //    //await INavigationService.NavigateAsync(path);
        //}
    }
}
