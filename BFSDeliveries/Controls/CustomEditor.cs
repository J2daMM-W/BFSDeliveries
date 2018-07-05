using System;
using Xamarin.Forms;

namespace BFSDeliveries
{
    public class CustomEditor : Editor
    {
        public static BindableProperty EditorTextProperty =
            BindableProperty.Create(nameof(EditorText), typeof(string), typeof(CustomEditor), null,
                                    propertyChanged: EditorTextPropertyChanged);

        public string EditorText
        {
            get
            {
                return (string)GetValue(EditorTextProperty);
            }
            set
            {
                SetValue(EditorTextProperty, value);
            }
        }

        private static void EditorTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var editor = (CustomEditor)bindable;
            var oldText = oldValue;
            var newText = newValue;

            //if (oldValue != newValue)
            //{
            //    editor.Render();
            //}
        }

        //public void Render()
        //{
        //    if (Text == null)
        //        return;
            
        //    var element = new CustomEditor();

        //}
    }
}
