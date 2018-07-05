using System;
using BFSDeliveries;
using BFSDeliveries.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Editor), typeof(FormEditorRenderer))]
namespace BFSDeliveries.iOS
{
    public class FormEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.Layer.CornerRadius = 4;
                Control.Layer.BorderColor = Color.LightGray.ToCGColor();
                Control.Layer.BorderWidth = 1;
            }
        }
    }
}
