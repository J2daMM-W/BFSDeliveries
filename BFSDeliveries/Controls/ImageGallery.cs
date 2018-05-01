using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Collections.Specialized;

namespace BFSDeliveries.Controls
{
    public class ImageGallery : ScrollView
    {
        readonly StackLayout _imageStack;

        public ImageGallery()
        {
            this.Orientation = ScrollOrientation.Horizontal;

            _imageStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            this.Content = _imageStack;
        }

        public new IList<View> Children
        {
            get
            {
                return _imageStack.Children;
            }
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource),
                                    typeof(IEnumerable), 
                                    typeof(ImageGallery), 
                                    default(IEnumerable),
                                    defaultBindingMode: BindingMode.Default, 
                                    propertyChanged: OnItemsSourceChanged);


        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            //throw new NotImplementedException();
            //if (ItemsSource == null)
                //return;
            
            var notifyCollection = newValue as INotifyCollectionChanged;

            if (notifyCollection != null)
            {
                notifyCollection.CollectionChanged += (sender, args) => {
                    if (args.NewItems != null)
                    {
                        foreach (var newItem in args.NewItems)
                        {

                            //var view = (View)ItemTemplate.CreateContent();
                            //var bindableObject = view as BindableObject;
                            //if (bindableObject != null)
                            //    bindableObject.BindingContext = newItem;
                            //_imageStack.Children.Add(view);
                        }
                    }
                };
            }
        }

        public DataTemplate ItemTemplate
        {
            get;
            set;
        }

        //void ItemsSourceChanging(BindableObject bindable, object oldValue, object newValue)
        //{
        //    //throw new NotImplementedException();
        //    if (ItemsSource == null)
        //        return;
        //}



    }
}
