using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BFSDeliveries.Interfaces;
using BFSDeliveries.ViewModels;
using Xamarin.Forms;

namespace BFSDeliveries.Views
{
    public partial class FormDetailPage : ContentPage
    {
        //private FormDetailViewModel formDetailViewModel;
        //List<byte[]> _images;  //Store Bytes of image
        //readonly StackLayout _imageStack;

        public FormDetailPage()
        {
            InitializeComponent();

            //Disable default navigation back button
            NavigationPage.SetHasBackButton(this, false);

            //_imageStack = new StackLayout
            //{
            //    Orientation = StackOrientation.Horizontal
            //};

            //this.Content = _imageStack;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //_images = new List<byte[]>();

            ////Subscribe notification
            //MessagingCenter.Subscribe<App, List<byte[]>>((App)Xamarin.Forms.Application.Current, "ImagesSelected", (s, images) =>
            //{
            //    foreach (byte[] image in images)
            //    {
            //        Image newImage = new Image();
            //        newImage.Source = ImageSource.FromStream(() => new MemoryStream(image));
            //        _imageStack.Children.Add(newImage);  //Stacklayout that holds images
            //        _images.Add(image);
            //    }
            //});
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //MessagingCenter.Unsubscribe<App, List<string>>(this, "ImagesSelected");
        }

    }
}
