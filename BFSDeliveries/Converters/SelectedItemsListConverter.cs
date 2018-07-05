using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BFSDeliveries.Models;
using Xamarin.Forms;

namespace BFSDeliveries
{
    public class SelectedItemsListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var modelList = value as IEnumerable<DeliveryOrder>;
            if (modelList != null)
            {
                return string.Join(", ", modelList.Select(m => m.PickTicketNumber));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
