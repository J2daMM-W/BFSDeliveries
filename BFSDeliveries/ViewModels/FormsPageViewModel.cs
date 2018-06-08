using System.Collections.ObjectModel;
using BFSDeliveries.Models;
using Xamarin.Forms;

namespace BFSDeliveries.ViewModels
{
    public class FormsPageViewModel: BaseViewModel
    {
        public ObservableCollection<Form> Forms { get; set; }
        public Command LoadFormsCommand { get; set; }

        public FormsPageViewModel()
        {
            Title = "Forms";
            Forms = new ObservableCollection<Form>();

            //var _form = form as Form;

            //need to change this to be dynamic - so we can always add different forms
            Forms.Add(new Form{
                Name = "Delivery Photos",
                Description = "Submit delivery photos"
            });

            //LoadFormsCommand = new Command(async () => await ExecuteLoadFormsCommand());
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
