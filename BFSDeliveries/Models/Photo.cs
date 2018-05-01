using System;
using UIKit;
using Xamarin.Forms;

namespace BFSDeliveries.Models
{
    public class Photo
    {
        public string Name { get; set; } 

        public UIImage Image { get; set; } 

        public string Path { get; set; } 

        //
        // Constructors
        //
        public Photo()
        {

        }

        public Photo(string name, string path, UIImage image)
        {
            this.Name = name;
            this.Path = path;
            this.Image = image;
        }
    }
}
