using System;
using BFSDeliveries.Models;

namespace BFSDeliveries.ViewModels
{
    public class FormDetailViewModel: BaseViewModel
    {
        public Form form { get; set; }
        public Delivery delivery { get;  set;}

        public FormDetailViewModel(Form form = null)
        {
            this.form = form;
        }
    }
}
