using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BFSDeliveries.Models;

namespace BFSDeliveries.Interfaces
{
    public interface IMediaService
    {
        Task<Photo> GetPhotosUsingCamera();
        //Task<List<Photo>> GetMultiplePhotos();
        //Task<List<Photo>> OpenGallery();
        void OpenGallery();
    }
}
