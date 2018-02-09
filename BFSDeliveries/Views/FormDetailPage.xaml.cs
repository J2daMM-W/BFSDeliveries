using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BFSDeliveries.ViewModels;
using Xamarin.Forms;

namespace BFSDeliveries.Views
{
    public partial class FormDetailPage : ContentPage
    {
        private FormDetailViewModel formDetailViewModel;

        public FormDetailPage()
        {
            InitializeComponent();
            //Disable default navigation back button
            NavigationPage.SetHasBackButton(this, false);
        }

        public FormDetailPage(FormDetailViewModel formDetailViewModel)
        {
            this.formDetailViewModel = formDetailViewModel;
        }

        async void Submit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        //async void GetPhoto_Clicked(object sender, EventArgs e)
        //{
        //    var action = await DisplayActionSheet("Get Photo From:", "Cancel", null, "Camera","Photo Library");
        //  await Task.Delay(100);

        //    if ((action != null) && action.Equals("Camera"))
        //    {
        //        // TODO - send to camera
        //    }
        //    else if ((action != null) && action.Equals("Photo Library"))
        //    {
        //        // TODO - send to Photo Library
        //    }
        //    else if ((action != null) && action.Equals("Cancel"))
        //    {
        //        // TODO - Cancel Actionsheet selection
        //    }
        //}

    }
}
