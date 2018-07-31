using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BFSDeliveries.Interfaces
{
    public interface IFileManager
    {
        void DeleteFile(List<string> imagePaths);
    }
}
