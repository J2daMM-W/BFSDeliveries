using System;
using System.Collections.Generic;

namespace BFSDeliveries.Models
{
    public class Delivery
    {
        public List<DeliveryOrder> PickTicketNumber = new List<DeliveryOrder>();
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Comments { get; set; }
        public bool NoSuperSign { get; set; }
        public bool FramerSmallParts { get; set; }
        public bool NoReturnAvail { get; set; }
        public bool DamagedProductsNoted { get; set; }
		public List<DeliveryImage> DeliveryImages = new List<DeliveryImage>();
    }
}
