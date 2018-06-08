using System;
using Xamarin.Forms;

namespace BFSDeliveries
{
    public class SelectableCell : ViewCell
    {
        public SelectableCell()
        {
            var cellWrapper = new Grid
            {
                Padding = 10,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1,GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
                }
            };

            //var cb = new CheckBox();
            //cellWrapper.Children.Add(cb, 0, 0);
            var sw = new Switch();
            sw.SetBinding(Switch.IsToggledProperty, "IsSelected");

            View = cellWrapper;
        }
    }
}
