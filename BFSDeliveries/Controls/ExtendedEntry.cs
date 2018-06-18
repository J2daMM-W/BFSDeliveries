using System;
using System.Collections;
using Xamarin.Forms;

namespace BFSDeliveries
{
    public class ExtendedEntry : Entry
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(ExtendedEntry), default(IEnumerable),
                                    BindingMode.Default, null, propertyChanged: OnItemsSourceChanged);

        public IEnumerable ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            throw new NotImplementedException();
        }
    }
}
