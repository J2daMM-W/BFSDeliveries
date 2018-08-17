using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using BFSDeliveries.Interfaces;
using BFSDeliveries.iOS.DependencyServices;
using BFSDeliveries.iOS.Helpers;
using BFSDeliveries.Models;
using ELCImagePicker;
using Plugin.Media;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(MediaService))]
namespace BFSDeliveries.iOS.DependencyServices
{
    public class MediaService : IMediaService
    {
        #region Fields
        public ObservableCollection<DeliveryImage> SelectedImages { get; set; } // Selected Images 
        private List<AssetResult> mResults = new List<AssetResult>();
        #endregion

        public MediaService()
        {
            IsCameraAvailable = UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera);
        }

        public bool IsCameraAvailable
        {
            get;
            private set;
        }

        //TaskCompletionSource<Stream> taskCompletionSource;
        //UIImagePickerController imagePicker;

        public async Task UseCameraAsync()
        {
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions 
                { PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small });

            if (file == null)
                return;

            // TODO: Add custom renderer and logic for camera to take and attach more than one pic

            //byte[] imageAsBytes = null;
            SelectedImages = new ObservableCollection<DeliveryImage>();

            using(var memoryStream = new MemoryStream()){
                file.GetStream().CopyTo(memoryStream);
                var path = file.Path;

                //TODO: make this it's own function since gallery action will also needs to do this
                var imageBytes = ImageHelper.ImageToBinary(path);
                var newImage = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                SelectedImages.Add(new DeliveryImage { ImagePath = path, Source = newImage, OrgImage = imageBytes });
                CleanPath(path);
            }

            //Send images back
            MessagingCenter.Send((App)Xamarin.Forms.Application.Current, "SelectedImages", SelectedImages);

        }

        //Get photos from album/gallery
        public async Task UsePhotoGalleryAsync()
        {
            var picker = ELCImagePickerViewController.Create(15);
            SelectedImages = new ObservableCollection<DeliveryImage>();

            picker.MaximumImagesCount = 15; 
            // TODO: Have an alert if picked pictures are more that picker.MaximumImagesCount

            var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            while (topController.PresentedViewController != null)
            {
                topController = topController.PresentedViewController;
            }
            topController.PresentViewController(picker, true, null);

            await picker.Completion.ContinueWith(t =>
            {
                picker.BeginInvokeOnMainThread(() =>
                {
                    //dismiss the picker
                    picker.DismissViewController(true, null);

                    if (t.IsCanceled || t.Exception != null)
                    {
                        // User canceled selection action
                        return;
                    }
                    else
                    {
                        foreach (var item in t.Result)
                        {
                            var path = ImageHelper.GetPathToImage(item.Image, item.Name);
                            //TODO: make this it's own function since camera action also needs to do this

                            var imageBytes = ImageHelper.ImageToBinary(path);
                            var newImage = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                            SelectedImages.Add(new DeliveryImage { ImagePath = path, Source = newImage, OrgImage = imageBytes });
                            CleanPath(path);
                        }

                        //Send images back
                        MessagingCenter.Send((App)Xamarin.Forms.Application.Current, "SelectedImages", SelectedImages);
                    }
                });
            });
        }

        //Method that will take in a photo and rotate it based on the orientation that the image was taken in
        double radians(double degrees) { return degrees * Math.PI / 180; }

        private void CleanPath(string file)
        {
            var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            if (Directory.Exists(documentsDirectory))
            {
                File.Delete(file);
            }
        }


    }
}
