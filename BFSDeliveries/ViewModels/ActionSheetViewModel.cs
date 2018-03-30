using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace BFSDeliveries
{
	public class ActionSheetViewModel : INotifyPropertyChanged
	{
		private bool _showDialog;
		private string _result;
		private ICommand _command;
		private ActionSheetParameters _parameters;
		private ActionSheetButton _buttonSelected;
		private IPhotoCapture _photoCapture;
		//private Button _resultSelected;

		public event PropertyChangedEventHandler PropertyChanged;

		public ICommand GetPhotoCommand { get; private set; }
		public ICommand ExecutePhotoCommand { get; private set; }

		public ActionSheetViewModel(IPhotoCapture photoCapture)
		{
			if (photoCapture == null)
			{
				throw new ArgumentNullException(nameof(photoCapture));
			}

			_photoCapture = photoCapture;

			GetPhotoCommand = new Command((o) =>
						{
							DialogParameters = new ActionSheetParameters
							{
								Title = "Get Photo From:",
								Cancel = "Cancel",
								Buttons = new[] { "Camera", "Photo Library" }
							};

							ShowDialog = true;
						});

			//ExecutePhotoCommand = new Command((o) =>
			//{
			//	SelectedButton = new ActionSheetButton
			//	{

			//	};
			//});
		}

		public bool ShowDialog
		{
			get { return _showDialog; }
			set { SetField(ref _showDialog, value); }
		}

		public string Result
		{
			get { return _result; }
			set { SetField(ref _result, value); }
		}

		public ICommand Command
		{
			get { return _command; }
			set { SetField(ref _command, value); }
		}

		public ActionSheetParameters DialogParameters
		{
			get { return _parameters; }
			set { SetField(ref _parameters, value); }
		}

		public ActionSheetButton ActionSelected
		{
			get { return _buttonSelected; }
			set { SetField(ref _buttonSelected, value); }
		}

		public DelegateCommand ShowDialogCommand
		{
			get;
			set;
		}

		public DelegateCommand SelectedButton
		{
			get;
			set;
		}

		protected void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			if (!Object.Equals(field, value))
			{
				field = value;
				//NotifyOfPropertyChange(propertyName);
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}

			//if (propertyName == "Result")
			//{
			//	switch (value.ToString())
			//	{
			//		case "Camera":

			//			//ExecutePhotoCommand = new Command(async () => await _photoCapture.GetPhotosUsingCamera());
			//			break;
			//		case "Photo Library":
			//			break;
			//		default:
			//			break;
			//	}
			//}
		}

		public bool canExecuteGetPhotoCommand()
		{
			//if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsPickPhotoSupported)
			//{
			//	return false;
			//}
			//else
			//{
			return true;
		}
	}
}
