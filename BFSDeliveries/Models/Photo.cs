using System;
using Xamarin.Forms;

namespace BFSDeliveries.Models
{
    public class Photo
    {
        //public string Id { get; set; }
        //public string Name { get; set; }
        public string PhotoInfo { get; set; }
        public ImageSource Image { get; set; } //For displaying photo on a page
        public string FilePath { get; set; } //Location on the device where the taken photo is stored.

        public Photo()
        {

        }

        public Photo(string photo, string path, ImageSource image)
        {
            this.PhotoInfo = photo;
            this.FilePath = path;
            this.Image = image;
        }
    }
}
