using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Android.Content.Res;
using Android.Graphics;
using Android.Media;
using static Android.Graphics.BitmapFactory;

namespace BFSDeliveries.Droid.Helpers
{
    public class ImageHelper
    {
        public static byte[] ImageToBinary(string imagePath)
        {
            byte[] imageBytes;

            using (var originalImage = DecodeFile(imagePath)){
                var rotation = GetRotation(imagePath);
                var width = (originalImage.Width * 0.25);
                var height = (originalImage.Height * 0.25);
                Bitmap scaledImage = null;
                try {
                    scaledImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, true);

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
                }
                finally
                {
                    scaledImage.Dispose();
                }
            }

            // Dispose of the Java side bitmap.
            GC.Collect();

            return imageBytes;
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
    }
}
