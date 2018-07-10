using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BFSDeliveries.Models;
using Prism.Navigation;
using Xamarin.Forms;

namespace BFSDeliveries.ViewModels
{
    public class FormsPageViewModel: BaseViewModel
    {
        public ObservableCollection<Form> Forms { get; set; }
        public Command LoadFormsCommand { get; set; }
        public Command SelectedItemCommand { get; private set; }

        //Form selectedForm;
        //public Form SelectedForm
        //{
        //    get { return selectedForm; }
        //    set
        //    {
        //        if (selectedForm != value)
        //        {
        //            selectedForm = value;
        //            OnPropertyChanged("SelectedForm");
        //        }
        //    }
        //}

        INavigationService _navigationService;

        public FormsPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Title = "Forms";
            Forms = new ObservableCollection<Form>();

            //var _form = form as Form;

            //TODO : need to change this to be dynamic - so we can always add different forms
            Forms.Add(new Form{
                Name = "Delivery Photos",
                Description = "Submit delivery photos"
            });

            //LoadFormsCommand = new Command(async () => await ExecuteLoadFormsCommand());
            SelectedItemCommand = new Command(async () => await ExecuteSelectedItemCommand());
        }

        async Task ExecuteSelectedItemCommand()
        {
            string path = "DeliveryPhotosPage";

            await _navigationService.NavigateAsync(path);
        }

        //async Task ExecuteLoadFormsCommand()
        //{
        //    if (IsBusy)
        //        return;

        //    IsBusy = true;

        //    try{

        //    } catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
    }
}
