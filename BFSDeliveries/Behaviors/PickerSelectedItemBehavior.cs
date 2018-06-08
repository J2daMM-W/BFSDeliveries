using System;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Android.Widget;
using BFSDeliveries;
using Xamarin.Forms;

namespace BFSDeliveries
{
    public class PickerSelectedItemBehavior : BehaviorBase<ItemsPicker>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ItemsPicker), null);

        public static readonly BindableProperty InputConverterProperty =
            BindableProperty.Create(nameof(InputConverter), typeof(IValueConverter), typeof(ItemsPicker), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public IValueConverter InputConverter
        {
            get { return (IValueConverter)GetValue(InputConverterProperty); }
            set { SetValue(InputConverterProperty, value); }
        }

        public new ItemsPicker AssociatedObject { get; private set; }

        protected override void OnAttachedTo(ItemsPicker bindable)
        {
            AssociatedObject = bindable;
            //bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.SelectedIndexChanged += Bindable_SelectedIndexChanged;
        }

        protected override void OnDetachingFrom(ItemsPicker bindable)
        {
            bindable.SelectedIndexChanged -= Bindable_SelectedIndexChanged;
            AssociatedObject = null;
        }

        void Bindable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Command == null)
            {
                return;
            }

            object parameter = InputConverter.Convert(e, typeof(object), null, null);
            if (Command.CanExecute(parameter))
            {
                Command.Execute(parameter);
            }


            // Bound and cast to a picker
            //if (!(sender is ItemsPicker bindable))
            //    return;

            //string value = string.Empty;
            //// Make sure the picker is data bound 
            //if (bindable.SelectedItem != null)
            //{
            //    // Get value of the selected item to be added to Editor
            //    var prop = bindable.SelectedItem.GetType().GetRuntimeProperties()
            //                          .FirstOrDefault(p => string.Equals(p.Name,
            //                                                             bindable.DisplayProperty, StringComparison.OrdinalIgnoreCase));

            //    if (prop != null)
            //    {
            //        value = prop.GetValue(bindable.SelectedItem).ToString();
            //    }

            //}
        }
    }
}
