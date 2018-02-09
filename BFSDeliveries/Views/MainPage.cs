﻿using System;
using BFSDeliveries.Views;
using Xamarin.Forms;

namespace BFSDeliveries
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page formsPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    formsPage = new NavigationPage(new FormsPage())
                    {
                        Title = "Forms"
                    };

                    //itemsPage = new NavigationPage(new ItemsPage())
                    //{
                    //    Title = "Browse"
                    //};

                    //aboutPage = new NavigationPage(new AboutPage())
                    //{
                    //    Title = "About"
                    //};

                    formsPage.Icon = "tab_feed.png";
                    //itemsPage.Icon = "tab_feed.png";
                    //aboutPage.Icon = "tab_about.png";
                    break;

                default:
                    formsPage = new FormsPage()
                    {
                        Title = "Forms"
                    };

                    //itemsPage = new ItemsPage()
                    //{
                    //    Title = "Browse"
                    //};

                    //aboutPage = new AboutPage()
                    //{
                    //    Title = "About"
                    //};

                    break;
            }

            Children.Add(formsPage);
            //Children.Add(itemsPage);
            //Children.Add(aboutPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
