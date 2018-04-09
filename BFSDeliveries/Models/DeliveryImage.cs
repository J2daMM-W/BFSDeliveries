using System;
using Xamarin.Forms;

namespace BFSDeliveries.Models
{
    public class Image
    {
        //public string Id { get; set; }
        //public string Name { get; set; }
        public string FileName { get; set; }
        //public ImageSource Photo { get; set; } //For displaying photo on a page
        public string ImageUrl { get; set; } //Location on the device where the taken photo is stored.

        public Image()
        {

        }

        public Image(string fileName, string imageUrl)
        {
            this.FileName = fileName;
            this.ImageUrl = imageUrl;
            //this.Photo = photo;
        }

    }
}
