using System;
using System.IO;
using System.Runtime.CompilerServices;
using BFSDeliveries.Interfaces;
using BFSDeliveries.iOS.DependencyServices;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(FileManager))]
namespace BFSDeliveries.iOS.DependencyServices
{
    public class FileManager : IFileManager
    {
        public void DeleteFile(string source)
        {
            File.Delete(source);
        }
    }
}
