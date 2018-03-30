using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using BFSDeliveries.Interfaces;
using BFSDeliveries.Models;
using Prism.Services;
using Xamarin.Forms;

namespace BFSDeliveries.ViewModels
{
    public class FormDetailViewModel
    {
        public Form form { get; set; }
        public Delivery delivery { get;  set;}
        public Photo photo;

        public IPageDialogService _pageDialogService;
        public DelegateCommand GetPhotoCommand { get; private set; }

        public FormDetailViewModel(IPageDialogService pageDialogService){
            
            _pageDialogService = pageDialogService;

            GetPhotoCommand = new DelegateCommand(DisplayActionSheetButtons);
        }

        private async void DisplayActionSheetButtons(object obj)
        {
            var result = await _pageDialogService.DisplayActionSheetAsync("Get Photo From:", "Cancel", "Destroy", "Camera", "Photo Library");
            Debug.WriteLine(result);
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
