using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using BFSDeliveries.Interfaces;
using BFSDeliveries.iOS.DependencyServices;
using BFSDeliveries.Models;
using CoreGraphics;
using ELCImagePicker;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(MediaService))]
namespace BFSDeliveries.iOS.DependencyServices
{
    public class MediaService : IMediaService
    {
        #region Fields

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

        //public Task<Photo> TakePictureAsync()
        //{
        //  var tcs = new TaskCompletionSource<Photo>();

        //  Camera.TakePicture(UIApplication.SharedApplication.KeyWindow.RootViewController, (imagePickerResult) =>
        //  {

        //      if (imagePickerResult == null)
        //      {
        //          tcs.TrySetResult(null);
        //          return;
        //      }

        //      var photo = imagePickerResult.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;

        //      // You can get photo meta data with using the following
        //      // var meta = obj.ValueForKey(new NSString("UIImagePickerControllerMediaMetadata")) as NSDictionary;

        //      var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //      string jpgFilename = Path.Combine(documentsDirectory, Guid.NewGuid() + ".jpg");
        //      NSData imgData = photo.AsJPEG();
        //      NSError err = null;

        //      if (imgData.Save(jpgFilename, false, out err))
        //      {
        //          CameraResult result = new CameraResult();
        //          result.Picture = ImageSource.FromStream(imgData.AsStream);
        //          result.FilePath = jpgFilename;

        //          tcs.TrySetResult(result);
        //      }
        //      else
        //      {
        //          tcs.TrySetException(new Exception(err.LocalizedDescription));
        //      }
        //  });

        //  return tcs.Task;
        //}

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
        //    imagePicker.FinishedPickingMedia += (sender, e) =>
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

        //        if (imgData.Save(jpgFilename, false, out err))
        //        {
        //            Photo result = new Photo();

        //            result.Image = ImageSource.FromStream(imgData.AsStream);
        //            result.FilePath = jpgFilename;

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

        //public Task<List<Photo>> GetMultiplePhotos()
        //{
        //    var tcs = new TaskCompletionSource<List<Photo>>();

        //    List<Photo> result = new List<Photo>();

        //    //create a new instance of the picker view controller
        //    var picker = ELCImagePickerViewController.Create(15);

        //    try
        //    {
        //        //setup the handling of completion once the items have been picked or the picker has been cancelled
        //        picker.Completion.ContinueWith(t =>
        //        {
        //            //execute any UI code on the UI thread
        //            picker.BeginInvokeOnMainThread(() =>
        //            {
        //                //dismiss the picker
        //                picker.DismissViewController(true, null);

        //                if (t.IsCanceled || t.Exception != null)
        //                {
        //                    //cancelled or error
        //                }
        //                else
        //                {
        //                    //get the selected items
        //                    var items = t.Result as List<AssetResult>;

        //                    foreach (AssetResult aItem in items)
        //                    {
        //                        mResults.Add(aItem);
        //                        NSData imgData = aItem.Image.AsJPEG();
        //                        ImageSource albumImage = ImageSource.FromStream(imgData.AsStream);

        //                        result.Add(new Photo(aItem.Name, aItem.Path, albumImage));
        //                    }

        //                    //Task.FromResult(mResults);
        //                    //TableView.ReloadData();

        //                }
        //            });
        //        });

        //        var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
        //        while (topController.PresentedViewController != null)
        //        {
        //            topController = topController.PresentedViewController;
        //        }

        //        topController.PresentViewController(picker, true, null);

        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e.Message);
        //    }

        //    //result.AddRange(mResults);

        //    //foreach (AssetResult aItem in mResults)
        //    //{
        //    //  result.Add(new Photo(aItem.Name, aItem.Path, ImageSource.FromStream(aItem.Image)));
        //    //}

        //    //Return Task Object
        //    try { tcs.TrySetResult(result); }
        //    catch (Exception exc) { tcs.SetException(exc); }
        //    return tcs.Task;
        //}

        //private void GotAccessToCamera()
        //{
        //  //create image picker object
        //  var imagePickerC = new UIImagePickerController { SourceType = UIImagePickerControllerSourceType.Camera };

        //  //Find the top most view controller to launch the camera
        //  var window = UIApplication.SharedApplication.KeyWindow;
        //  var vc = window.RootViewController;
        //  while (vc.PresentedViewController != null)
        //  {
        //      vc = vc.PresentedViewController;
        //  }

        //  vc.PresentViewController(imagePickerC, true, null);

        //  //Callback method for when picture user has finished
        //  imagePickerC.FinishedPickingMedia += (sender, e) =>
        //  {
        //      //Grab the image
        //      UIImage image = (UIImage)e.Info.ObjectForKey(new NSString("UIImagePickerControllerOriginalImage"));

        //      //we will need to rotate image based on it's orientation - pics are side ways
        //      UIImage rotateImage = RotateImage(image, image.Orientation);

        //      //adjust the amount of compression
        //      rotateImage = rotateImage.Scale(new CGSize(rotateImage.Size.Width, rotateImage.Size.Height), 0.5f);

        //      var jpegImage = rotateImage.AsPNG();

        //      //convert image to a byte array to be able to send to server via API
        //      //also use byte array to populate image view
        //      byte[] myByteArray = new byte[jpegImage.Length];
        //      System.Runtime.InteropServices.Marshal.Copy(jpegImage.Bytes, myByteArray, 0, Convert.ToInt32(jpegImage.Length));

        //      //Using messaging center to send byte array back up to the UI
        //      MessagingCenter.Send<byte[]>(myByteArray, "ImageSelected");

        //      //Dismiss the camera view controller on UI thread
        //      Device.BeginInvokeOnMainThread(() =>
        //      {
        //          vc.DismissViewController(true, null);
        //      });
        //  };

        //  //Callback method for when user cancels taking picture action
        //  imagePickerC.Canceled += (sender, e) => vc.DismissViewController(true, null);

        //}

        public async Task OpenGallery()
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
                    }
                    else
                    {
                        List<string> images = new List<string>();

                        var items = t.Result as List<AssetResult>;
                        foreach (var item in items)
                        {
                            var path = Save(item.Image, item.Name);
                            images.Add(path);
                            //CleanPath(path);
                        }

                        MessagingCenter.Send<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelected", images);
                    }
                });
            });
        }

        string Save(UIImage image, string name)
        {
            var documentsDirectory = Environment.GetFolderPath
                                  (Environment.SpecialFolder.Personal);
            string jpgFilename = Path.Combine(documentsDirectory, name); // hardcoded filename, overwritten each time
            NSData imgData = image.AsJPEG();
            NSError err = null;
            if (imgData.Save(jpgFilename, false, out err))
            {
                return jpgFilename;
            }
            else
            {
                Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
                return null;
            }
        }

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
    }
}
