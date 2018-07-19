using System.ComponentModel;

namespace BFSDeliveries.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        public BaseModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
