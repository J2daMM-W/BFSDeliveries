using System.Collections.Generic;
using System.IO;
using BFSDeliveries.Droid.DependencyServices;
using BFSDeliveries.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(FileManager))]
namespace BFSDeliveries.Droid.DependencyServices
{
    public class FileManager: IFileManager
    {
        public void DeleteFile(List<string> imagePaths)
        {
            foreach (var imagePath in imagePaths)
            {
                File.Delete(imagePath);
            }

        }
    }
}
