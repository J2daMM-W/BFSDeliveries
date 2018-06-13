
using BFSDeliveries.Interfaces;
using BFSDeliveries.Models;
using Prism.Services;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Collections.Generic;
using System.IO;
using System;
using System.Windows.Input;
using BFSDeliveries.Views;
using System.Threading.Tasks;

namespace BFSDeliveries.ViewModels
{
	public class FormDetailPageViewModel : BaseViewModel
	{
		public Form form { get; set; }
		public Delivery delivery { get; set; }
		public ObservableCollection<DeliveryImage> Items { get; set; } // Selected Images 
        public ObservableCollection<DeliveryOrder> SelectedOrders { get; set; } //Orders to be submitted with a given form

		INavigationService _navigationService;

		public IPageDialogService _pageDialogService;
		public DelegateCommand GetPhotoCommand { get; private set; }
		public DelegateCommand CancelCommand { get; private set; }
		public DelegateCommand SubmitCommand { get; private set; }
        public ICommand SelectDeliveriesCommand { get; private set; }


		public FormDetailPageViewModel(IPageDialogService pageDialogService, INavigationService navigationService)
		{
            //RetrieveDeliveryOrders();
			
            Items = new ObservableCollection<DeliveryImage>();

			_navigationService = navigationService;
			_pageDialogService = pageDialogService;

			CancelCommand = new DelegateCommand(CancelFormSubmition);
			SubmitCommand = new DelegateCommand(ExecuteFormSubmition);

			GetPhotoCommand = new DelegateCommand(DisplayActionSheetButtons);

            SelectDeliveriesCommand = new DelegateCommand(async() => await NavigateToDeliveryOrdersPageAsync());

			//Subscribe notification for camera choice
			MessagingCenter.Subscribe<App, byte[]>((App)Xamarin.Forms.Application.Current, "CameraImage", (s, imageAsBytes) =>
			{
				var imageSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));

                Items.Add(new DeliveryImage { Source = imageSource, OrgImage = imageAsBytes });
			});

			//Subscribe notification for photo library choice
			MessagingCenter.Subscribe<App, List<byte[]>>((App)Xamarin.Forms.Application.Current, "ImagesSelected", (s, images) =>
			{
				foreach (byte[] selectedImage in images)
				{
					var newImage = ImageSource.FromStream(() => new MemoryStream(selectedImage));

                    Items.Add(new DeliveryImage { Source = newImage, OrgImage = selectedImage });
				}
			});
		}

        public async Task NavigateToDeliveryOrdersPageAsync()
        {
            string path = "DeliveryOrdersPage";
            await _navigationService.NavigateAsync(path);
            //var navigateTo = typeof(DeliveryOrdersPage);
            //var message = new NavigationMessage(NavigationTo = navigateTo);
            //MessagingCenter.Send(this, message);
        }

        private async void DisplayActionSheetButtons()
		{
			var result = await _pageDialogService.DisplayActionSheetAsync("Get Photo From:", "Cancel", "Camera", "Photo Library");

			if (result == "Camera")
			{
				//send to camera
				await Xamarin.Forms.DependencyService.Get<IMediaService>().UseCamera();
			}
			else if (result == "Photo Library")
			{
				// send to photo lib
				Xamarin.Forms.DependencyService.Get<IMediaService>().UsePhotoGallery();
			}

			//Debug.WriteLine(result);
		}

		private void CancelFormSubmition()
		{
			_navigationService.GoBackAsync();
		}

		private void ExecuteFormSubmition()
		{
			// Do form submit after verification 
			// otherwise cancel
			_navigationService.GoBackAsync();
		}

	}
}
