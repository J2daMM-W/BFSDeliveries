using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using Prism.Ioc;
using Prism;
using System.Collections.Generic;
using Android.Database;
using Xamarin.Forms;
using BFSDeliveries.Droid.Helpers;
using Android.Provider;
using Plugin.Permissions;
using Microsoft.AppCenter.Crashes;
using Plugin.CurrentActivity;
using Xamarin.Forms.Platform.Android;
using System.Collections.ObjectModel;
using BFSDeliveries.Models;
using System.IO;

namespace BFSDeliveries.Droid
{
    [Activity(Label = "BFSDeliveries", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true,
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FormsAppCompatActivity
    {
        public static int OPENGALLERYCODE = 200;  //Used to determine which service is being called - photoselection

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Forms.Init(this, savedInstanceState);
            LoadApplication(new App(new AndroidInitializer()));

            CrossCurrentActivity.Current.Init(this, savedInstanceState);
        }

        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            ObservableCollection<DeliveryImage> SelectedImages; // Selected Images 
            base.OnActivityResult(requestCode, resultCode, data);

            //request_code is the calling function identity, from where it is requested, result_code is the called function identifier, 
            //also it specifies status of the called message as Intent.ACTIVITY_OK and so on
            if (requestCode == OPENGALLERYCODE && resultCode == Result.Ok && data != null)
            {
                //var images = new List<byte[]>();
                SelectedImages = new ObservableCollection<DeliveryImage>();

                var clipData = data.ClipData;
                if (clipData != null) //
                {
                    for (int i = 0; i < clipData.ItemCount; i++)
                    {
                        var item = clipData.GetItemAt(i);

                        if (TryGetRealPathFromURI(this, item.Uri, out string path))
                        {
                            //TODO: make this it's own function since camera action also needs to do this
                            var imageBytes = ImageHelper.ImageToBinary(path);
                            var newImage = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                            SelectedImages.Add(new DeliveryImage { ImagePath = path, Source = newImage, OrgImage = imageBytes });
                            //CleanPath(path);
                        }
                    }
                }
                else if (TryGetRealPathFromURI(this, data.Data, out string path))
                {
                    //images.Add(ImageHelper.ImageToBinary(path));
                    //TODO: make this it's own function since camera action also needs to do this
                    var imageBytes = ImageHelper.ImageToBinary(path);
                    var newImage = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                    SelectedImages.Add(new DeliveryImage { ImagePath = path, Source = newImage, OrgImage = imageBytes });
                    //CleanPath(path);
                }

                //MessagingCenter.Send<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelected", images);
                //Send images back
                MessagingCenter.Send((App)Xamarin.Forms.Application.Current, "SelectedImages", SelectedImages);
            }
        }

        public bool TryGetRealPathFromURI(Context context, Android.Net.Uri contentURI, out string path)
        {
            //Context context;

            if (context != null)
            {
                //context = MainApplication.CurrentContext;

                try
                {
                    ICursor imageCursor = null;
                    string fullPathToImage = "";

                    imageCursor = ContentResolver.Query(contentURI, null, null, null, null);
                    imageCursor.MoveToFirst();
                    int idx = imageCursor.GetColumnIndex(MediaStore.Images.ImageColumns.Data);

                    if (idx != -1)
                    {
                        //This works for older devices
                        fullPathToImage = imageCursor.GetString(idx);
                    }
                    else
                    {
                        //This works for newer devices
                        ICursor cursor = null;
                        var docID = DocumentsContract.GetDocumentId(contentURI);
                        var id = docID.Split(':')[1];
                        var whereSelect = MediaStore.Images.ImageColumns.Id + "=?";
                        var projections = new string[] { MediaStore.Images.ImageColumns.Data };

                        // Try internal storage first
                        cursor = ContentResolver.Query(MediaStore.Images.Media.InternalContentUri, projections, whereSelect, new string[] { id }, null);
                        if (cursor.Count == 0)
                        {
                            // not found on internal storage, try external storage
                            cursor = ContentResolver.Query(MediaStore.Images.Media.ExternalContentUri, projections, whereSelect, new string[] { id }, null);
                        }
                        var colData = cursor.GetColumnIndexOrThrow(MediaStore.Images.ImageColumns.Data);
                        cursor.MoveToFirst();
                        fullPathToImage = cursor.GetString(colData);
                    }
                    path = fullPathToImage;
                    return true;
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    Toast.MakeText(context, "Unable to get path", ToastLength.Long).Show();
                }
            }
            path = null;
            return false;
        }

        private void CleanPath(string file)
        {
            var documentsDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            if (Directory.Exists(documentsDirectory))
            {
                File.Delete(file);
            }
        }
    }
}
