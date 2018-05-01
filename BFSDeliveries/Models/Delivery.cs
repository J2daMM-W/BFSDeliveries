using System;
namespace BFSDeliveries.Models
{
    public class Delivery
    {
        public string PickTicketNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Comments { get; set; }
        public bool NoSuperSign { get; set; }
        public bool FramerSmallParts { get; set; }
        public bool NoReturnAvail { get; set; }
        public bool DamagedProductsNoted { get; set; }
        //public class List<Image> DeliveryPhotos();//List of photos 
    }
}
