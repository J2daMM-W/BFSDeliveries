using System;
using System.Collections.Generic;
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
    }
}
