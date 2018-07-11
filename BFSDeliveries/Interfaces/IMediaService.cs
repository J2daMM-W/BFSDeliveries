using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BFSDeliveries.Models;

namespace BFSDeliveries.Interfaces
{
    public interface IMediaService
    {
        //Task<Photo> GetPhotosUsingCamera();
        //Task PhotosUsingCamera();
        Task UseCamera();
        Task UsePhotoGalleryAsync();
        //void ClearFiles(List<string> filePaths);
        //void OpenGallery();
    }
}
