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

namespace BFSDeliveries.ViewModels
{
    public class DeliveryPhotosPageViewModel : BaseViewModel
    {
        public Form form { get; set; }
        public DeliveryForm delivery { get; set; }
        public ObservableCollection<DeliveryImage> SelectedImages { get; set; } // Selected Images 
        public ObservableCollection<DeliveryOrder> SelectedOrders { get; set; } // Orders to be submitted with a given form

        string text;
        public string PickTicketNumbers
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged("PickTicketNumbers");
                }
            }
        }

        INavigationService _navigationService;
        IPageDialogService _pageDialogService;
        public DelegateCommand GetPhotoCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }
        public ICommand GetSelectedOrdersCommand { get; private set; }

        private bool _deletePhotosSelected;

        public bool DeleteAttachedPhotos
        {
            get { return _deletePhotosSelected;  }
            set 
            {
                _deletePhotosSelected = value;
                OnPropertyChanged("DeleteAttachedPhotos");
            }
        }

        protected ValidationHelper Validator { get; private set; }

        public DeliveryPhotosPageViewModel(IPageDialogService pageDialogService, INavigationService navigationService)
        {
            Title = "Delivery Photos";
            SelectedImages = new ObservableCollection<DeliveryImage>();
            SelectedOrders = new ObservableCollection<DeliveryOrder>();

            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

            CancelCommand = new DelegateCommand(CancelFormSubmission);
            SubmitCommand = new DelegateCommand(ExecuteFormSubmission);

            GetPhotoCommand = new DelegateCommand(DisplayActionSheetButtons);

            async void executeMethod() => await SelectDeliveryOrders();
            GetSelectedOrdersCommand = new DelegateCommand(executeMethod);

            Validator = new ValidationHelper();

            //Subscribe notification for camera choice - remember to UnSubscribe
            MessagingCenter.Subscribe<App, byte[]>((App)Application.Current, "CameraImage", (s, imageAsBytes) =>
            {
                var imageSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));

                SelectedImages.Add(new DeliveryImage { Source = imageSource, OrgImage = imageAsBytes });
            });

            //Subscribe notification for photo library choice - remember to UnSubscribe
            MessagingCenter.Subscribe<App, List<byte[]>>((App)Application.Current, "ImagesSelected", (s, images) =>
            {
                foreach (byte[] selectedImage in images)
                {
                    var newImage = ImageSource.FromStream(() => new MemoryStream(selectedImage));

                    SelectedImages.Add(new DeliveryImage { Source = newImage, OrgImage = selectedImage });
                }
            });

            //Subscribe notification for Selected Orders Pickticket Numbers - remember to UnSubscribe
            MessagingCenter.Subscribe<App, string>((App)Application.Current, "SelectedOrders", (s, pickTicketNumbers) =>
            {
                PickTicketNumbers = pickTicketNumbers;
            });
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
                await Xamarin.Forms.DependencyService.Get<IMediaService>().UseCamera();
            }
            else if (result == "Photo Library")
            {
                // send to photo lib
                Xamarin.Forms.DependencyService.Get<IMediaService>().UsePhotoGallery();
            }

            //Debug.WriteLine(result);
        }

        void CancelFormSubmission()
        {
            _navigationService.GoBackAsync();
        }

        void ExecuteFormSubmission()
        {
            // Do form submit after verification - will show alert if verification failure

            //check if delete attched photos has been selected 
            if(DeleteAttachedPhotos)
            {
                // get photos to delete
                //var photosToDelete; 

                //call delete function
                //Xamarin.Forms.DependencyService.Get<IFileManager>().DeleteFile();
                
            }

            _navigationService.GoBackAsync();
        }

    }
}
