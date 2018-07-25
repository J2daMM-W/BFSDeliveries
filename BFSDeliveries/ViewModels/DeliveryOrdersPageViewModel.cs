using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using BFSDeliveries.Controls;
using BFSDeliveries.Models;
using Prism.Navigation;
using Xamarin.Forms;

namespace BFSDeliveries.ViewModels
{
	public class DeliveryOrdersPageViewModel : BaseViewModel
    {
        public SelectableObservableCollection<DeliveryOrder> Orders { get; set; } // Drivers delivery orders - (DeliveryOrders) - PickTicket Number List
        public ObservableCollection<DeliveryOrder> SelectedOrders{ get; set; } //Orders to be submitted with a given form


        public ICommand DoneCommand { get; private set; }
        INavigationService _navigationService { get; }

        public DeliveryOrdersPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Title = "Select PickTicket No.(s)";
            DoneCommand = new Command(OnDoneSelected);

            RetrieveDeliveryOrders();
        }

        private void RetrieveDeliveryOrders()
        {
            Orders = new SelectableObservableCollection<DeliveryOrder>();

            string[] mockPickTickets = { "123456", "654321", "098765", "109283", "657483", "109283", "384756", "209384", "5682038", "797451" };

            foreach (var pickTicket in mockPickTickets)
            {
                Orders.Add(new DeliveryOrder { PickTicketNumber = pickTicket });
            }
        }

        private void OnDoneSelected()
        {
            SelectedOrders = new ObservableCollection<DeliveryOrder>();
            var selectedItems = Orders.SelectedItems.ToArray();

            foreach (var item in selectedItems)
            {
                SelectedOrders.Add(item);
            }
                
            string PickTicketsNumbers = GetPickTicketsFromSelectedOrders();

            //Send selected PickTickets  back
            //MessagingCenter.Send((App)Application.Current,"SelectedOrders", PickTicketsNumbers);
            MessagingCenter.Send((App)Application.Current, "SelectedOrders", SelectedOrders);

            _navigationService.GoBackAsync();
        }

        private string GetPickTicketsFromSelectedOrders()
        {
            List<string> _selectedOrders = new List<string>();
            string _selectedResult;

            //update the Editor with selected orders
            foreach (var selectedOrder in SelectedOrders)
            {
                _selectedOrders.Add(selectedOrder.PickTicketNumber);
            }

            return _selectedResult = string.Join(",", _selectedOrders);
        }
    }
}
