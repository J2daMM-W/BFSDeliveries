using System;

namespace BFSDeliveries.Models
{
    public class DeliveryOrder
    {
        public string PickTicketNumber { get; set; }
        public string LocationCode { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime ScanDate { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string StopNumber { get; set; }
    }
}
