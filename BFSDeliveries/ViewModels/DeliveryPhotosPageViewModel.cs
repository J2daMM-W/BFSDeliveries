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
        //public ObservableCollection<DeliveryImage> SelectedImages { get; set; } // Selected Images 
        public ObservableCollection<DeliveryImage> DeliveryImages { get; set; } // Selected Images to attach to delivery form
        public ObservableCollection<DeliveryOrder> SelectedOrders { get; set; } // Orders to be submitted with a given form

        #region Properties
        /// <summary>
        /// Gets or sets the DeliveryForm object.
        /// </summary>
        /// <value>deliveryForm</value>
        private DeliveryForm deliveryForm;
        public DeliveryForm DeliveryForm
        {
            get
            {
                return deliveryForm;
            }
            set
            {
                deliveryForm = value;
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
            //SelectedImages = new ObservableCollection<DeliveryImage>();
            DeliveryImages = new ObservableCollection<DeliveryImage>();
            SelectedOrders = new ObservableCollection<DeliveryOrder>();
            //Form = new DeliveryForm();

            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

            CancelCommand = new DelegateCommand(CancelFormSubmission);
            SubmitCommand = new DelegateCommand(async () => await ExecuteFormSubmission());

            GetPhotoCommand = new DelegateCommand(async () => await DisplayActionSheetButtons());

            async void executeMethod() => await SelectDeliveryOrders();
            GetSelectedOrdersCommand = new DelegateCommand(executeMethod);

            //Validator = new ValidationHelper();

            //Subscribe notification for both camera choice and  photo library choice - remember to UnSubscribe
            MessagingCenter.Subscribe<App, ObservableCollection<DeliveryImage>>((App)Application.Current, "SelectedImages", (s, SelectedImages) =>
            {
                 foreach (var selectedImage in SelectedImages)
                 {
                     DeliveryImages.Add(new DeliveryImage
                     {
                         ImagePath = selectedImage.ImagePath,
                         Source = selectedImage.Source,
                         OrgImage = selectedImage.OrgImage
                     });
                 }
             });

            //Subscribe notification for Selected Orders Pickticket Numbers - remember to UnSubscribe
            MessagingCenter.Subscribe<App, ObservableCollection<DeliveryOrder>>((App)Application.Current, "SelectedOrders", (s, SelectedOrders) =>
            {
                List<string> _selectedOrders = new List<string>();

                //update the Editor with selected orders
                foreach (var selectedOrder in SelectedOrders)
                {
                    _selectedOrders.Add(selectedOrder.PickTicketNumber);
                }

                this.SelectedOrders = SelectedOrders;
                PickTicketNumbers = string.Join(",", _selectedOrders);
            });

            this.DeliveryForm = new DeliveryForm();
        }

        async Task SelectDeliveryOrders()
        {
            string path = "DeliveryOrdersPage";

            await _navigationService.NavigateAsync(path);
        }

        async Task DisplayActionSheetButtons()
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

        async Task ExecuteFormSubmission()
        {
            var deliveryFormData = DeliveryForm;

            //update form with PickTicket List and SelectedImages List
            foreach (var selectedOrder in SelectedOrders)
            {
                deliveryFormData.PickTicketNumbers.Add(selectedOrder);
            }

            foreach (var selectedImage in DeliveryImages)
            {
                deliveryFormData.DeliveryImages.Add(selectedImage);
            }

            // To submit form there needs to be a verification since PickTickeNumber field can't be empty 
            //will show alert if verification failure


            //check if there is network if not send form to outbox.Will keep it there and when network is back active sent it.
            //Check whether the device is connected, and if so, whether the connection
            //is wifi or mobile (it could be something else).
            try
            {
                bool internetActive = Xamarin.Forms.DependencyService.Get<IDeviceState>().isNetworkReachable();


                if (internetActive)
                {

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            //check if delete attached photos has been selected 
            if (deliveryFormData.DeleteAttachedPhotos)
            {
                var imagePaths = new List<string>();
                // get photos to delete
                foreach (var photosToDelete in deliveryFormData.DeliveryImages)
                {
                    imagePaths.Add(photosToDelete.ImagePath);
                }

                //call delete function
                try
                {
                    Xamarin.Forms.DependencyService.Get<IFileManager>().DeleteFile(imagePaths);
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            }

            //call submit photos
            //var submitResult =


            await _navigationService.GoBackAsync();
        }

    }
}
