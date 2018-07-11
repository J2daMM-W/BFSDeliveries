using Android.Content;
using BFSDeliveries;
using BFSDeliveries.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImagesScrollView), typeof(ImagesScrollViewRenderer))]
namespace BFSDeliveries.Droid
{
    public class ImagesScrollViewRenderer: ScrollViewRenderer
    {
        public ImagesScrollViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var element = e.NewElement as ImagesScrollView;
            element?.Render();
        }
    }
}
