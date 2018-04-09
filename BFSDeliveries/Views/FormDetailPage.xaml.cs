using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BFSDeliveries.Interfaces;
using BFSDeliveries.ViewModels;
using Xamarin.Forms;

namespace BFSDeliveries.Views
{
    public partial class FormDetailPage : ContentPage
    {
        //private FormDetailViewModel formDetailViewModel;
        List<string> _images = new List<string>();


        public FormDetailPage()
        {
            InitializeComponent();

            //Disable default navigation back button
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //MessagingCenter.Subscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelected", (s, images) =>
            //{
            //    ImagesSelected.FlowItemsSource = images;
            //    _images = images;
            //});
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<App, List<string>>(this, "ImagesSelected");
        }

    }
}
