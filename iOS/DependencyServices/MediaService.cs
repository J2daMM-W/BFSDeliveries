using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BFSDeliveries.Interfaces;
using BFSDeliveries.iOS.DependencyServices;
using BFSDeliveries.iOS.Helpers;
using CoreGraphics;
using ELCImagePicker;
using Foundation;
using Plugin.Media;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(MediaService))]
namespace BFSDeliveries.iOS.DependencyServices
{
    public class MediaService : IMediaService
    {
        #region Fields
        //ObservableCollection<Photo> selectedPhotos { get; set; }
        private List<AssetResult> mResults = new List<AssetResult>();
        //var selectedImages = new IList();
        //public DeliveryImage cameraImage { get; set; }

        #endregion

        //public MediaService()
        //{
        //    IsCameraAvailable = UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera);
        //}

        //public bool IsCameraAvailable
        //{
        //    get;
        //    private set;
        //}

        //TaskCompletionSource<Stream> taskCompletionSource;
        //UIImagePickerController imagePicker;

        public async Task UseCameraAsync()
        {
            //cameraImage = new DeliveryImage();
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions { PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small });

            if (file == null)
                return;

            byte[] imageAsBytes = null;

            using(var memoryStream = new MemoryStream()){
                file.GetStream().CopyTo(memoryStream);
                file.Dispose();
                imageAsBytes = memoryStream.ToArray();
            }

            //Send images back
            MessagingCenter.Send<byte[]>(imageAsBytes, "CameraImage");

        }

        //private void GotAccessToCamera()
        //{
        //    //create image picker object
        //    var imagePickerC = new UIImagePickerController { SourceType = UIImagePickerControllerSourceType.Camera };

        //    //Find the top most view controller to launch the camera
        //    var window = UIApplication.SharedApplication.KeyWindow;
        //    var vc = window.RootViewController;
        //    while (vc.PresentedViewController != null)
        //    {
        //        vc = vc.PresentedViewController;
        //    }

        //    vc.PresentViewController(imagePickerC, true, null);

        //    //Callback method for when picture user has finished
        //    imagePickerC.FinishedPickingMedia += (sender, e) =>
        //    {
        //        //Grab the image
        //        var image = (UIImage)e.Info.ObjectForKey(new NSString("UIImagePickerControllerOriginalImage"));

        //        //we will need to rotate image based on it's orientation - pics are side ways
        //        image = RotateImage(image, image.Orientation);

        //        //adjust the amount of compression and convert to PNG
        //        var jpegImage = image.Scale(image.Size, 0.5f).AsPNG();

        //        //convert image to a byte array to be able to send to server via API
        //        //also use byte array to populate image view
        //        var myByteArray = new byte[jpegImage.Length];
        //        Marshal.Copy(jpegImage.Bytes, myByteArray, 0, Convert.ToInt32(jpegImage.Length));

        //        //Using messaging center to send byte array back up to the UI
        //        MessagingCenter.Send<byte[]>(myByteArray, "ImageSelected");

        //        //Dismiss the camera view controller on UI thread
        //        Device.BeginInvokeOnMainThread(() =>
        //        {
        //            vc.DismissViewController(true, null);
        //        });
        //    };

        //    //Callback method for when user cancels taking picture action
        //    imagePickerC.Canceled += (sender, e) => vc.DismissViewController(true, null);

        //}

