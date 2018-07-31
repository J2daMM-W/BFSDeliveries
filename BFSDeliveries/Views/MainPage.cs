using System;
using BFSDeliveries.Views;
using Xamarin.Forms;

namespace BFSDeliveries
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page formsPage, tasksPage, outBoxPage, settingsPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    tasksPage = new NavigationPage(new TasksPage())
                    {
                        Title = "Tasks"
                    };

                    formsPage = new NavigationPage(new FormsPage())
                    {
                        Title = "Forms"
                    };

                    outBoxPage = new NavigationPage(new OutBoxPage())
                    {
                        Title = "OutBox"
                    };

                    settingsPage = new NavigationPage(new SettingsPage())
                    {
                        Title = "Settings"
                    };

                    //itemsPage = new NavigationPage(new ItemsPage())
                    //{
                    //    Title = "Browse"
                    //};

                    //aboutPage = new NavigationPage(new AboutPage())
                    //{
                    //    Title = "About"
                    //};

                    tasksPage.Icon = "tab_about.png";
                    formsPage.Icon = "tab_feed.png";
                    outBoxPage.Icon = "tab_feed.png";
                    settingsPage.Icon = "tab_settings.png";

                    //itemsPage.Icon = "tab_feed.png";
                    //aboutPage.Icon = "tab_about.png";
                    break;

                default:
                    tasksPage = new NavigationPage(new TasksPage())
                    {
                        Title = "Tasks"
                    };

                    formsPage = new NavigationPage(new FormsPage())
                    {
                        Title = "Forms"
                    };

                    outBoxPage = new NavigationPage(new OutBoxPage())
                    {
                        Title = "OutBox"
                    };

                    settingsPage = new NavigationPage(new SettingsPage())
                    {
                        Title = "Settings"
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

            Children.Add(tasksPage);
            Children.Add(formsPage);
            Children.Add(outBoxPage);
            Children.Add(settingsPage);
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
