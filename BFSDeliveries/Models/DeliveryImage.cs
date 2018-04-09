using System;
using Xamarin.Forms;

namespace BFSDeliveries.Models
{
    public class DeliveryImage
    {
        public DeliveryImage()
        {
            ImageId = Guid.NewGuid();
        }

        public Guid ImageId
        {
            get;
            set;
        }

        public ImageSource Source
        {
            get;
            set;
        }

        public byte[] OrgImage
        {
            get;
            set;
        }
    }
}
