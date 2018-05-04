using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using BFSDeliveries.Interfaces;
using BFSDeliveries.Models;
using Prism.Services;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using BFSDeliveries.Controls;

using Xamarin.Forms;
using System.Collections.Generic;
using System.IO;

namespace BFSDeliveries.ViewModels
{
    public class FormDetailPageViewModel : BaseViewModel
    {
        public Form form { get; set; }
        public Delivery delivery { get;  set;}
        public Models.DeliveryImage image;
        public ObservableCollection<DeliveryImage> Items { get; set; }

        //List<byte[]> _images;  //Store Bytes of image
        //readonly StackLayout _imageStack;
        //ObservableCollection<DeliveryImage> deliveryImages = new ObservableCollection<DeliveryImage>();


        INavigationService _navigationService;

        public IPageDialogService _pageDialogService;
        public DelegateCommand GetPhotoCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }

        //public ObservableCollection<DeliveryImage> Images
        //{
        //    get { return deliveryImages; }
        //}

        public FormDetailPageViewModel(IPageDialogService pageDialogService, INavigationService navigationService)
        {
            //_imageStack = new StackLayout
            //{
            //    Orientation = StackOrientation.Horizontal
            //};
            Items = new ObservableCollection<DeliveryImage>();

            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

            CancelCommand = new DelegateCommand(CancelFormSubmition);
            SubmitCommand = new DelegateCommand(ExecuteFormSubmition);

            GetPhotoCommand = new DelegateCommand(DisplayActionSheetButtons);


            //Subscribe notification for camera choice
            MessagingCenter.Subscribe<App, byte[]>((App)Xamarin.Forms.Application.Current, "CameraImage", (s, imageAsBytes) =>
            {
                var imageSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));

                Items.Add(new DeliveryImage { Source = imageSource, OrgImage = imageAsBytes });
            });

            //Subscribe notification for photo library choice
            MessagingCenter.Subscribe<App, List<byte[]>>((App)Xamarin.Forms.Application.Current, "ImagesSelected", (s, images) =>
            {
                foreach (byte[] selectedImage in images)
                {
                    //Image newImage = new Image();
                    var newImage = ImageSource.FromStream(() => new MemoryStream(selectedImage));

                    //_imageStack.Children.Add(newImage);  //Stacklayout that holds images
                    //_images.Add(selectedImage);

                    Items.Add(new DeliveryImage { Source = newImage, OrgImage = selectedImage });
                }
            });
        }

        private async void DisplayActionSheetButtons()
        {
            var result = await _pageDialogService.DisplayActionSheetAsync("Get Photo From:", "Cancel", "Camera", "Photo Library");

            if(result == "Camera"){
                //send to camera
                await Xamarin.Forms.DependencyService.Get<IMediaService>().UseCamera();
            }
            else if(result == "Photo Library")
            {
                // send to photo lib
                Xamarin.Forms.DependencyService.Get<IMediaService>().UsePhotoGallery();
            }


            Debug.WriteLine(result);
        }

        private void CancelFormSubmition()
        {
            _navigationService.GoBackAsync();
        }

        private void ExecuteFormSubmition()
        {
            // Do form submit after verification 
            // otherwise cancel
            _navigationService.GoBackAsync();
        }

    }
}
