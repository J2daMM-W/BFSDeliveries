using BFSDeliveries.Droid.DependencyServices;
using BFSDeliveries.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceState))]
namespace BFSDeliveries.Droid.DependencyServices
{
    public class DeviceState : IDeviceState
    {
        public bool isNetworkReachable()
        {
            bool hasInternet = true;
            //NetworkStatus internetStatus = Reachability.InternetConnectionStatus();

            //if (internetStatus == NetworkStatus.NotReachable)
            //{
            //    hasInternet = false;

            //}
            return hasInternet;
        }
    }
}
