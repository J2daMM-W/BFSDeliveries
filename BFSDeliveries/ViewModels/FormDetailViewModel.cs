using System;
using System.Threading.Tasks;
using BFSDeliveries.Models;
using Xamarin.Forms;

namespace BFSDeliveries.ViewModels
{
    public class FormDetailViewModel: BaseViewModel
    {
        public Form form { get; set; }
        public Delivery delivery { get;  set;}
        public Command GetPhotoCommand { get; set; }
        public Command DisplayActionSheet { get; set; }

        public FormDetailViewModel()
        {
            //GetPhotoCommand = new Command(async () => await ExecuteGetPhotoCommand());
            //this.form = form;
        }

        //async Task ExecuteGetPhotoCommand()
        //{
        //    DisplayActionSheet = new Command(async () => await ExecuteDisplayActionSheet());
        //}
    }
}
