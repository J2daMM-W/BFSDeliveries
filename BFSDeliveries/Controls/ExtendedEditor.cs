using System;
using System.Collections;
using Xamarin.Forms;

namespace BFSDeliveries
{
    public class ExtendedEditor : CustomEditor
    {
        public static readonly BindableProperty SelectedOrdersProperty =
            BindableProperty.Create(nameof(SelectedOrders), typeof(IEnumerable), typeof(ExtendedEditor), default(IEnumerable),
                                    BindingMode.Default, null, propertyChanged: OnItemsSourceChanged);

        public IEnumerable SelectedOrders
        {
            get { return (IList)GetValue(SelectedOrdersProperty); }
            set { SetValue(SelectedOrdersProperty, value); }
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            throw new NotImplementedException();
        }
    }
}
