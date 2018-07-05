using System;
using BFSDeliveries.Models;
using Xamarin.Forms;

namespace BFSDeliveries
{
    public class DeliveryForm : BindableObject
    {
        #region PicketTicketNumbers
        public static readonly BindableProperty PickTicketNumbersProperty =
            BindableProperty.Create(nameof(PickTicketNumbers), typeof(string), typeof(DeliveryForm), string.Empty,
                                    propertyChanged: OnPickTicketNumbersChanged);

        public string PickTicketNumbers
        {
            get { return (string)GetValue(PickTicketNumbersProperty); }
            set { SetValue(PickTicketNumbersProperty, value); }
        }

        private static void OnPickTicketNumbersChanged(BindableObject bindable, object oldValue, object newValue)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region DeleteAttachedPhotos
        public static BindableProperty DeleteAttachedPhotosProperty =
            BindableProperty.Create(nameof(DeleteAttachedPhotos), typeof(bool), typeof(DeliveryForm), default(bool),
                                    propertyChanged: OnDeleteAttachedPhotos, defaultBindingMode: BindingMode.TwoWay);

        public bool DeleteAttachedPhotos
        {
            get { return (bool)GetValue(DeleteAttachedPhotosProperty); }
            set { SetValue(DeleteAttachedPhotosProperty, value); }
        }

        private static void OnDeleteAttachedPhotos(BindableObject bindable, object oldValue, object newValue)
        {
            bool deleteAttachedPhotos = (bool)newValue;

        }
        #endregion

        #region SelectedImages
        public static BindableProperty SelectedImagesProperty =
            BindableProperty.Create(nameof(SelectedImages), typeof(DeliveryImage), typeof(DeliveryForm));

        public DeliveryImage SelectedImages
        {
            get { return (DeliveryImage)GetValue(SelectedImagesProperty); }
            set { SetValue(SelectedImagesProperty, value); }
        }
        #endregion
    }
}
