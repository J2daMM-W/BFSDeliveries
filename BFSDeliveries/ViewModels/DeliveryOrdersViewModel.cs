using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BFSDeliveries.Controls;
using BFSDeliveries.Models;
using Prism.Navigation;
using Xamarin.Forms;

namespace BFSDeliveries.ViewModels
{
	public class DeliveryOrdersViewModel : BaseViewModel
    {
        public ObservableCollection<DeliveryOrder> SelectedOrders { get; set; } //Orders to be submitted with a given form
        public SelectableObservableCollection<DeliveryOrder> Items { get; set; } // Drivers delivery orders - (DeliveryOrders) - PickTicket Number List

        public ICommand DoneCommand { get; private set; }
        public ICommand RemoveSelectedCommand { get; }
        public ICommand ToggleSelectionCommand { get; }
        private bool enableMultiSelect;

        INavigationService _navigationService;

        public DeliveryOrdersViewModel()
        {
            Title = "Select PickTicket No.(s)";
            enableMultiSelect = true;
            DoneCommand = new Command(OnDoneSelected);
            //RemoveSelectedCommand = new Command(OnRemoveSelected);
            //ToggleSelectionCommand = new Command(OnToggleSelection);

            RetrieveDeliveryOrders();
        }

        private void RetrieveDeliveryOrders()
        {
            Items = new SelectableObservableCollection<DeliveryOrder>();
            string[] mockPickTickets = { "123456", "654321", "098765", "109283", "657483", "109283", "384756", "209384", "5682038", "797451" };

            foreach (var pickTicket in mockPickTickets)
            {
                Items.Add(new DeliveryOrder { PickTicketNumber = pickTicket });
            }
        }

        public bool EnableMultiSelect
        {
            get { return enableMultiSelect; }
            set
            {
                enableMultiSelect = value;
                OnPropertyChanged();
            }
        }

        private void OnDoneSelected()
        {
            SelectedOrders = new ObservableCollection<DeliveryOrder>();
            var selectedItems = Items.SelectedItems.ToArray();
            foreach (var item in selectedItems)
            {
                SelectedOrders.Add(item);
            }

            _navigationService.GoBackAsync();
        }

        //private void OnRemoveSelected()
        //{
        //    var selectedItems = Items.SelectedItems.ToArray();
        //    foreach (var item in selectedItems)
        //    {
        //        Items.Remove(item);
        //    }
        //}

        //private void OnToggleSelection()
        //{
        //    foreach (var item in Items)
        //    {
        //        item.IsSelected = !item.IsSelected;
        //    }
        //}
    }
}
