using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BFSDeliveries.Models;

namespace BFSDeliveries.ViewModels
{
	public class DeliveryOrdersViewModel : BaseViewModel
    {
        public ObservableCollection<DeliveryOrder> DeliveryOrders { get; set; } // Drivers delivery orders to populate PickTicket Numbers DropDown
        public ObservableCollection<DeliveryOrder> SelectedOrders { get; set; } //Orders to be submitted with a given form
        public ObservableCollection<DeliveryOrder> Items { get; set; } // Drivers delivery orders
        public ICommand DoneCommand { get; private set; }

        public DeliveryOrdersViewModel()
        {
            Title = "Select Pick Ticket(s)";
            DeliveryOrders = new ObservableCollection<DeliveryOrder>();
            RetrieveDeliveryOrders();
        }

        private void RetrieveDeliveryOrders()
        {
            Items = new ObservableCollection<DeliveryOrder>();
            string[] mockPickTickets = { "123456", "654321", "098765", "109283", "657483", "109283", "384756", "209384", "5682038", "797451" };

            foreach (var pickTicket in mockPickTickets)
            {
                Items.Add(new DeliveryOrder { PickTicketNumber = pickTicket });
            }
        }
    }
}
