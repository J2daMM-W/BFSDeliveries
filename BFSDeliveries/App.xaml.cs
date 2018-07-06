using System;
using BFSDeliveries.Views;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

namespace BFSDeliveries
{
    public partial class App : PrismApplication
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "https://localhost:5000";

        public App(Prism.IPlatformInitializer initializer = null) : base(initializer){}

        protected override void OnInitialized() 
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new LoginPage();
            else
                MainPage = new NavigationPage(new LoginPage());
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<DeliveryOrdersPage>();
            //containerRegistry.RegisterForNavigation<Views.MainPage>();
        }
    }
}
