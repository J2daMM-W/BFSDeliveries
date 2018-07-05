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
        //public ObservableCollection<DeliveryOrder> SelectedOrders { get; set; } //Orders to be submitted with a given form
        public SelectableObservableCollection<DeliveryOrder> Orders { get; set; } // Drivers delivery orders - (DeliveryOrders) - PickTicket Number List

        //private ObservableCollection<DeliveryOrder> _selectedOrders;
        public ObservableCollection<DeliveryOrder> SelectedOrders{ get; set; }
        //{
        //    get { return _selectedOrders; }
        //    set
        //    {
        //        if (Equals(value, _selectedOrders)) return;
        //        if (_selectedOrders != null)
        //            _selectedOrders.CollectionChanged -= SelectedItemsCollectionChanged;
        //        _selectedOrders = value;
        //        if (value != null)
        //            _selectedOrders.CollectionChanged += SelectedItemsCollectionChanged;
        //        OnPropertyChanged(nameof(SelectedOrders));
        //    }
        //}

        //private void SelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    OnPropertyChanged(nameof(SelectedOrders));
        //}

        public ICommand DoneCommand { get; private set; }
        public ICommand RemoveSelectedCommand { get; }
        public ICommand ToggleSelectionCommand { get; }
        //private bool enableMultiSelect;

        INavigationService _navigationService { get; }

        public DeliveryOrdersPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Title = "Select PickTicket No.(s)";
            //enableMultiSelect = true;
            DoneCommand = new Command(OnDoneSelected);
            //RemoveSelectedCommand = new Command(OnRemoveSelected);
            //ToggleSelectionCommand = new Command(OnToggleSelection);

            RetrieveDeliveryOrders();
        }

        private void RetrieveDeliveryOrders()
        {
            Orders = new SelectableObservableCollection<DeliveryOrder>();
            //SelectedOrders = new ObservableCollection<DeliveryOrder>();

            string[] mockPickTickets = { "123456", "654321", "098765", "109283", "657483", "109283", "384756", "209384", "5682038", "797451" };

            foreach (var pickTicket in mockPickTickets)
            {
                Orders.Add(new DeliveryOrder { PickTicketNumber = pickTicket });
            }
        }

        //public bool EnableMultiSelect
        //{
        //    get { return enableMultiSelect; }
        //    set
        //    {
        //        enableMultiSelect = value;
        //        OnPropertyChanged();
        //    }
        //}

        private void OnDoneSelected()
        {
            //List<DeliveryOrder> selectedOrders = new List<DeliveryOrder>();
            SelectedOrders = new ObservableCollection<DeliveryOrder>();
            var selectedItems = Orders.SelectedItems.ToArray();

            foreach (var item in selectedItems)
            {
                SelectedOrders.Add(item);
            }

            //SelectedOrders = new ObservableCollection<DeliveryOrder>(new[] { selectedOrders[0] });
                
            string PickTicketsNumbers = GetPickTicketsFromSelectedOrders();

            //Send selected PickTickets  back
            MessagingCenter.Send<App, string>((App)Application.Current,"SelectedOrders", PickTicketsNumbers);

            _navigationService.GoBackAsync();
        }

        private string GetPickTicketsFromSelectedOrders()
        {
            List<string> _selectedOrders = new List<string>();
            string _selectedResult;

            //updated the Editor with selected orders
            foreach (var selectedOrder in SelectedOrders)
            {
                _selectedOrders.Add(selectedOrder.PickTicketNumber);
            }

            return _selectedResult = string.Join(",", _selectedOrders);
        }


        //private void OnRemoveSelected()
        //{
        //    var selectedItems = Items.SelectedItems.ToArray();
        //    foreach (var item in selectedItems)
        //    {
        //        Items.Remove(item);
        //    }
        //}
    }
}
