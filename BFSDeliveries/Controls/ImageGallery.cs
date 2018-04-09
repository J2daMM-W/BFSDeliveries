using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace BFSDeliveries.Controls
{
    public class ImageGallery : ScrollView
    {
        readonly StackLayout _imageStack;

        public ImageGallery()
        {
            this.Orientation = ScrollOrientation.Horizontal;

            _imageStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            this.Content = _imageStack;
        }

        public new IList<View> Children
        {
            get
            {
                return _imageStack.Children;
            }
        }

        //public static readonly BindableProperty ItemsSourceProperty = 
            //BindableProperty.Create<ImageGallery,IList>
    }
}
