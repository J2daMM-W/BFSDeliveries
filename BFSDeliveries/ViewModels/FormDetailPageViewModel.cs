using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using BFSDeliveries.Interfaces;
using BFSDeliveries.Models;
using Prism.Services;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace BFSDeliveries.ViewModels
{
    public class FormDetailPageViewModel
    {
        public Form form { get; set; }
        public Delivery delivery { get;  set;}
        public Models.DeliveryImage image;

        INavigationService _navigationService;

        public IPageDialogService _pageDialogService;
        public DelegateCommand GetPhotoCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }

        public FormDetailPageViewModel(IPageDialogService pageDialogService, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

            GetPhotoCommand = new DelegateCommand(DisplayActionSheetButtons);
            CancelCommand = new DelegateCommand(CancelFormSubmition);
            SubmitCommand = new DelegateCommand(ExecuteFormSubmition);
        }

        private async void DisplayActionSheetButtons()
        {
            var result = await _pageDialogService.DisplayActionSheetAsync("Get Photo From:", "Cancel", "Camera", "Photo Library");

            if(result == "Camera"){
                //send to camera
                //await Xamarin.Forms.DependencyService.Get<IMediaService>().OpenGallery();
            }
            else if(result == "Photo Library")
            {
                // send to photo lib
                await Xamarin.Forms.DependencyService.Get<IMediaService>().OpenGallery();
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
        //public Photo PhotoLib
        //{
        //    get
        //    {
        //        return this.photo;
        //    }
        //    set
        //    {
        //        if (Equals(value, this.photo))
        //        {
        //            return;
        //        }
        //        this.photo = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public FormDetailViewModel(IMediaService mediaService)
        //{
        //    //GetPhotoCommand = new Command(async () => await ExecuteGetPhotoCommand());
        //    //this.form = form;

        //    if(mediaService == null)
        //    {
        //        throw new ArgumentNullException(nameof(mediaService));
        //    }

        //    //check if the device has a camera or photos are supported - if so present alert to chose camera or photo library
        //    GetPhotoCommand = new Command(async () => await TakePictureAsync(), () => canExecuteGetPhotoCommand());
        //}

        //public bool canExecuteGetPhotoCommand()
        //{
        //    //if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsPickPhotoSupported)
        //    //{
        //    //  return false;
        //    //}
        //    //else
        //    //{
        //    return true;
        //    //}
        //}

        //private async Task TakePictureAsync()
        //{
        //    //await photoCapture.GetPhotosUsingCamera();
        //    var cameraResult = await mediaService.GetMultiplePhotos();

        //    //if (cameraResult != null)
        //    //{
        //    //    PhotoLib.Image = cameraResult.Image;
        //    //    PhotoLib.FilePath = cameraResult.FilePath;
        //    //}
        //}
        //async Task ExecuteGetPhotoCommand()
        //{
        //    //await DependencyService.Get<IMediaService>().OpenGallery();
        //    //DisplayActionSheetCommand = new Command() =>
        //    //{
                
        //    //}
        //}
    }
}
