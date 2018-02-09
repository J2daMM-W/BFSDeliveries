using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BFSDeliveries.Models;

namespace BFSDeliveries.Interfaces
{
    public interface IPhotoCapture
    {
        Task<Photo> GetPhotosUsingCamera();
        Task<List<Photo>> GetMultiplePhotos();
    }
}
