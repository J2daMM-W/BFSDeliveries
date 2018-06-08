using System;

namespace BFSDeliveries.Models
{
    public class SelectableItemWrapper<T>
    {
        public bool IsSelected { get; set; }
        public T Item { get; set; }
    }
}
