using System;

using Xamarin.Forms;

namespace BFSDeliveries.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string userNameEntry = userName.Text;
            string passwordEntry = userPassword.Text;

            var mainPage = new MainPage();

            //username and password can't be empty 
            if ((!string.IsNullOrEmpty(userNameEntry)) || (!string.IsNullOrEmpty(passwordEntry)))
            {
                // TODO:
                // On Successfull login:
                //Navigation.InsertPageBefore(new TabbedPage(), this);
                await Navigation.PushModalAsync(mainPage);
            }
            else
            {
                // TODO: Prompt for entry - for now can be empty for testing 
                //messageLabel.Text = "Username/Password is Empty.";
                //Navigation.InsertPageBefore(new TabbedPage(), this);
                await Navigation.PushModalAsync(mainPage);
            }
        }
    }
}
