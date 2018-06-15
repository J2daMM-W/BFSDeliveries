using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public SelectableObservableCollection<DeliveryOrder> Items { get; set; } // Drivers delivery orders - (DeliveryOrders) - PickTicket Number List


        public ICommand DoneCommand { get; private set; }
        public ICommand RemoveSelectedCommand { get; }
        public ICommand ToggleSelectionCommand { get; }
        private bool enableMultiSelect;

        INavigationService _navigationService { get; }

        public DeliveryOrdersPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

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
            //SelectedOrders = new ObservableCollection<DeliveryOrder>();

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
            List<DeliveryOrder> selectedOrders = new List<DeliveryOrder>();
            var selectedItems = Items.SelectedItems.ToArray();

            foreach (var item in selectedItems)
            {
                selectedOrders.Add(item);
            }

            //Send selected PickTickets  back
            MessagingCenter.Send<App, List<DeliveryOrder>>((App)Xamarin.Forms.Application.Current,"SelectedOrders", selectedOrders);

            _navigationService.GoBackAsync();
        }

        //public void OnNavigatedFrom(NavigationParameters parameters)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnNavigatedTo(NavigationParameters parameters)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnNavigatingTo(NavigationParameters parameters)
        //{
        //    throw new NotImplementedException();
        //}

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
