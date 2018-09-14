using System;
using System.Collections.Generic;
using System.IO;
using BFSDeliveries.Interfaces;
using BFSDeliveries.iOS.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(FileManager))]
namespace BFSDeliveries.iOS.DependencyServices
{
    public class FileManager : IFileManager
    {
        public void DeleteFile(List<string> imagePaths)
        {
            var docsDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            if(Directory.Exists(docsDir))
            {
                foreach (var imagePath in imagePaths)
                {
                    File.Delete(imagePath);
                }
            }
        }
    }
}
