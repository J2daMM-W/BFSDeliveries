using System.Threading.Tasks;

namespace BFSDeliveries.Interfaces
{
    public interface IMediaService
    {
        Task UseCameraAsync();
        Task UsePhotoGalleryAsync();
        //void ClearFiles(List<string> filePaths);
    }
}
