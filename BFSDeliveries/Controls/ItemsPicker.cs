using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace BFSDeliveries.Controls
{
    public class ItemsPicker : Picker
    {
        public ItemsPicker()
        {
            base.SelectedIndexChanged += OnSelectedIndexChanged;
        }

        #region Fields

        //Bindable property for the items source
        public static readonly new BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(ItemsPicker), null,
                                    BindingMode.OneWay, null, new BindableProperty.BindingPropertyChangedDelegate(OnItemsSourceChanged),
                                    null, null, null);

        //Bindable property for the selected item
        public static readonly new BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(ItemsPicker), null,
                                    BindingMode.TwoWay, null, new BindableProperty.BindingPropertyChangedDelegate(OnSelectedItemChanged),
                                    null, null, null);

        //Bindable property for the display property
        public static readonly BindableProperty DisplayPropertyProperty =
            BindableProperty.Create(nameof(DisplayProperty), typeof(string), typeof(ItemsPicker), null,
                                    BindingMode.TwoWay, null, new BindableProperty.BindingPropertyChangedDelegate(OnDisplayPropertyChanged),
                                    null, null, null);

        public string DisplayMember { get; set; }

        //Bindable property for the editor property
        //public static readonly BindableProperty EditorProperty =
            //BindableProperty.Create(nameof(EditorProperty), typeof(IList<object>), typeof(ItemsPicker), null,
                                    //BindingMode.OneWay);
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>
        /// The items source.
        /// </value>
        public new IList ItemsSource
        {
            get { return (IList)base.GetValue(ItemsSourceProperty); }
            set { base.SetValue(ItemsSourceProperty, value); }

        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>
        /// The selected item.
        /// </value>
        public new object SelectedItem
        {
            get { return base.GetValue(SelectedItemProperty); }
            //set { SetValue(SelectedItemProperty, value); }

            set
            {
                if (SelectedItem == value)
                    return;

                base.SetValue(SelectedItemProperty, value);
                if (ItemsSource.Contains(SelectedItem))
                {
                    SelectedIndex = ItemsSource.IndexOf(SelectedItem);
                }
                else
                {
                    SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Display Property.
        /// </summary>
        /// <value>
        /// The display property.
        /// </value>
        public string DisplayProperty
        {
            get { return (string)base.GetValue(DisplayPropertyProperty); }
            set { base.SetValue(DisplayPropertyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Editor property.
        /// </summary>
        /// <value>
        /// The editor property.
        /// </value>
        //public IList<object> Editor
        //{
        //    get { return (IList<object>)base.GetValue(EditorProperty); }
        //    set { base.SetValue(EditorProperty, value); }
        //}

        #endregion

        #region Methods
        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedItem = ItemsSource[SelectedIndex];
            //Editor.Add(SelectedItem);
        }

        /// <summary>
        /// Called when [selected item property changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="value">The value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ItemsPicker picker = (ItemsPicker)bindable;
            picker.SelectedItem = newValue;
            if (picker.ItemsSource != null && picker.SelectedItem != null)
            {
                int count = 0;
                foreach (object obj in picker.ItemsSource)
                {
                    if (obj == picker.SelectedItem)
                    {
                        picker.SelectedIndex = count;
                        break;
                    }
                    count++;
                }
            }
        }

        private static void OnDisplayPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ItemsPicker picker = (ItemsPicker)bindable;
            picker.DisplayProperty = (string)newValue;
            LoadItemsAndSetSelected(bindable);
        }

        /// <summary>
        /// Called when [items source property changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="value">The value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ItemsPicker picker = (ItemsPicker)bindable;
            picker.ItemsSource = (IList)newValue;
            LoadItemsAndSetSelected(bindable);
        }

        private static void LoadItemsAndSetSelected(BindableObject bindable)
        {
            ItemsPicker picker = (ItemsPicker)bindable;

            if (picker.ItemsSource as IEnumerable != null)
            {
                int count = 0;
                foreach (object obj in (IEnumerable)picker.ItemsSource)
                {
                    string value = string.Empty;
                    if (picker.DisplayProperty != null)
                    {
                        var prop = obj.GetType().GetRuntimeProperties()
                                      .FirstOrDefault(p => string.Equals(p.Name,
                                                                         picker.DisplayProperty, StringComparison.OrdinalIgnoreCase));

                        if (prop != null)
                        {
                            value = prop.GetValue(obj).ToString();
                        }
                    }
                    else
                    {
                        value = obj.ToString();
                    }
                    picker.Items.Add(value);

                    if (picker.SelectedItem != null)
                    {
                        if (picker.SelectedItem == obj)
                        {
                            picker.SelectedIndex = count;
                            break;
                        }
                    }
                    count++;
                }
            }
        }
        #endregion
    }
}
