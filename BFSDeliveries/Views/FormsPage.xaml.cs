using System;
using System.Collections.Generic;
using BFSDeliveries.Models;
using BFSDeliveries.ViewModels;
using Xamarin.Forms;

namespace BFSDeliveries.Views
{
    public partial class FormsPage : ContentPage
    {
        FormsPageViewModel viewModel;

        public FormsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new FormsPageViewModel();
        }

        async void OnFormSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var form = args.SelectedItem as Form;
            if (form == null)
                return;

            await Navigation.PushAsync(new FormDetailPage());

            // Manually deselect item
            FormsListView.SelectedItem = null;
        }

        //async void SelectForm_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new FormDetailPage());

        //    // Manually deselect item
        //    FormsListView.SelectedItem = null;
        //}
    }
}
