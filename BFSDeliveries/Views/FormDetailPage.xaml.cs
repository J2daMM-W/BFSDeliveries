
using Xamarin.Forms;

namespace BFSDeliveries.Views
{
    public partial class FormDetailPage : ContentPage
    {      
        public FormDetailPage()
        {
            InitializeComponent();

            //Disable default navigation back button
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

    }
}
