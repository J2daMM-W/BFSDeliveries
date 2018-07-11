using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Widget;
using BFSDeliveries.Droid.DependencyServices;
using BFSDeliveries.Interfaces;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(MediaService))]
namespace BFSDeliveries.Droid.DependencyServices
{
    public class MediaService: Java.Lang.Object, IMediaService
    {
        public Task UseCamera()
        {
            throw new NotImplementedException();
        }

        public async Task UsePhotoGalleryAsync()
        {
            Context context;

            if (MainApplication.CurrentContext != null)
            {
                context = MainApplication.CurrentContext;

                try
                {
                    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage);
                    if (status != PermissionStatus.Granted)
                    {
                        if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Storage))
                        {
                            Toast.MakeText(context, "Need Storage permission to access to your photos.", ToastLength.Long).Show();
                        }

                        var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Plugin.Permissions.Abstractions.Permission.Storage });
                        status = results[Plugin.Permissions.Abstractions.Permission.Storage];
                    }

                    if (status == PermissionStatus.Granted)
                    {
                        Toast.MakeText(context, "Select max 15 images", ToastLength.Long).Show();
                        var imageIntent = new Intent(
                            Intent.ActionPick);
                        imageIntent.SetType("image/*");
                        imageIntent.PutExtra(Intent.ExtraAllowMultiple, true);
                        imageIntent.SetAction(Intent.ActionGetContent);
                        ((Activity)context).StartActivityForResult(
                            Intent.CreateChooser(imageIntent, "Select photo"), MainActivity.OPENGALLERYCODE);

                    }
                    else if (status != PermissionStatus.Unknown)
                    {
                        Toast.MakeText(context, "Permission Denied. Can not continue, try again.", ToastLength.Long).Show();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Toast.MakeText(context, "Error. Can not continue, try again.", ToastLength.Long).Show();
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