        //Get photos from album/gallery
        public async Task UsePhotoGalleryAsync()
        {
            var picker = ELCImagePickerViewController.Create(15);
            picker.MaximumImagesCount = 15;

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
                        //To do
                    }
                    else
                    {
                        var images = new List<byte[]>();

                        //t.Result

                        foreach (var item in t.Result)
                        {
                            var path = ImageHelper.GetPathToImage(item.Image, item.Name);
                            var imageBytes = ImageHelper.ImageToBinary(path);
                            images.Add(imageBytes);
                            CleanPath(path);
                        }

                        //Send images back
                        MessagingCenter.Send((App)Xamarin.Forms.Application.Current, "ImagesSelected", images);
                    }
                });
            });
        }

        //string Save(UIImage image, string name)
        //{
        //    var documentsDirectory = Environment.GetFolderPath
        //                          (Environment.SpecialFolder.Personal);
        //    string jpgFilename = Path.Combine(documentsDirectory, name); // hardcoded filename, overwritten each time
        //    NSData imgData = image.AsJPEG();
        //    NSError err = null;
        //    if (imgData.Save(jpgFilename, false, out err))
        //    {
        //        return jpgFilename;
        //    }
        //    else
        //    {
        //        Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
        //        return null;
        //    }
        //}

        //Method that will take in a photo and rotate it based on the orientation that the image was taken in
        double radians(double degrees) { return degrees * Math.PI / 180; }

        private UIImage RotateImage(UIImage src, UIImageOrientation orientation)
        {
            UIGraphics.BeginImageContext(src.Size);

            if (orientation == UIImageOrientation.Right)
            {
                CGAffineTransform.MakeRotation((nfloat)radians(90));
            }
            else if (orientation == UIImageOrientation.Left)
            {
                CGAffineTransform.MakeRotation((nfloat)radians(-90));
            }
            else if (orientation == UIImageOrientation.Down)
            {
                // NOTHING
            }
            else if (orientation == UIImageOrientation.Up)
            {
                CGAffineTransform.MakeRotation((nfloat)radians(90));
            }

            src.Draw(new CGPoint(0, 0));
            UIImage image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return image;
        }

        //public Task<Photo> GetPhotosUsingCamera()
        //{
        //    var tcs = new TaskCompletionSource<Photo>();

        //    //create image picker object
        //    var imagePicker = new UIImagePickerController { SourceType = UIImagePickerControllerSourceType.Camera };

        //    //Find the top most view controller to launch the camera
        //    var window = UIApplication.SharedApplication.KeyWindow;
        //    var vc = window.RootViewController;
        //    while (vc.PresentedViewController != null)
        //    {
        //        vc = vc.PresentedViewController;
        //    }

        //    //show the image gallery
        //    vc.PresentViewController(imagePicker, true, null);


        //    //Callback method for when picture user has finished
        //    imagePicker.FinishedPickingMedia += async (sender, e) =>
        //    {
        //        //Grab the image
        //        UIImage image = (UIImage)e.Info.ObjectForKey(new NSString("UIImagePickerControllerOriginalImage"));

        //        //var photo = e.Info.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;

        //        //we will need to rotate image based on it's orientation - pics are side ways
        //        UIImage rotateImage = RotateImage(image, image.Orientation);

        //        //adjust the amount of compression
        //        rotateImage = rotateImage.Scale(new CGSize(rotateImage.Size.Width, rotateImage.Size.Height), 0.5f);

        //        var jpegImage = rotateImage.AsPNG();

        //        //get photo meta data 
        //        var meta = e.Info.ValueForKey(new NSString("UIImagePickerControllerMediaMetadata")) as NSDictionary;

        //        var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //        string jpgFilename = Path.Combine(documentsDirectory, Guid.NewGuid() + ".jpg");
        //        NSData imgData = rotateImage.AsJPEG();
        //        NSError err = null;

        //        //NSData imgData = aItem.Image.AsJPEG();
        //        var selImage = new StreamImagesourceHandler();

        //        ImageSource albumImage = ImageSource.FromStream(imgData.AsStream);
        //        UIImage cameraImage = await selImage.LoadImageAsync(albumImage);

        //        if (imgData.Save(jpgFilename, false, out err))
        //        {
        //            Photo result = new Photo();

        //            result.Image = cameraImage;
        //            result.Path = jpgFilename;

        //            try { tcs.TrySetResult(result); }
        //            catch (Exception exc) { tcs.SetException(exc); }
        //        }
        //        else
        //        {
        //            tcs.TrySetException(new Exception(err.LocalizedDescription));
        //        }

        //        //convert image to a byte array to be able to send to server via API
        //        //also use byte array to populate image view
        //        byte[] myByteArray = new byte[jpegImage.Length];
        //        System.Runtime.InteropServices.Marshal.Copy(jpegImage.Bytes, myByteArray, 0, Convert.ToInt32(jpegImage.Length));

        //        //Using messaging center to send byte array back up to the UI
        //        MessagingCenter.Send<byte[]>(myByteArray, "ImageSelected");

        //        //Dismiss the camera view controller on UI thread
        //        Device.BeginInvokeOnMainThread(() =>
        //        {
        //            vc.DismissViewController(true, null);
        //        });
        //    };

        //    //Callback method for when user cancels taking picture action
        //    imagePicker.Canceled += (sender, e) => vc.DismissViewController(true, null);

        //    return tcs.Task;
        //}

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
