using System;
using System.Collections.Generic;

namespace BFSDeliveries.Models
{
    public class Form
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Photo> Image { get; set; }
    }
}
