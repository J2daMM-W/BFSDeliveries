using System;
using BFSDeliveries.Views;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace BFSDeliveries
{
    public partial class App : PrismApplication
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "https://localhost:5000";

        public App(Prism.IPlatformInitializer initializer = null) : base(initializer) { }
        //private static ILogger logger = DependencyService.Get<ILogManager>().GetLog();

        protected override void OnInitialized()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

            //if (Device.RuntimePlatform == Device.iOS)
            //    MainPage = new LoginPage();
            //else
            MainPage = new NavigationPage(new LoginPage());

            AppCenter.Start("ios=925cde4-709c-4817-9438-cf5ea107068e;" + "android=d211d475-da25-4809-8711-2d557f854c89;", typeof(Analytics), typeof(Crashes));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<DeliveryPhotosPage>();
            containerRegistry.RegisterForNavigation<DeliveryOrdersPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
        }
    }
}
