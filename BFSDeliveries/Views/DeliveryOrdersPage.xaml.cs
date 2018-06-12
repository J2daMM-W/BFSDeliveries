using System;
using System.Collections.Generic;
using BFSDeliveries.ViewModels;
using Xamarin.Forms;

namespace BFSDeliveries.Views
{
    public partial class DeliveryOrdersPage : ContentPage
    {
        DeliveryOrdersViewModel viewModel;

        public DeliveryOrdersPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new DeliveryOrdersViewModel();
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
