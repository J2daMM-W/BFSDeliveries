using BFSDeliveries.Interfaces;
using BFSDeliveries.Models;
using Prism.Services;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using System.Threading.Tasks;
using MvvmValidation;
using Microsoft.AppCenter.Crashes;
using System;

namespace BFSDeliveries.ViewModels
{
    public class DeliveryPhotosPageViewModel : BaseViewModel
    {
        public ObservableCollection<DeliveryImage> SelectedImages { get; set; } // Selected Images 
        public ObservableCollection<DeliveryOrder> SelectedOrders { get; set; } // Orders to be submitted with a given form

        #region Properties
        /// <summary>
        /// Gets or sets the DeliveryForm object.
        /// </summary>
        /// <value>deliveryForm</value>
        private DeliveryForm form;
        public DeliveryForm Form 
        {
            get
            {
                return form;
            }
            set
            {
                form = value; 
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the PickTicketNumbers string to be displayed.
        /// </summary>
        /// <value>text.</value>
        string text;
        public string PickTicketNumbers
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        INavigationService _navigationService;
        IPageDialogService _pageDialogService;
        public DelegateCommand GetPhotoCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }
        public ICommand GetSelectedOrdersCommand { get; private set; }

        //protected ValidationHelper Validator { get; private set; }

        public DeliveryPhotosPageViewModel(IPageDialogService pageDialogService, INavigationService navigationService)
        {
            Title = "Delivery Photos";
            SelectedImages = new ObservableCollection<DeliveryImage>();
            SelectedOrders = new ObservableCollection<DeliveryOrder>();
            //Form = new DeliveryForm();

            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

            CancelCommand = new DelegateCommand(CancelFormSubmission);
            SubmitCommand = new DelegateCommand(ExecuteFormSubmission);

            GetPhotoCommand = new DelegateCommand(DisplayActionSheetButtons);

            async void executeMethod() => await SelectDeliveryOrders();
            GetSelectedOrdersCommand = new DelegateCommand(executeMethod);

            //Validator = new ValidationHelper();

            //Subscribe notification for camera choice - remember to UnSubscribe
            MessagingCenter.Subscribe<App, byte[]>((App)Application.Current, "CameraImage", (s, imageAsBytes) =>
            {
                var imageSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));

                SelectedImages.Add(new DeliveryImage { Source = imageSource, OrgImage = imageAsBytes });
            });

            //Subscribe notification for photo library choice - remember to UnSubscribe
            MessagingCenter.Subscribe<App,List<byte[]>>((App)Application.Current, "ImagesSelected", (s, images) =>
            {
                foreach (byte[] selectedImage in images)
                {
                    var newImage = ImageSource.FromStream(() => new MemoryStream(selectedImage));

                    SelectedImages.Add(new DeliveryImage { Source = newImage, OrgImage = selectedImage });
                }
            });

            //Subscribe notification for Selected Orders Pickticket Numbers - remember to UnSubscribe
            //MessagingCenter.Subscribe<App, string>((App)Application.Current, "SelectedOrders", (s, pickTicketNumbers) =>
            //{
            //    PickTicketNumbers = pickTicketNumbers;
            //});

            MessagingCenter.Subscribe<App, ObservableCollection<DeliveryOrder>>((App)Application.Current, "SelectedOrders", (s, SelectedOrders) =>
            {
                List<string> _selectedOrders = new List<string>();

                //update the Editor with selected orders
                foreach(var selectedOrder in SelectedOrders)
                {
                    _selectedOrders.Add(selectedOrder.PickTicketNumber);
                }

                this.SelectedOrders = SelectedOrders;
                PickTicketNumbers = string.Join(",", _selectedOrders);
            });

            this.Form = new DeliveryForm();
        }

        async Task SelectDeliveryOrders()
        {
            string path = "DeliveryOrdersPage";

            await _navigationService.NavigateAsync(path);
        }

        async void DisplayActionSheetButtons()
        {
            var result = await _pageDialogService.DisplayActionSheetAsync("Get Photo From:", "Cancel", "Camera", "Photo Library");

            if (result == "Camera")
            {
                //send to camera
                await Xamarin.Forms.DependencyService.Get<IMediaService>().UseCameraAsync();
            }
            else if (result == "Photo Library")
            {
                // send to photo lib
                await Xamarin.Forms.DependencyService.Get<IMediaService>().UsePhotoGalleryAsync();
            }

            //Debug.WriteLine(result);
        }

        void CancelFormSubmission()
        {
            _navigationService.GoBackAsync();
        }

        void ExecuteFormSubmission()
        {
            var deliveryData = Form;

            //update form with PickTicket List and SelectedImages List
            foreach (var selectedOrder in SelectedOrders)
            {
                deliveryData.PickTicketNumbers.Add(selectedOrder);
            }

            foreach(var selectedImage in SelectedImages)
            {
                deliveryData.SelectedImages.Add(selectedImage);
            }

            // Do form submit after verification PickTickeNumber field can't be empty 
            //will show alert if verification failure


            //check if there is network if not send for to outbox.


            //check if delete attched photos has been selected 
            if(deliveryData.DeleteAttachedPhotos)
            {
                var imageSource = new List<ImageSource>();
                // get photos to delete
                foreach(var photosToDelete in deliveryData.SelectedImages)
                {
                    imageSource.Add(photosToDelete.Source);
                }
                //call delete function
                try
                {
                    Xamarin.Forms.DependencyService.Get<IFileManager>().DeleteFile(imageSource);
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);   
                }

            }

            _navigationService.GoBackAsync();
        }

    }
}
