using System;
using System.Globalization;
using Xamarin.Forms;

namespace BFSDeliveries
{
    public class SelectedIndexChangedEventArgsToSelectedIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value ;
            return eventArgs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
