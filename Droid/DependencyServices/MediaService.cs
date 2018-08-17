using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Widget;
using BFSDeliveries.Droid.Activities;
using BFSDeliveries.Droid.DependencyServices;
using BFSDeliveries.Interfaces;
using Microsoft.AppCenter.Crashes;
using Plugin.CurrentActivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

[assembly: Dependency(typeof(MediaService))]
namespace BFSDeliveries.Droid.DependencyServices
{
    public class MediaService: Java.Lang.Object, IMediaService
    {
        public async Task UseCameraAsync()
        {
            //Context context;
            //context = Android.App.Application.Context; 

            //try to use current activity else application context
            var permissionContext = CrossCurrentActivity.Current.Activity ?? Android.App.Application.Context;

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {
                        Toast.MakeText(permissionContext,"Need Camera permission to access your camera", ToastLength.Long).Show();
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera });
                    status = results[Permission.Camera];
                }

                if (status == PermissionStatus.Granted)
                {

                    await CrossMedia.Current.Initialize();

                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        Toast.MakeText(permissionContext,"No Camera Available", ToastLength.Long).Show();
                    }

                    var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium,

                    });

                    if (file == null)
                        return;

                    byte[] imageAsBytes = null;

                    using (var memoryStream = new MemoryStream())
                    {
                        file.GetStream().CopyTo(memoryStream);
                        file.Dispose();
                        imageAsBytes = memoryStream.ToArray();
                    }

                    //PhotoIDImage.Source = ImageSource.FromFile(file.Path);
                    //Send images back
                    MessagingCenter.Send<byte[]>(imageAsBytes, "CameraImage");
                }
                else if (status != PermissionStatus.Unknown)
                {
                    Toast.MakeText(permissionContext,"Camera Denied, Can not continue, try again.", ToastLength.Long).Show();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                Toast.MakeText(permissionContext, "Error , Camera Not Available", ToastLength.Long).Show();
            }
        }

        public async Task UsePhotoGalleryAsync()
        {
            //Context context;
            //context = Android.App.Application.Context;

            //try to use current activity else application context
            var permissionContext = CrossCurrentActivity.Current.Activity ?? Android.App.Application.Context;

            if (permissionContext != null)
            {
                //context = MainApplication.CurrentContext;

                try
                {
                    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                    if (status != PermissionStatus.Granted)
                    {
                        if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                        {
                            Toast.MakeText(permissionContext, "Need Storage permission to access to your photos.", ToastLength.Long).Show();
                        }

                        var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                        status = results[Permission.Storage];
                    }

                    if (status == PermissionStatus.Granted)
                    {
                        Toast.MakeText(permissionContext, "Select max 15 images", ToastLength.Long).Show();
                        var imageIntent = new Intent(
                            Intent.ActionPick);
                        imageIntent.SetType("image/*");
                        imageIntent.PutExtra(Intent.ExtraAllowMultiple, true);
                        imageIntent.SetAction(Intent.ActionGetContent);
                        ((Activity)permissionContext).StartActivityForResult(
                            Intent.CreateChooser(imageIntent, "Select photo"), MainActivity.OPENGALLERYCODE);
                    }
                    else if (status != PermissionStatus.Unknown)
                    {
                        Toast.MakeText(permissionContext, "Permission Denied. Can not continue, try again.", ToastLength.Long).Show();
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    Toast.MakeText(permissionContext, "Error. Can not continue, try again.", ToastLength.Long).Show();
                }
            }
        }


        //void ClearFiles(List<string> filePaths)
        //{
        //    foreach (var p in filePaths)
        //    {
        //        if (File.Exists(p))
        //        {
        //            File.Delete(p);
        //        }
        //    }
        //}

    }
}
