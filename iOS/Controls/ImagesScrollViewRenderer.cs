using System;
using BFSDeliveries.Controls;
using BFSDeliveries.iOS.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImagesScrollView), typeof(ImagesScrollViewRenderer))]
namespace BFSDeliveries.iOS.Controls
{
    public class ImagesScrollViewRenderer: ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var element = e.NewElement as ImagesScrollView;
            element?.Render();
        }
    }
}
