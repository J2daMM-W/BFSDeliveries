using System;
using System.IO;
using Android.Content.Res;
using Android.Graphics;
using Android.Media;
using static Android.Graphics.BitmapFactory;

namespace BFSDeliveries.Droid.Helpers
{
    public class ImageHelper
    {
        //public static string SaveFile(string collectionName, byte[] imageByte, string fileName)
        //{
        //    var fileDir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), collectionName);
        //    if (!fileDir.Exists())
        //    {
        //        fileDir.Mkdirs();
        //    }

        //    var file = new Java.IO.File(fileDir, fileName);
        //    System.IO.File.WriteAllBytes(file.Path, imageByte);

        //    return file.Path;

        //}

        public static byte[] ImageToBinary(string imagePath)
        {
            byte[] imageBytes;

            var originalImage = BitmapFactory.DecodeFile(imagePath);

            //var thumbNail = decodeSampledBitmapFromResource(GetResources(), R.id.myimage, 100, 100);
            var rotation = GetRotation(imagePath);
            //var width = (originalImage.Width * LocalStorage.ImageQualityPercent);
            //var height = (originalImage.Height * LocalStorage.ImageQualityPercent);
            var width = (originalImage.Width);
            var height = (originalImage.Height);
            var scaledImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, true);

            if (rotation != 0)
            {
                var matrix = new Matrix();
                matrix.PostRotate(rotation);
                scaledImage = Bitmap.CreateBitmap(scaledImage, 0, 0, scaledImage.Width, scaledImage.Height, matrix, true);
            }

            using (var ms = new MemoryStream())
            {
                scaledImage.Compress(Bitmap.CompressFormat.Jpeg, 90, ms);
                imageBytes = ms.ToArray();
            }

            originalImage.Recycle();
            scaledImage.Recycle();
            originalImage.Dispose();
            scaledImage.Dispose();
            // Dispose of the Java side bitmap.
            GC.Collect();

            return imageBytes;
        }

        private static Resources GetResources()
        {
            throw new NotImplementedException();
        }

        private static int GetRotation(string filePath)
        {
            using (var ei = new ExifInterface(filePath))
            {
                var orientation = (Android.Media.Orientation)ei.GetAttributeInt(ExifInterface.TagOrientation, (int)Android.Media.Orientation.Normal);

                switch (orientation)
                {
                    case Android.Media.Orientation.Rotate90:
                        return 90;
                    case Android.Media.Orientation.Rotate180:
                        return 180;
                    case Android.Media.Orientation.Rotate270:
                        return 270;
                    default:
                        return 0;
                }
            }
        }

        public static int calculateInSampleSize(
            BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Raw height and width of image
            int height = options.OutHeight;
            int width = options.OutWidth;
            int inSampleSize = 1;

            if (height > reqHeight || width > reqWidth)
            {

                int halfHeight = height / 2;
                int halfWidth = width / 2;

                // Calculate the largest inSampleSize value that is a power of 2 and keeps both
                // height and width larger than the requested height and width.
                while ((halfHeight / inSampleSize) >= reqHeight
                        && (halfWidth / inSampleSize) >= reqWidth)
                {
                    inSampleSize *= 2;
                }
            }

            return inSampleSize;
        }

        public static Bitmap decodeSampledBitmapFromResource(Resources res, int resId,
        int reqWidth, int reqHeight)
        {

            // First decode with inJustDecodeBounds=true to check dimensions
            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InJustDecodeBounds = true;
            BitmapFactory.DecodeResource(res, resId, options);

            // Calculate inSampleSize
            options.InSampleSize = calculateInSampleSize(options, reqWidth, reqHeight);

            // Decode bitmap with inSampleSize set
            options.InJustDecodeBounds = false;
            return BitmapFactory.DecodeResource(res, resId, options);
        }
    }
}
