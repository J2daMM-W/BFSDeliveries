using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BFSDeliveries.Models;
using BFSDeliveries.ViewModels;
using Xamarin.Forms;

namespace BFSDeliveries.Views
{
    public partial class FormsPage : ContentPage
    {
        public FormsPage()
        {
            InitializeComponent();
        }

        //async void OnFormSelected(object sender, SelectedItemChangedEventArgs args)
        //{
        //    var form = args.SelectedItem as Form;
        //    if (form == null)
        //        return;

        //    await Navigation.PushAsync(new DeliveryPhotosPage());

        //    // Manually deselect item
        //    //FormsListView.SelectedItem = null;
        //}
    }
}
