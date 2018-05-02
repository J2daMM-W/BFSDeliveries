using System;
using System.IO;
using CoreGraphics;
using Foundation;
using UIKit;

namespace BFSDeliveries.iOS.Helpers
{
    public static class ImageHelper
    {
        public static byte[] ImageToBinary(string imagePath)
        {
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, (int)fileStream.Length);
            fileStream.Close();
            return buffer;
        }

        public static string GetPathToImage(UIImage image, string name)
        {

            var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string jpgFilename = System.IO.Path.Combine(documentsDirectory, name); // hardcoded filename, overwritten each time

            //calculate new size - user can determine the quality of images they want  
            //var width = (image.CGImage.Width * LocalStorage.ImageQualityPercent);
            //var height = (image.CGImage.Height * LocalStorage.ImageQualityPercent);
            var width = (image.CGImage.Width);
            var height = (image.CGImage.Height);

            //begin resizing image
            UIImage resizedImage = ResizeImageWithAspectRatio(image, width, height);
            NSData imgData = resizedImage.AsJPEG();
            NSError err = null;
            if (imgData.Save(jpgFilename, false, out err))
            {
                return jpgFilename;
            }
            else
            {
                return null;
            }
        }

        private static UIImage ResizeImageWithAspectRatio(UIImage sourceImage, float maxWidth, float maxHeight)
        {
            var sourceSize = sourceImage.Size;
            var maxResizeFactor = Math.Max(maxWidth / sourceSize.Width, maxHeight / sourceSize.Height);
            if (maxResizeFactor > 1) return sourceImage;
            var width = maxResizeFactor * sourceSize.Width;
            var height = maxResizeFactor * sourceSize.Height;
            UIGraphics.BeginImageContext(new CGSize(width, height));
            sourceImage.Draw(new CGRect(0, 0, width, height));
            var resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return resultImage;
        }
    }
}
