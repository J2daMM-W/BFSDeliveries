using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BFSDeliveries.Views
{
    public partial class DeliveryPhotosPage : ContentPage
    {
        public DeliveryPhotosPage()
        {
            InitializeComponent();

            //Disable default navigation back button
            NavigationPage.SetHasBackButton(this, false);
        }
    }
}
